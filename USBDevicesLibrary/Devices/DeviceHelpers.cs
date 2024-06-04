using System.Collections.ObjectModel;
using System.Reflection;
using System.Security.AccessControl;
using USBDevicesLibrary.Devices;
using USBDevicesLibrary.Win32API;
using static USBDevicesLibrary.Devices.DevicePropertiesTypeHelper;
using static USBDevicesLibrary.Win32API.DevicePropKeys;
using static USBDevicesLibrary.Win32API.SetupAPIData;

namespace USBDevicesLibrary.Devices;

public static class DeviceHelpers
{
    public static ObservableCollection<Device> GetClassDevicesWithProperties(Guid classGuid, string enumString, uint flags)
    {
        ObservableCollection<Device> devices = new();
        Win32ResponseDataStruct devicesHandle = SetupAPIFunctions.GetClassDevices(classGuid, enumString, IntPtr.Zero, flags);
        if (devicesHandle.Status) // INVALID_HANDLE_VALUE = -1
        {
            IntPtr hDevInfo = ((IntPtr)devicesHandle.Data);
            uint memberIndex = 0;
            while (true)
            {
                // Create New Device
                Device device = new();

                // Get Device Info Data
                Win32ResponseDataStruct enumDeviceInfo = SetupAPIFunctions.EnumDeviceInfo(hDevInfo, memberIndex);
                if (!enumDeviceInfo.Status)
                {
                    //throw new Exception(deviceInfo.ToString());
                    break;
                }

                SP_DEVINFO_DATA deviceInfoData = (SP_DEVINFO_DATA)enumDeviceInfo.Data;

                // Get Device Properties 
                Win32ResponseDataStruct devicePropertyKeyArray = SetupAPIFunctions.GetDevicePropertyKeyArray(hDevInfo, deviceInfoData);
                if (devicePropertyKeyArray.Status)
                {
                    DEVPROPKEY[] _DevicePropertyKeyArray = (DEVPROPKEY[])devicePropertyKeyArray.Data;
                    foreach (DEVPROPKEY itemDevicePropKey in _DevicePropertyKeyArray)
                    {
                        Win32ResponseDataStruct propertyValue = SetupAPIFunctions.GetDevicePropertyValueByPropertyKey(hDevInfo, deviceInfoData, itemDevicePropKey);
                        if (propertyValue.Status)
                        {
                            SetDevicePropertiesValue(device, DevicePropertiesTtype.DeviceProperties, itemDevicePropKey, propertyValue.Data, propertyValue.DataType);
                        }
                    }
                }

                // Get Device Class Properties
                if (device.DeviceProperties.Device_ClassGuid != Guid.Empty)
                {
                    Win32ResponseDataStruct classPropertyKeyArray = SetupAPIFunctions.GetClassPropertyKeyArray(device.DeviceProperties.Device_ClassGuid, DICLASSPROP.DICLASSPROP_INSTALLER);
                    if (classPropertyKeyArray.Status)
                    {
                        DEVPROPKEY[] _ClassPropertyKeyArray = (DEVPROPKEY[])classPropertyKeyArray.Data;
                        foreach (DEVPROPKEY itemClassPropKey in _ClassPropertyKeyArray)
                        {
                            Win32ResponseDataStruct propertyValue = SetupAPIFunctions.GetClassPropertyValueByPropertyKey(device.DeviceProperties.Device_ClassGuid, itemClassPropKey, DICLASSPROP.DICLASSPROP_INSTALLER);
                            if (propertyValue.Status)
                            {
                                SetDevicePropertiesValue(device, DevicePropertiesTtype.ClassProperties, itemClassPropKey, propertyValue.Data, propertyValue.DataType);
                            }
                        }
                    }
                }

                // Device Interfaces Data
                uint memberIndexInterfaces = 0;
                while (true)
                {
                    // Get Device Interface Data
                    Win32ResponseDataStruct enumDeviceInterface = SetupAPIFunctions.EnumDeviceInterfaces(hDevInfo, deviceInfoData, classGuid, memberIndexInterfaces);
                    if (!enumDeviceInterface.Status)
                        break;

                    SpDeviceInterfaceData deviceInterfaceData = (SpDeviceInterfaceData)enumDeviceInterface.Data;

                    // Get Interface Properties 
                    Win32ResponseDataStruct interfacePropertyKeyArray = SetupAPIFunctions.GetDeviceInterfacePropertyKeyArray(hDevInfo, deviceInterfaceData);
                    if (interfacePropertyKeyArray.Status)
                    {
                        DEVPROPKEY[] _InterfacePropertyKeyArray = (DEVPROPKEY[])interfacePropertyKeyArray.Data;
                        foreach (DEVPROPKEY itemInterfacePropKey in _InterfacePropertyKeyArray)
                        {
                            Win32ResponseDataStruct propertyValue = SetupAPIFunctions.GetDeviceInterfacePropertyValueByPropertyKey(hDevInfo, deviceInterfaceData, itemInterfacePropKey);
                            if (propertyValue.Status)
                            {
                                SetDevicePropertiesValue(device, DevicePropertiesTtype.InterfaceProperties, itemInterfacePropKey, propertyValue.Data, propertyValue.DataType);
                            }
                        }
                    }

                    // Get Interface Detail
                    Win32ResponseDataStruct interfaceDetail = SetupAPIFunctions.GetDeviceInterfaceDetail(hDevInfo, deviceInterfaceData);
                    if (interfaceDetail.Status)
                    {
                        device.DevicePath = ((SpDeviceInterfaceDetailData)interfaceDetail.Data).DevicePath;
                    }

                    memberIndexInterfaces++;
                }

                devices.Add(device);
                memberIndex++;
            }
            SetupAPIFunctions.SetupDiDestroyDeviceInfoList(hDevInfo);
        }
        return devices;
    }

    public static ObservableCollection<Device> GetClassDevices(Guid classGuid, string enumString, uint flags)
    {
        ObservableCollection<Device> devices = new();
        Win32ResponseDataStruct devicesHandle = SetupAPIFunctions.GetClassDevices(classGuid, enumString, IntPtr.Zero, flags);
        if (devicesHandle.Status) 
        {
            IntPtr hDevInfo = ((IntPtr)devicesHandle.Data);
            uint memberIndex = 0;
            while (true)
            {
                // Create New Device
                Device device = new();

                // Get Device Info Data
                Win32ResponseDataStruct enumDeviceInfo = SetupAPIFunctions.EnumDeviceInfo(hDevInfo, memberIndex);
                if (!enumDeviceInfo.Status)
                {
                    //throw new Exception(deviceInfo.ToString());
                    break;
                }

                SP_DEVINFO_DATA deviceInfoData = (SP_DEVINFO_DATA)enumDeviceInfo.Data;

                string deviceID = SetupAPIFunctions.GetDeviceId(deviceInfoData.DevInst);
                if (!string.IsNullOrEmpty(deviceID))
                    device.DeviceProperties.Device_InstanceId = deviceID;

                // Device Interfaces Data
                uint memberIndexInterfaces = 0;
                while (true)
                {
                    // Get Device Interface Data
                    Win32ResponseDataStruct enumDeviceInterface = SetupAPIFunctions.EnumDeviceInterfaces(hDevInfo, deviceInfoData, classGuid, memberIndexInterfaces);
                    if (!enumDeviceInterface.Status)
                        break;

                    SpDeviceInterfaceData deviceInterfaceData = (SpDeviceInterfaceData)enumDeviceInterface.Data;

                    // Get Interface Detail
                    Win32ResponseDataStruct interfaceDetail = SetupAPIFunctions.GetDeviceInterfaceDetail(hDevInfo, deviceInterfaceData);
                    if (interfaceDetail.Status)
                        device.DevicePath = ((SpDeviceInterfaceDetailData)interfaceDetail.Data).DevicePath;

                    memberIndexInterfaces++;
                }

                devices.Add(device);
                memberIndex++;
            }
            SetupAPIFunctions.SetupDiDestroyDeviceInfoList(hDevInfo);
        }
        return devices;
    }

    public static void SetDevicePropertiesValue(Device device, DevicePropertiesTtype devicePropertiesTtype, DEVPROPKEY itemPropKey, object propertyValue, Type propertyType)
    {
        PropertyInfo[] properties;
        object baseProperties;
        if (devicePropertiesTtype == DevicePropertiesTtype.DeviceProperties)
        {
            baseProperties = device.DeviceProperties;
            properties = typeof(DeviceProperties).GetProperties();
        }
        else if (devicePropertiesTtype == DevicePropertiesTtype.ClassProperties)
        {
            baseProperties = device.ClassProperties;
            properties = typeof(DeviceClassProperties).GetProperties();
        }
        //else if (devicePropertiesTtype == DevicePropertiesTtype.InterfaceProperties)
        //{
        //    baseProperties = device.InterfaceProperties;
        //    properties = typeof(DeviceInterfaceProperties).GetProperties();
        //}
        //else if (devicePropertiesTtype == DevicePropertiesTtype.ContainerProperties)
        //{
        //    baseProperties = device.ContainerProperties;
        //    properties = typeof(DeviceContainerProperties).GetProperties();
        //}
        //else if (devicePropertiesTtype == DevicePropertiesTtype.DriverProperties)
        //{
        //    baseProperties = device.DriverProperties;
        //    properties = typeof(DeviceDriverProperties).GetProperties();
        //}
        else return;

        if (DEVPKEY.ContainsKey(itemPropKey))
        {
            string propertyName = DEVPKEY[itemPropKey].ToString().Remove(0, 8);
            foreach (PropertyInfo property in properties)
            {
                if (
                    propertyType == typeof(string) ||
                    propertyType == typeof(List<string>) ||
                    propertyType == typeof(Int16) ||
                    propertyType == typeof(UInt16) ||
                    propertyType == typeof(Int32) ||
                    propertyType == typeof(Int64) ||
                    propertyType == typeof(UInt64) ||
                    propertyType == typeof(sbyte) ||
                    propertyType == typeof(bool) ||
                    propertyType == typeof(float) ||
                    propertyType == typeof(double) ||
                    propertyType == typeof(decimal) ||
                    propertyType == typeof(Guid) ||
                    propertyType == typeof(DateTime) ||
                    propertyType == typeof(RawSecurityDescriptor) ||
                    propertyType == typeof(DevPropType)
                    )
                {
                    if (property.Name == propertyName)
                    {
                        property.SetValue(baseProperties, propertyValue);
                    }
                }
                else if (propertyType == typeof(byte))
                {
                    if (property.Name == "Device_PowerData")
                    {
                        List<DEVICED_POWER_INFORMATION> pi = [];
                        foreach (DEVICED_POWER_INFORMATION item in Enum.GetValues(typeof(DEVICED_POWER_INFORMATION)))
                            if (((int)item & (byte)propertyValue) != 0)
                                pi.Add(item);
                        property.SetValue(baseProperties, pi);
                    }
                    else if (property.Name == propertyName)
                    {
                        property.SetValue(baseProperties, propertyValue);
                    }
                }
                else if (propertyType == typeof(UInt32))
                {
                    if (property.Name == "Device_ConfigFlags" || property.Name == "DeviceContainer_ConfigFlags")
                    {
                        List<CONFIG_FLAGS> cf = [];
                        foreach (CONFIG_FLAGS item in Enum.GetValues(typeof(CONFIG_FLAGS)))
                            if (((int)item & (uint)propertyValue) != 0)
                                cf.Add(item);
                        property.SetValue(baseProperties, cf);
                    }
                    else if (property.Name == "Device_Capabilities")
                    {
                        List<CM_DRP_CAPABILITIES> cap = [];
                        foreach (CM_DRP_CAPABILITIES item in Enum.GetValues(typeof(CM_DRP_CAPABILITIES)))
                            if (((int)item & (uint)propertyValue) != 0)
                                cap.Add(item);
                        property.SetValue(baseProperties, cap);
                    }
                    else if (property.Name == "Device_Characteristics")
                    {
                        List<DEVICE_CHARACTERISTICS> ch = [];
                        foreach (DEVICE_CHARACTERISTICS item in Enum.GetValues(typeof(DEVICE_CHARACTERISTICS)))
                            if (((int)item & (uint)propertyValue) != 0)
                                ch.Add(item);
                        property.SetValue(baseProperties, ch);
                    }
                    else if (property.Name == "Device_DevNodeStatus")
                    {
                        List<DEVICE_INSTANCE_STATUS> ns = [];
                        foreach (DEVICE_INSTANCE_STATUS item in Enum.GetValues(typeof(DEVICE_INSTANCE_STATUS)))
                            if (((int)item & (uint)propertyValue) != 0)
                                ns.Add(item);
                        property.SetValue(baseProperties, ns);
                    }
                    else if (property.Name == "Device_DriverRank")
                    {
                        List<SP_DRVINSTALL_PARAMS> dr = [];
                        foreach (SP_DRVINSTALL_PARAMS item in Enum.GetValues(typeof(SP_DRVINSTALL_PARAMS)))
                            if (((int)item & (uint)propertyValue) != 0)
                                dr.Add(item);
                        property.SetValue(baseProperties, dr);
                    }
                    else if (property.Name == propertyName)
                    {
                        property.SetValue(baseProperties, propertyValue);
                    }
                }
            }
        }
        else
        {
            PropertyInfo? otherProperty = baseProperties.GetType().GetProperty("OtherProperties");
            if (otherProperty is not null)
            {
                var oldValue = otherProperty.GetValue(baseProperties);
                if (oldValue != null)
                {
                    ((SortedList<string, object>)oldValue).Add(itemPropKey.ToString(), propertyValue);
                    otherProperty.SetValue(baseProperties, oldValue);
                }
            }
        }
    }
}