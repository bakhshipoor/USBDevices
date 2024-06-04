using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;
using static USBDevicesLibrary.Win32API.SetupAPIData;

namespace USBDevicesLibrary.Win32API;

public static partial class SetupAPIFunctions
{
    public static Win32ResponseDataStruct GetClassDevices(Guid classGuid, string Enum, IntPtr hwndParent, uint flags)
    {
        Win32ResponseDataStruct bResponse = new();
        IntPtr devicesHandle = SetupDiGetClassDevs(classGuid, Enum, hwndParent, flags);
        if (devicesHandle != -1)
        {
            bResponse.Status = true;
            bResponse.Data = devicesHandle;
        }
        else
        {
            bResponse.Status = false;
            bResponse.ErrorCode = Marshal.GetLastWin32Error();
            bResponse.ErrorDescription = WindowsSystemErrorCodes.Win32Error[bResponse.ErrorCode];
            bResponse.ErrorFunctionName = "SetupDiGetClassDevs";
        }
        return bResponse;
    }

    public static string GetDeviceId(UInt32 devInst)
    {
        Int32 bufferSize = 1024;
        IntPtr buffer = Marshal.AllocHGlobal(bufferSize);
        string deviceId = string.Empty;
        int errorCode = CM_Get_Device_ID(devInst, buffer, bufferSize, 0);
        if (0 == errorCode)
        {
            var id = Marshal.PtrToStringAuto(buffer);
            if (id != null)
                deviceId = id;
        }
        Marshal.FreeHGlobal(buffer);
        return deviceId;
    }

    public static List<string>? GetDeviceProperty(IntPtr hDevInfo, SP_DEVINFO_DATA devInfoData, SPDRP property)
    {
        List<string>? deviceProperty = [];
        uint PropertyBufferSize = 4096;
        byte[] PropertyBuffer = new byte[PropertyBufferSize];
        bool res = SetupDiGetDeviceRegistryProperty(hDevInfo, ref devInfoData, (uint)property,
            out uint PropertyRegDataType, PropertyBuffer, PropertyBufferSize, out uint requiredSize);
        if (property == SPDRP.SPDRP_ADDRESS)
        {
        }
        if (res)
        {
            if (property == SPDRP.SPDRP_BUSTYPEGUID)
            {
                byte[] busTypeGuidByte = new byte[16];
                Array.Copy(PropertyBuffer, busTypeGuidByte, 16);
                Guid busTypeGuid = new Guid(busTypeGuidByte);
                deviceProperty.Add(busTypeGuid.ToString());
            }
            else
            {
                List<char> data = new();
                char[] dataBuffer = Encoding.UTF8.GetString(PropertyBuffer, 0, (int)requiredSize).ToArray();
                for (int i = 0; i < dataBuffer.Length; i++)
                {
                    if (i % 2 == 0)
                        data.Add(dataBuffer[i]);
                }
                string[] dataArray = new string(data.ToArray()).Split("\0");
                foreach (string d in dataArray)
                {
                    if (!string.IsNullOrEmpty(d))
                        deviceProperty.Add(d);
                }
            }
        }
        return deviceProperty;
    }

    public static Win32ResponseDataStruct EnumDeviceInfo(IntPtr hDevInfo, uint memberIndex)
    {
        Win32ResponseDataStruct bResponse = new();
        SP_DEVINFO_DATA DeviceInfoData = new();
        DeviceInfoData.CbSize = (uint)Marshal.SizeOf(typeof(SP_DEVINFO_DATA));

        bool isSuccess = SetupDiEnumDeviceInfo(hDevInfo, memberIndex, out DeviceInfoData);
        if (isSuccess)
        {
            bResponse.Status = true;
            bResponse.Data = DeviceInfoData;
        }
        else
        {
            bResponse.Status = false;
            bResponse.ErrorCode = Marshal.GetLastWin32Error();
            bResponse.ErrorDescription = WindowsSystemErrorCodes.Win32Error[bResponse.ErrorCode];
            bResponse.ErrorFunctionName = "SetupDiEnumDeviceInfo";
        }
        return bResponse;
    }

    public static Win32ResponseDataStruct EnumDeviceInterfaces(IntPtr hDevInfo, SP_DEVINFO_DATA deviceInfoData, Guid interfaceClassGuid, uint memberIndexInterfaces)
    {
        Win32ResponseDataStruct bResponse = new();
        SpDeviceInterfaceData spDeviceInterfaceData = new SpDeviceInterfaceData();
        spDeviceInterfaceData.CbSize = (uint)Marshal.SizeOf(typeof(SpDeviceInterfaceData));

        bool isSuccess = SetupDiEnumDeviceInterfaces(hDevInfo, deviceInfoData, interfaceClassGuid, memberIndexInterfaces, out spDeviceInterfaceData);
        if (isSuccess)
        {
            bResponse.Status = true;
            bResponse.Data = spDeviceInterfaceData;
        }
        else
        {
            bResponse.Status = false;
            bResponse.ErrorCode = Marshal.GetLastWin32Error();
            bResponse.ErrorDescription = WindowsSystemErrorCodes.Win32Error[bResponse.ErrorCode];
            bResponse.ErrorFunctionName = "SetupDiEnumDeviceInterfaces";
        }
        return bResponse;
    }

    public static Win32ResponseDataStruct GetDevicePropertyKeyArray(IntPtr hDevInfo, SP_DEVINFO_DATA DeviceInfoData)
    {
        Win32ResponseDataStruct bResponse = new();
        uint PropertyKeyCount = 200; // Maximum DEVPKEY property count is 197
        DEVPROPKEY[] prePropertyKeyArray = new DEVPROPKEY[PropertyKeyCount];
        uint RequiredPropertyKeyCount = 0;
        bool isSuccess = SetupDiGetDevicePropertyKeys(hDevInfo, ref DeviceInfoData, prePropertyKeyArray, PropertyKeyCount, ref RequiredPropertyKeyCount, 0);
        if (isSuccess)
        {
            DEVPROPKEY[] finalPropertyKeyArray = new DEVPROPKEY[RequiredPropertyKeyCount];
            Array.Copy(prePropertyKeyArray, finalPropertyKeyArray, RequiredPropertyKeyCount);
            bResponse.Status = true;
            bResponse.Data = finalPropertyKeyArray;
            bResponse.LengthTransferred = RequiredPropertyKeyCount;
        }
        else
        {
            bResponse.Status = false;
            bResponse.ErrorCode = Marshal.GetLastWin32Error();
            bResponse.ErrorDescription = WindowsSystemErrorCodes.Win32Error[bResponse.ErrorCode];
            bResponse.ErrorFunctionName = "SetupDiGetClassDevs";
        }
        return bResponse;
    }

    public static Win32ResponseDataStruct GetDevicePropertyValueByPropertyKey(IntPtr hDevInfo, SP_DEVINFO_DATA DeviceInfoData, DEVPROPKEY PropertyKey)
    {
        Win32ResponseDataStruct bResponse = new();

        uint PropertyBufferSize = 4096;
        byte[] prePropertyBuffer = new byte[PropertyBufferSize];
        bool isSuccess = SetupDiGetDevicePropertyW(hDevInfo, DeviceInfoData, PropertyKey, out DevPropType propertyType, prePropertyBuffer, PropertyBufferSize, out uint RequiredSize, 0);
        if (isSuccess)
        {
            byte[] finalPropertyBuffer = new byte[RequiredSize];
            Array.Copy(prePropertyBuffer, finalPropertyBuffer, RequiredSize);

            PropertyValueResponse bResult = GetPropertyValueByType(propertyType, finalPropertyBuffer);

            bResponse.Status = true;
            bResponse.Data = bResult.Data;
            bResponse.DataType = bResult.DataType;
            bResponse.LengthTransferred = RequiredSize;
        }
        else
        {
            bResponse.Status = false;
            bResponse.ErrorCode = Marshal.GetLastWin32Error();
            bResponse.ErrorDescription = WindowsSystemErrorCodes.Win32Error[bResponse.ErrorCode];
            bResponse.ErrorFunctionName = "SetupDiGetClassDevs";
        }

        return bResponse;
    }

    public static Win32ResponseDataStruct GetClassPropertyKeyArray(Guid classGuid, DICLASSPROP flag)
    {
        Win32ResponseDataStruct bResponse = new();
        uint PropertyKeyCount = 200; // Maximum DEVPKEY property count is 197
        DEVPROPKEY[] prePropertyKeyArray = new DEVPROPKEY[PropertyKeyCount];
        uint RequiredPropertyKeyCount = 0;
        bool isSuccess = SetupDiGetClassPropertyKeys(classGuid, prePropertyKeyArray, PropertyKeyCount, ref RequiredPropertyKeyCount, flag);
        if (isSuccess)
        {
            DEVPROPKEY[] finalPropertyKeyArray = new DEVPROPKEY[RequiredPropertyKeyCount];
            Array.Copy(prePropertyKeyArray, finalPropertyKeyArray, RequiredPropertyKeyCount);
            bResponse.Status = true;
            bResponse.Data = finalPropertyKeyArray;
            bResponse.LengthTransferred = RequiredPropertyKeyCount;
        }
        else
        {
            bResponse.Status = false;
            bResponse.ErrorCode = Marshal.GetLastWin32Error();
            if (WindowsSystemErrorCodes.Win32Error.ContainsKey(bResponse.ErrorCode))
                bResponse.ErrorDescription = WindowsSystemErrorCodes.Win32Error[bResponse.ErrorCode];
            else
                bResponse.ErrorDescription ="Unknown Error Code!";
            bResponse.ErrorFunctionName = "SetupDiGetClassDevs";
        }
        return bResponse;
    }

    public static Win32ResponseDataStruct GetClassPropertyValueByPropertyKey(Guid classGuid, DEVPROPKEY PropertyKey, DICLASSPROP flag)
    {
        Win32ResponseDataStruct bResponse = new();

        uint PropertyBufferSize = 4096;
        byte[] prePropertyBuffer = new byte[PropertyBufferSize];
        bool isSuccess = SetupDiGetClassPropertyW(classGuid, PropertyKey, out DevPropType propertyType, prePropertyBuffer, PropertyBufferSize, out uint RequiredSize, flag);
        if (isSuccess)
        {
            byte[] finalPropertyBuffer = new byte[RequiredSize];
            Array.Copy(prePropertyBuffer, finalPropertyBuffer, RequiredSize);

            PropertyValueResponse bResult = GetPropertyValueByType(propertyType, finalPropertyBuffer);

            bResponse.Status = true;
            bResponse.Data = bResult.Data;
            bResponse.DataType = bResult.DataType;
            bResponse.LengthTransferred = RequiredSize;
        }
        else
        {
            bResponse.Status = false;
            bResponse.ErrorCode = Marshal.GetLastWin32Error();
            bResponse.ErrorDescription = WindowsSystemErrorCodes.Win32Error[bResponse.ErrorCode];
            bResponse.ErrorFunctionName = "SetupDiGetClassDevs";
        }

        return bResponse;
    }

    public static Win32ResponseDataStruct GetDeviceInterfacePropertyKeyArray(IntPtr hDevInfo, SpDeviceInterfaceData interfaceData)
    {
        Win32ResponseDataStruct bResponse = new();
        uint PropertyKeyCount = 200; // Maximum DEVPKEY property count is 197
        DEVPROPKEY[] prePropertyKeyArray = new DEVPROPKEY[PropertyKeyCount];
        uint RequiredPropertyKeyCount = 0;
        bool isSuccess = SetupDiGetDeviceInterfacePropertyKeys(hDevInfo, interfaceData, prePropertyKeyArray, PropertyKeyCount, ref RequiredPropertyKeyCount, 0);
        if (isSuccess)
        {
            DEVPROPKEY[] finalPropertyKeyArray = new DEVPROPKEY[RequiredPropertyKeyCount];
            Array.Copy(prePropertyKeyArray, finalPropertyKeyArray, RequiredPropertyKeyCount);
            bResponse.Status = true;
            bResponse.Data = finalPropertyKeyArray;
            bResponse.LengthTransferred = RequiredPropertyKeyCount;
        }
        else
        {
            bResponse.Status = false;
            bResponse.ErrorCode = Marshal.GetLastWin32Error();
            if (WindowsSystemErrorCodes.Win32Error.ContainsKey(bResponse.ErrorCode))
                bResponse.ErrorDescription = WindowsSystemErrorCodes.Win32Error[bResponse.ErrorCode];
            else
                bResponse.ErrorDescription = "Unknown Error Code!";
            bResponse.ErrorFunctionName = "SetupDiGetDeviceInterfacePropertyKeys";
        }
        return bResponse;
    }

    public static Win32ResponseDataStruct GetDeviceInterfacePropertyValueByPropertyKey(IntPtr hDevInfo, SpDeviceInterfaceData interfaceData, DEVPROPKEY PropertyKey)
    {
        Win32ResponseDataStruct bResponse = new();

        uint PropertyBufferSize = 4096;
        byte[] prePropertyBuffer = new byte[PropertyBufferSize];
        bool isSuccess = SetupDiGetDeviceInterfacePropertyW(hDevInfo, interfaceData, PropertyKey, out DevPropType propertyType, prePropertyBuffer, PropertyBufferSize, out uint RequiredSize, 0);
        if (isSuccess)
        {
            byte[] finalPropertyBuffer = new byte[RequiredSize];
            Array.Copy(prePropertyBuffer, finalPropertyBuffer, RequiredSize);

            PropertyValueResponse bResult = GetPropertyValueByType(propertyType, finalPropertyBuffer);

            bResponse.Status = true;
            bResponse.Data = bResult.Data;
            bResponse.DataType = bResult.DataType;
            bResponse.LengthTransferred = RequiredSize;
        }
        else
        {
            bResponse.Status = false;
            bResponse.ErrorCode = Marshal.GetLastWin32Error();
            bResponse.ErrorDescription = WindowsSystemErrorCodes.Win32Error[bResponse.ErrorCode];
            bResponse.ErrorFunctionName = "SetupDiGetDeviceInterfacePropertyW";
        }

        return bResponse;
    }

    public static PropertyValueResponse GetPropertyValueByType(DevPropType propertyType, byte[] prePropertyBuffer)
    {
        PropertyValueResponse bResponse = new();
        switch (propertyType)
        {
            case DevPropType.DEVPROP_TYPE_EMPTY:
                {

                    break;
                }
            case DevPropType.DEVPROP_TYPE_NULL:
                {

                    break;
                }
            case DevPropType.DEVPROP_TYPE_SBYTE:
                {
                    sbyte sb = (sbyte)prePropertyBuffer[0];
                    bResponse.Data = sb;
                    bResponse.DataType = typeof(sbyte);
                    break;
                }
            case DevPropType.DEVPROP_TYPE_BINARY:
            case DevPropType.DEVPROP_TYPE_BYTE:
                {
                    byte b = prePropertyBuffer[0];
                    bResponse.Data = b;
                    bResponse.DataType = typeof(byte);
                    break;
                }
            case DevPropType.DEVPROP_TYPE_INT16:
                {
                    Int16 num = BitConverter.ToInt16(prePropertyBuffer, 0);
                    bResponse.Data = num;
                    bResponse.DataType = typeof(Int16);
                    break;
                }
            case DevPropType.DEVPROP_TYPE_UINT16:
                {
                    UInt16 num = BitConverter.ToUInt16(prePropertyBuffer, 0);
                    bResponse.Data = num;
                    bResponse.DataType = typeof(UInt16);
                    break;
                }
            case DevPropType.DEVPROP_TYPE_INT32:
                {
                    Int32 num = BitConverter.ToInt32(prePropertyBuffer, 0);
                    bResponse.Data = num;
                    bResponse.DataType = typeof(Int32);
                    break;
                }
            case DevPropType.DEVPROP_TYPE_ERROR:
            case DevPropType.DEVPROP_TYPE_NTSTATUS:
            case DevPropType.DEVPROP_TYPE_UINT32:
                {
                    UInt32 num = BitConverter.ToUInt32(prePropertyBuffer,0);
                    bResponse.Data = num;
                    bResponse.DataType = typeof(UInt32);
                    break;
                }
            case DevPropType.DEVPROP_TYPE_CURRENCY:
            case DevPropType.DEVPROP_TYPE_INT64:
                {
                    Int64 num = BitConverter.ToInt64(prePropertyBuffer, 0);
                    bResponse.Data = num;
                    bResponse.DataType = typeof(Int64);
                    break;
                }
            case DevPropType.DEVPROP_TYPE_UINT64:
                {
                    UInt64 num = BitConverter.ToUInt64(prePropertyBuffer, 0);
                    bResponse.Data = num;
                    bResponse.DataType = typeof(UInt64);
                    break;
                }
            case DevPropType.DEVPROP_TYPE_FLOAT:
                {
                    float num = BitConverter.ToSingle(prePropertyBuffer, 0);
                    bResponse.Data = num;
                    bResponse.DataType = typeof(float);
                    break;
                }
            case DevPropType.DEVPROP_TYPE_DOUBLE:
                {
                    double num = BitConverter.ToDouble(prePropertyBuffer, 0);
                    bResponse.Data = num;
                    bResponse.DataType = typeof(double);
                    break;
                }
            case DevPropType.DEVPROP_TYPE_DECIMAL:
                {
                    decimal num = Convert.ToDecimal(BitConverter.ToDouble(prePropertyBuffer, 0));
                    bResponse.Data = num;
                    bResponse.DataType = typeof(decimal);
                    break;
                }
            case DevPropType.DEVPROP_TYPE_GUID:
                {
                    Guid guid = new Guid(prePropertyBuffer);
                    bResponse.Data = guid;
                    bResponse.DataType = typeof(Guid);
                    break;
                }
            case DevPropType.DEVPROP_TYPE_DATE:
                {
                    double dateNum = BitConverter.ToDouble(prePropertyBuffer, 0);
                    DateTime date = DateTime.FromOADate(dateNum);
                    bResponse.Data = date;
                    bResponse.DataType = typeof(DateTime);
                    break;
                }
            case DevPropType.DEVPROP_TYPE_FILETIME:
                {
                    Int64 fileTimeNum = BitConverter.ToInt64(prePropertyBuffer, 0);
                    DateTime fileTime = DateTime.FromFileTime(fileTimeNum);
                    bResponse.Data = fileTime;
                    bResponse.DataType = typeof(DateTime);
                    break;
                }
            case DevPropType.DEVPROP_TYPE_BOOLEAN:
                {
                    bool status = BitConverter.ToBoolean(prePropertyBuffer, 0);
                    bResponse.Data = status;
                    bResponse.DataType = typeof(bool);
                    break;
                }
            case DevPropType.DEVPROP_TYPE_SECURITY_DESCRIPTOR_STRING:
            case DevPropType.DEVPROP_TYPE_STRING_INDIRECT:
            case DevPropType.DEVPROP_TYPE_STRING:
                {
                    string str = Encoding.Unicode.GetString(prePropertyBuffer).Replace("\0", "");
                    bResponse.Data = str;
                    bResponse.DataType = typeof(string);
                    break;
                }
            case DevPropType.DEVPROP_TYPE_STRING_LIST:
                {
                    string[] strings = Encoding.Unicode.GetString(prePropertyBuffer).Split("\0");
                    List<string> liststr = new List<string>();
                    foreach (string str in strings)
                    {
                        if (!string.IsNullOrEmpty(str))
                            liststr.Add(str);
                    }
                    bResponse.Data = liststr;
                    bResponse.DataType = typeof(List<string>);
                    break;
                }
            case DevPropType.DEVPROP_TYPE_SECURITY_DESCRIPTOR:
                {
                    RawSecurityDescriptor securityDescriptor = new RawSecurityDescriptor(prePropertyBuffer, 0);
                    bResponse.Data = securityDescriptor;
                    bResponse.DataType = typeof(RawSecurityDescriptor);
                    break;
                }
            case DevPropType.DEVPROP_TYPE_DEVPROPKEY:
                {
                    // To DO
                    break;
                }
            case DevPropType.DEVPROP_TYPE_DEVPROPTYPE:
                {
                    UInt32 devProptype = BitConverter.ToUInt32(prePropertyBuffer, 0);
                    bResponse.Data = (DevPropType)devProptype;
                    bResponse.DataType = typeof(DevPropType);
                    break;
                }
        }
        return bResponse;
    }

    public static Win32ResponseDataStruct GetDeviceInterfaceDetail(IntPtr hDevInfo, SpDeviceInterfaceData interfaceData)
    {
        Win32ResponseDataStruct bResponse = new();
        SP_DEVINFO_DATA deviceInfoData;
        deviceInfoData.CbSize = (uint)Marshal.SizeOf(typeof(SP_DEVINFO_DATA));
        SpDeviceInterfaceDetailData spDeviceInterfaceDetailData = new SpDeviceInterfaceDetailData();
        uint deviceInterfaceDetailDataSize = 1024;
        uint requiredSize = 0;
        bool isSuccess = SetupDiGetDeviceInterfaceDetail(hDevInfo, interfaceData, ref spDeviceInterfaceDetailData, deviceInterfaceDetailDataSize, out requiredSize, out deviceInfoData);
        if (isSuccess)
        {
            bResponse.Status = true;
            bResponse.Data = spDeviceInterfaceDetailData;
            bResponse.DataType = typeof(SP_DEVINFO_DATA);
            bResponse.LengthTransferred = requiredSize;
        }
        else
        {
            bResponse.Status = false;
            bResponse.ErrorCode = Marshal.GetLastWin32Error();
            bResponse.ErrorDescription = WindowsSystemErrorCodes.Win32Error[bResponse.ErrorCode];
            bResponse.ErrorFunctionName = "SetupDiGetDeviceInterfaceDetail";
        }

        return bResponse;
    }

    public static Win32ResponseDataStruct EnumDriverInfo(IntPtr hDevInfo, SP_DEVINFO_DATA deviceInfoData, SPDIT driverType, uint memberIndexDriverInfo)
    {
        Win32ResponseDataStruct bResponse = new();
        PSP_DRVINFO_DATA_W driverInfoData = new PSP_DRVINFO_DATA_W();
        driverInfoData.cbSize = (uint)Marshal.SizeOf(typeof(PSP_DRVINFO_DATA_W));

        bool isSuccess = SetupDiEnumDriverInfoW(hDevInfo, deviceInfoData, driverType, memberIndexDriverInfo, out driverInfoData);
        if (isSuccess)
        {
            bResponse.Status = true;
            bResponse.Data = driverInfoData;
        }
        else
        {
            bResponse.Status = false;
            bResponse.ErrorCode = Marshal.GetLastWin32Error();
            bResponse.ErrorDescription = WindowsSystemErrorCodes.Win32Error[bResponse.ErrorCode];
            bResponse.ErrorFunctionName = "SetupDiEnumDeviceInterfaces";
        }
        return bResponse;
    }


}

public struct PropertyValueResponse
{
    public object Data;
    public Type DataType;
}
