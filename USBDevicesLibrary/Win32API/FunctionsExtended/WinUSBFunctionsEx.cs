using Microsoft.Win32.SafeHandles;
using System.ComponentModel;
using System.Runtime.InteropServices;
using USBDevicesLibrary.USBDevices;
using static USBDevicesLibrary.Win32API.USBSpec;
using static USBDevicesLibrary.Win32API.WinUSBData;
using static USBDevicesLibrary.Win32API.WinUSBIO;

namespace USBDevicesLibrary.Win32API;

public static partial class WinUSBFunctions
{
    public static Win32ResponseDataStruct WinUsbInitialize(SafeFileHandle deviceHandle)
    {
        Win32ResponseDataStruct bResponse = new();
        IntPtr interfaceHandle = new();
        bool isSuccess = WinUsb_Initialize( deviceHandle, out interfaceHandle);
        if (isSuccess)
        {
            bResponse.Status = true;
            bResponse.Data = interfaceHandle;
        }
        else
        {
            bResponse.Status = false;
            bResponse.Exception = new Win32Exception(Marshal.GetLastWin32Error());
            bResponse.ErrorFunctionName = $"WinUsb_Initialize [{interfaceHandle}]";
        }
        return bResponse;
    }

    public static Win32ResponseDataStruct GetDeviceDescriptor(IntPtr interfaceHandle, byte index, ushort languageID)
    {
        Win32ResponseDataStruct bResponse = new();
        uint bufferLength = (uint)Marshal.SizeOf(typeof(USB_DEVICE_DESCRIPTOR));
        bool isSuccess = WinUsb_GetDescriptor(interfaceHandle, (byte)DescriptorTypes.USB_DEVICE_DESCRIPTOR_TYPE, index, languageID, out USB_DEVICE_DESCRIPTOR usbDeviceDescriptor, bufferLength, out uint lengthTransferred);
        if (isSuccess)
        {
            bResponse.Status = true;
            bResponse.Data = usbDeviceDescriptor;
            bResponse.LengthTransferred = lengthTransferred;
        }
        else
        {
            bResponse.Status = false;
            bResponse.Exception = new Win32Exception(Marshal.GetLastWin32Error());
            bResponse.ErrorFunctionName = $"WinUsb_GetDescriptor [{DescriptorTypes.USB_DEVICE_DESCRIPTOR_TYPE}]";
        }
        return bResponse;
    }

    public static Win32ResponseDataStruct GetConfigurationDescriptor(SafeFileHandle interfaceHandle, byte index, ushort languageID)
    {
        Win32ResponseDataStruct bResponse = new();
        uint bufferLength = (uint)Marshal.SizeOf(typeof(USB_CONFIGURATION_DESCRIPTOR));
        bool isSuccess = WinUsb_GetDescriptor(interfaceHandle, (byte)DescriptorTypes.USB_CONFIGURATION_DESCRIPTOR_TYPE, index, languageID, out USB_CONFIGURATION_DESCRIPTOR usbConfigurationDescriptor, bufferLength, out uint lengthTransferred);
        if (isSuccess)
        {
            bResponse.Status = true;
            bResponse.Data = usbConfigurationDescriptor;
            bResponse.LengthTransferred = lengthTransferred;
        }
        else
        {
            bResponse.Status = false;
            bResponse.Exception = new Win32Exception(Marshal.GetLastWin32Error());
            bResponse.ErrorFunctionName = $"WinUsb_GetDescriptor [{DescriptorTypes.USB_CONFIGURATION_DESCRIPTOR_TYPE}]";
        }
        return bResponse;
    }

    public static Win32ResponseDataStruct GetStringDescriptor(SafeFileHandle interfaceHandle, byte index, ushort languageID)
    {
        Win32ResponseDataStruct bResponse = new();
        uint bufferLength = 256;
        bool isSuccess = WinUsb_GetDescriptor(interfaceHandle, (byte)DescriptorTypes.USB_STRING_DESCRIPTOR_TYPE, index, languageID, out USB_STRING_DESCRIPTOR usbStringDescriptor, bufferLength, out uint lengthTransferred);
        if (isSuccess)
        {
            bResponse.Status = true;
            bResponse.Data = usbStringDescriptor;
            bResponse.LengthTransferred = lengthTransferred;
        }
        else
        {
            bResponse.Status = false;
            bResponse.Exception = new Win32Exception(Marshal.GetLastWin32Error());
            bResponse.ErrorFunctionName = $"WinUsb_GetDescriptor [{DescriptorTypes.USB_STRING_DESCRIPTOR_TYPE}]";
        }
        return bResponse;
    }

    public static Win32ResponseDataStruct GetDeviceSpeed(IntPtr interfaceHandle)
    {
        Win32ResponseDataStruct bResponse = new();
        uint BufferLength = 1;
        bool isSuccess = WinUSBFunctions.WinUsb_QueryDeviceInformation(interfaceHandle, DEVICE_SPEED, ref BufferLength, out byte deviceSpeed);
        if (isSuccess)
        {
            bResponse.Status = true;
            bResponse.Data = (DeviceSpeeds)deviceSpeed;
        }
        else
        {
            bResponse.Status = false;
            bResponse.Exception = new Win32Exception(Marshal.GetLastWin32Error());
            bResponse.ErrorFunctionName = $"WinUsb_QueryDeviceInformation [{nameof(DEVICE_SPEED)}]";
        }
        return bResponse;
    }

    public static USBEndpointDirection GetPipeDirection(byte pipID)
    {
        return ((pipID & (byte)USB_ENDPOINT_DIRECTION_MASK) != 0) ? USBEndpointDirection.DIRECTION_IN : USBEndpointDirection.DIRECTION_OUT;
    }

    public static Win32ResponseDataStruct GetPipePolicy(SafeFileHandle winUSBInterfaceHandle, byte pipeID, PIPE_POLICY_TYPES policyType)
    {
        Win32ResponseDataStruct bResponse = new();
        bool isSuccess = true;
        uint boolValueLength = sizeof(bool);
        uint uintValueLength = sizeof(uint);
        byte boolValue = 0;
        uint uintValue = 0;
        USBEndpointDirection pipeDirection = GetPipeDirection(pipeID);
        switch (policyType)
        {
            // 0x01 SHORT_PACKET_TERMINATE
            case PIPE_POLICY_TYPES.SHORT_PACKET_TERMINATE:
                if (pipeDirection == USBEndpointDirection.DIRECTION_OUT)
                    isSuccess = WinUsb_GetPipePolicy(winUSBInterfaceHandle, pipeID, (uint)PIPE_POLICY_TYPES.SHORT_PACKET_TERMINATE,ref boolValueLength, out boolValue);
                break;
            // 0x02 AUTO_CLEAR_STALL
            case PIPE_POLICY_TYPES.AUTO_CLEAR_STALL:
                if (pipeDirection == USBEndpointDirection.DIRECTION_IN)
                    isSuccess = WinUsb_GetPipePolicy(winUSBInterfaceHandle, pipeID, (uint)PIPE_POLICY_TYPES.AUTO_CLEAR_STALL, ref boolValueLength, out boolValue);
                break;
            // 0x03 PIPE_TRANSFER_TIMEOUT
            case PIPE_POLICY_TYPES.PIPE_TRANSFER_TIMEOUT:
                isSuccess = WinUsb_GetPipePolicy(winUSBInterfaceHandle, pipeID, (uint)PIPE_POLICY_TYPES.PIPE_TRANSFER_TIMEOUT, ref uintValueLength, out uintValue);
                break;
            // 0x04 IGNORE_SHORT_PACKETS
            case PIPE_POLICY_TYPES.IGNORE_SHORT_PACKETS:
                if (pipeDirection == USBEndpointDirection.DIRECTION_IN)
                    isSuccess = WinUsb_GetPipePolicy(winUSBInterfaceHandle, pipeID, (uint)PIPE_POLICY_TYPES.IGNORE_SHORT_PACKETS, ref boolValueLength, out boolValue);
                break;
            // 0x05 ALLOW_PARTIAL_READS
            case PIPE_POLICY_TYPES.ALLOW_PARTIAL_READS:
                if (pipeDirection == USBEndpointDirection.DIRECTION_IN)
                    isSuccess = WinUsb_GetPipePolicy(winUSBInterfaceHandle, pipeID, (uint)PIPE_POLICY_TYPES.ALLOW_PARTIAL_READS, ref boolValueLength, out boolValue);
                break;
            // 0x06 AUTO_FLUSH
            case PIPE_POLICY_TYPES.AUTO_FLUSH:
                if (pipeDirection == USBEndpointDirection.DIRECTION_IN)
                    isSuccess = WinUsb_GetPipePolicy(winUSBInterfaceHandle, pipeID, (uint)PIPE_POLICY_TYPES.AUTO_FLUSH, ref boolValueLength, out boolValue);
                break;
            // 0x07 RAW_IO
            case PIPE_POLICY_TYPES.RAW_IO:
                if (pipeDirection == USBEndpointDirection.DIRECTION_IN)
                    isSuccess = WinUsb_GetPipePolicy(winUSBInterfaceHandle, pipeID, (uint)PIPE_POLICY_TYPES.RAW_IO, ref boolValueLength, out boolValue);
                break;
            // 0x08 MAXIMUM_TRANSFER_SIZE !!! READ ONLY !!!
            case PIPE_POLICY_TYPES.MAXIMUM_TRANSFER_SIZE:
                isSuccess = WinUsb_GetPipePolicy(winUSBInterfaceHandle, pipeID, (uint)PIPE_POLICY_TYPES.MAXIMUM_TRANSFER_SIZE, ref uintValueLength, out uintValue);
                break;
            // 0x09 RESET_PIPE_ON_RESUME
            case PIPE_POLICY_TYPES.RESET_PIPE_ON_RESUME:
                isSuccess = WinUsb_GetPipePolicy(winUSBInterfaceHandle, pipeID, (uint)PIPE_POLICY_TYPES.RESET_PIPE_ON_RESUME, ref boolValueLength, out boolValue);
                break;
        }
        if (isSuccess)
        {
            bResponse.Status = true;
            if (policyType== PIPE_POLICY_TYPES.MAXIMUM_TRANSFER_SIZE || policyType == PIPE_POLICY_TYPES.PIPE_TRANSFER_TIMEOUT)
            {
                bResponse.Data = uintValue;
            }
            else
            {
                bResponse.Data = boolValue;
            }
        }
        else
        {
            bResponse.Status = false;
            bResponse.Exception = new Win32Exception(Marshal.GetLastWin32Error());
            bResponse.ErrorFunctionName = $"WinUsb_GetPipePolicy [{policyType}]";
        }
        return bResponse;
    }

    public static Win32ResponseDataStruct SetPipePolicy(SafeFileHandle winUSBInterfaceHandle, byte pipeID, PIPE_POLICY_TYPES policyType, object value)
    {
        Win32ResponseDataStruct bResponse = new();
        bool isSuccess = true;
        uint boolValueLength = sizeof(bool);
        uint uintValueLength = sizeof(uint);
        USBEndpointDirection pipeDirection = GetPipeDirection(pipeID);
        switch (policyType)
        {
            // 0x01 SHORT_PACKET_TERMINATE
            case PIPE_POLICY_TYPES.SHORT_PACKET_TERMINATE:
                if (pipeDirection == USBEndpointDirection.DIRECTION_OUT)
                {
                    if (value.GetType() == typeof(bool))
                    {
                        byte shortPacketTerminate = Convert.ToByte(value);
                        isSuccess = WinUsb_SetPipePolicy(winUSBInterfaceHandle, pipeID, (uint)PIPE_POLICY_TYPES.SHORT_PACKET_TERMINATE, boolValueLength, shortPacketTerminate);
                    }
                }
                break;
            // 0x02 AUTO_CLEAR_STALL
            case PIPE_POLICY_TYPES.AUTO_CLEAR_STALL:
                if (pipeDirection == USBEndpointDirection.DIRECTION_IN)
                {
                    if (value.GetType() == typeof(bool))
                    {
                        byte autoClearStall = Convert.ToByte(value);
                        isSuccess = WinUsb_SetPipePolicy(winUSBInterfaceHandle, pipeID, (uint)PIPE_POLICY_TYPES.AUTO_CLEAR_STALL, boolValueLength, autoClearStall);
                    }
                }
                break;
            // 0x03 PIPE_TRANSFER_TIMEOUT
            case PIPE_POLICY_TYPES.PIPE_TRANSFER_TIMEOUT:
                if (value.GetType() == typeof(uint))
                {
                    uint pipeTransferTimeOut = (uint)value;
                    isSuccess = WinUsb_SetPipePolicy(winUSBInterfaceHandle, pipeID, (uint)PIPE_POLICY_TYPES.PIPE_TRANSFER_TIMEOUT, uintValueLength, pipeTransferTimeOut);
                }
                break;
            // 0x04 IGNORE_SHORT_PACKETS
            case PIPE_POLICY_TYPES.IGNORE_SHORT_PACKETS:
                if (pipeDirection == USBEndpointDirection.DIRECTION_IN)
                {
                    if (value.GetType() == typeof(bool))
                    {
                        byte ignoreShortPackets = Convert.ToByte(value);
                        isSuccess = WinUsb_SetPipePolicy(winUSBInterfaceHandle, pipeID, (uint)PIPE_POLICY_TYPES.IGNORE_SHORT_PACKETS, boolValueLength, ignoreShortPackets);
                    }
                }
                break;
            // 0x05 ALLOW_PARTIAL_READS
            case PIPE_POLICY_TYPES.ALLOW_PARTIAL_READS:
                if (pipeDirection == USBEndpointDirection.DIRECTION_IN)
                {
                    if (value.GetType() == typeof(bool))
                    {
                        byte allowPartialReads = Convert.ToByte(value);
                        isSuccess = WinUsb_SetPipePolicy(winUSBInterfaceHandle, pipeID, (uint)PIPE_POLICY_TYPES.ALLOW_PARTIAL_READS, boolValueLength, allowPartialReads);
                    }
                }
                break;
            // 0x06 AUTO_FLUSH
            case PIPE_POLICY_TYPES.AUTO_FLUSH:
                if (pipeDirection == USBEndpointDirection.DIRECTION_IN)
                {
                    if (value.GetType() == typeof(bool))
                    {
                        byte autoFlush = Convert.ToByte(value);
                        isSuccess = WinUsb_SetPipePolicy(winUSBInterfaceHandle, pipeID, (uint)PIPE_POLICY_TYPES.AUTO_FLUSH, boolValueLength, autoFlush);
                    }
                }
                break;
            // 0x07 RAW_IO
            case PIPE_POLICY_TYPES.RAW_IO:
                if (pipeDirection == USBEndpointDirection.DIRECTION_IN)
                {
                    if (value.GetType() == typeof(bool))
                    {
                        byte rawIO = Convert.ToByte(value);
                        isSuccess = WinUsb_SetPipePolicy(winUSBInterfaceHandle, pipeID, (uint)PIPE_POLICY_TYPES.RAW_IO, boolValueLength, rawIO);
                    }
                }
                break;
            // 0x08 MAXIMUM_TRANSFER_SIZE !!! READ ONLY !!!
            case PIPE_POLICY_TYPES.MAXIMUM_TRANSFER_SIZE:
                if (value.GetType() == typeof(uint))
                {
                    uint maximumTransferSize = (uint)value;
                    isSuccess = WinUsb_SetPipePolicy(winUSBInterfaceHandle, pipeID, (uint)PIPE_POLICY_TYPES.MAXIMUM_TRANSFER_SIZE, uintValueLength, maximumTransferSize);
                }
                break;
            // 0x09 RESET_PIPE_ON_RESUME
            case PIPE_POLICY_TYPES.RESET_PIPE_ON_RESUME:
                if (value.GetType() == typeof(bool))
                {
                    byte resetPipeOnResume = Convert.ToByte(value);
                    isSuccess = WinUsb_SetPipePolicy(winUSBInterfaceHandle, pipeID, (uint)PIPE_POLICY_TYPES.RESET_PIPE_ON_RESUME, boolValueLength, resetPipeOnResume);
                }
                break;
        }
        if (isSuccess)
        {
            bResponse.Status = true;
        }
        else
        {
            bResponse.Status = false;
            bResponse.Exception = new Win32Exception(Marshal.GetLastWin32Error());
            bResponse.ErrorFunctionName = $"WinUsb_SetPipePolicy [{policyType}]";
        }
        return bResponse;
    }

    public static Win32ResponseDataStruct SendDatatoDefaultEndpoint(SafeFileHandle interfaceHandle, byte requestType, byte request, ushort value, ushort index, ushort length)
    {
        Win32ResponseDataStruct bResponse = new();

        byte[] data = new byte[length];
        WINUSB_SETUP_PACKET SetupPacket;

        //Create the setup packet
        // http://www.poweredusb.org/pdf/usb20.pdf
        /*
        Characteristics of request:
        D7: Data transfer direction
        0 = Host - to - device
        1 = Device - to - host
        D6...5: Type
        0 = Standard
        1 = Class
        2 = Vendor
        3 = Reserved
        D4...0: Recipient
        0 = Device
        1 = Interface
        2 = Endpoint
        3 = Other
        4...31 = Reserved
        */
        SetupPacket.RequestType = requestType;
        // Specific request (refer to Table 9-3)
        SetupPacket.Request = request;
        // Word-sized field that varies according to request
        SetupPacket.Value = value;
        // Word-sized field that varies according to request; typically used to pass an index or offset
        SetupPacket.Index = index;
        // Number of bytes to transfer if there is a Data stage
        SetupPacket.Length = length;

        bool isSuccess = WinUsb_ControlTransfer(interfaceHandle, SetupPacket, data, length, out uint lengthTransferred, 0);
        if (isSuccess)
        {
            bResponse.Status = true;
            bResponse.Data = data;
            bResponse.LengthTransferred = lengthTransferred;
        }
        else
        {
            bResponse.Status = false;
            bResponse.Exception = new Win32Exception(Marshal.GetLastWin32Error());
            bResponse.ErrorFunctionName = $"WinUsb_ControlTransfer [{interfaceHandle}]";
        }

        return bResponse;
    }

    public static Win32ResponseDataStruct WriteToBulkEndpoint(SafeFileHandle winUSBInterfaceHandle, byte pipeID, byte[] data, uint bufferSize)
    {
        Win32ResponseDataStruct bResponse = new();
        byte[] txData = data;
        bool isSuccess = WinUsb_WritePipe(winUSBInterfaceHandle, pipeID, txData, bufferSize, out uint lengthTransferred, IntPtr.Zero);
        if (isSuccess)
        {
            bResponse.Status = true;
            bResponse.Data = txData;
            bResponse.LengthTransferred = lengthTransferred;
        }
        else
        {
            bResponse.Status = false;
            bResponse.Exception = new Win32Exception(Marshal.GetLastWin32Error());
            bResponse.ErrorFunctionName = $"WinUsb_WritePipe [{pipeID}]";
        }
        return bResponse;
    }

    public static Win32ResponseDataStruct ReadFromBulkEndpoint(SafeFileHandle interfaceHandle, SafeFileHandle winUSBInterfaceHandle, byte pipeID, uint bufferSize)
    {
        Win32ResponseDataStruct bResponse = new();
        byte[] data = new byte[bufferSize];
        for (int i = 0; i < data.Length; i++)
            data[i] = 0;

        bool isSuccess = WinUsb_ReadPipe(winUSBInterfaceHandle, pipeID, data, bufferSize, out uint lengthTransferred, 0);
        if (isSuccess)
        {
            bResponse.Status = true;
            bResponse.Data = data;
            bResponse.LengthTransferred = lengthTransferred;
        }
        else
        {
            bResponse.Status = false;
            bResponse.Exception = new Win32Exception(Marshal.GetLastWin32Error());
            bResponse.ErrorFunctionName = $"WinUsb_ReadPipe [{pipeID}]";
        }
        return bResponse;
    }

    public static Win32ResponseDataStruct GetAssociatedInterface(IntPtr interfaceHandle, byte numberOfInterfaces)
    {
        Win32ResponseDataStruct bResponse = new();
        bool isSuccess = false;
        List<IntPtr> winUSB_MI_Handles = [];
        winUSB_MI_Handles.Add(interfaceHandle);
        for (byte i = 0; i < numberOfInterfaces; i++)
        {
            bool issss = WinUSBFunctions.WinUsb_QueryInterfaceSettings(interfaceHandle, i, out USBSpec.USB_INTERFACE_DESCRIPTOR UsbAltInterfaceDescriptor);
            isSuccess = WinUsb_GetAssociatedInterface(interfaceHandle, 4, out IntPtr associatedInterfaceHandle);
            if (isSuccess)
                winUSB_MI_Handles.Add(associatedInterfaceHandle);
            else
                break;
        }
        if (isSuccess)
        {
            bResponse.Status = true;
            bResponse.Data = winUSB_MI_Handles;
        }
        else
        {
            bResponse.Status = false;
            bResponse.Exception = new Win32Exception(Marshal.GetLastWin32Error());
            bResponse.ErrorFunctionName = $"GetAssociatedInterface [{winUSB_MI_Handles.Count}]";
        }
        return bResponse;
    }

}
