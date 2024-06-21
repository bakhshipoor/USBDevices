using Microsoft.Win32.SafeHandles;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
using USBDevicesLibrary.Devices;
using USBDevicesLibrary.Interfaces.Storage;
using USBDevicesLibrary.USBDevices;
using USBDevicesLibrary.Win32API;
using static USBDevicesLibrary.Win32API.ClassesGUID;
using static USBDevicesLibrary.Win32API.Kernel32Data;
using static USBDevicesLibrary.Win32API.NTDDDiskData;
using static USBDevicesLibrary.Win32API.NTDDStorData;
using static USBDevicesLibrary.Win32API.SetupAPIData;
using static USBDevicesLibrary.Win32API.USB;
using static USBDevicesLibrary.Win32API.USBIOCtl;
using static USBDevicesLibrary.Win32API.USBSpec;
using static USBDevicesLibrary.Win32API.WinUSBData;

public static class USBDevicesListHelpers
{
    public static bool ConnectedEventStatus { get; set; }
    public static bool DisconnectedEventStatus { get; set; }
    public static bool ModifiedEventStatus { get; set; }
    public static bool FilterDeviceStatus { get; set; }
    public static bool CheckInterfacesStatus { get; set; }
    public static USBDevicesFilter USBDevicesFilterData { get; set; } = new();

    public static List<HUB_CHARACHTERISTICS> ExtractHubCharachteristics(ushort charachteristics)
    {
        List<HUB_CHARACHTERISTICS> bResponse = [];
        int ch = 0;
        ch = charachteristics & (int)HUB_CHARACHTERISTICS_Mask.LOGICAL_POWER_SWITCHING_MODE;
        if (ch == 0 || ch == 2) bResponse.Add(HUB_CHARACHTERISTICS.GANGED_POWER_SWITCHING);
        else if (ch == 1 || ch == 3) bResponse.Add(HUB_CHARACHTERISTICS.INDIVIDUAL_PORT_POWER_SWITCHING);
        if (ch > 1) bResponse.Add(HUB_CHARACHTERISTICS.NO_POWER_SWITCHING);

        ch = charachteristics & (int)HUB_CHARACHTERISTICS_Mask.IDENTIFIES_COMPOUND_DEVICE;
        if (ch == 0) bResponse.Add(HUB_CHARACHTERISTICS.HUB_IS_NOT_PART_OF_COMPOUND_DEVICE);
        else bResponse.Add(HUB_CHARACHTERISTICS.HUB_IS_PART_OF_COMPOUND_DEVICE);

        ch = charachteristics & (int)HUB_CHARACHTERISTICS_Mask.OVER_CURRENT_PROTECTION_MODE;
        if (ch == 0 || ch == 16) bResponse.Add(HUB_CHARACHTERISTICS.GLOBAL_OVER_CURRENT_PROTECTION);
        else if (ch == 8 || ch == 48) bResponse.Add(HUB_CHARACHTERISTICS.INDIVIDUAL_PORT_OVER_CURRENT_PROTECTION);
        if (ch > 8) bResponse.Add(HUB_CHARACHTERISTICS.NO_OVER_CURRENT_PROTECTION);

        ch = charachteristics & (int)HUB_CHARACHTERISTICS_Mask.TT_THINK_TIME;
        if (ch == 0) bResponse.Add(HUB_CHARACHTERISTICS.TT_REQUIRES_AT_MOST_8_FS_BIT_TIMES);
        else if (ch == 32) bResponse.Add(HUB_CHARACHTERISTICS.TT_REQUIRES_AT_MOST_16_FS_BIT_TIMES);
        else if (ch == 64) bResponse.Add(HUB_CHARACHTERISTICS.TT_REQUIRES_AT_MOST_24_FS_BIT_TIMES);
        else if (ch == 96) bResponse.Add(HUB_CHARACHTERISTICS.TT_REQUIRES_AT_MOST_32_FS_BIT_TIMES);

        ch = charachteristics & (int)HUB_CHARACHTERISTICS_Mask.PORT_INDICATORS_SUPPORTED;
        if (ch == 0) bResponse.Add(HUB_CHARACHTERISTICS.PORT_INDICATORS_ARE_NOT_SUPPORTED);
        else bResponse.Add(HUB_CHARACHTERISTICS.PORT_INDICATORS_ARE_SUPPORTED);

        return bResponse;
    }

    public static USB_DEVICE_CHARACTERISTICS GetDeviceCharacteristics(string devicePath)
    {
        USB_DEVICE_CHARACTERISTICS bResponse = new();
        Win32ResponseDataStruct deviceCharacteristicsHandle = Kernel32Functions.CreateDeviceHandle(devicePath);
        if (deviceCharacteristicsHandle.Status)
        {
            USB_DEVICE_CHARACTERISTICS deviceCharacteristics = new()
            {
                Version = 0x01 // USB_DEVICE_CHARACTERISTICS_VERSION_1
            };
            Win32ResponseDataStruct decviceIOControl = Kernel32Functions.GetDeviceIoControl((SafeFileHandle)deviceCharacteristicsHandle.Data, deviceCharacteristics, USB_IOCTL[USB_IOCTL_Enum.IOCTL_USB_GET_DEVICE_CHARACTERISTICS]);
            if (decviceIOControl.Status)
            {
                bResponse = ((USB_DEVICE_CHARACTERISTICS)decviceIOControl.Data);
            }
            ((SafeFileHandle)deviceCharacteristicsHandle.Data).Close();
        }
        return bResponse;
    }

    public static USB_HUB_CAP_FLAGS GetHubCapabilities(string devicePath)
    {
        USB_HUB_CAP_FLAGS bResponse = 0;
        Win32ResponseDataStruct controllerHandle = Kernel32Functions.CreateDeviceHandle(devicePath);
        if (controllerHandle.Status)
        {
            USB_HUB_CAPABILITIES_EX rootHubName = new();
            Win32ResponseDataStruct decviceIOControl = Kernel32Functions.GetDeviceIoControl((SafeFileHandle)controllerHandle.Data, rootHubName, USB_IOCTL[USB_IOCTL_Enum.IOCTL_USB_GET_HUB_CAPABILITIES_EX]);
            if (decviceIOControl.Status)
            {
                bResponse = (USB_HUB_CAP_FLAGS)((USB_HUB_CAPABILITIES_EX)decviceIOControl.Data).CapabilityFlags;
            }
            ((SafeFileHandle)controllerHandle.Data).Close();
        }
        return bResponse;
    }

    public static USB_NODE_CONNECTION_NAME GetNodeConnectionName(string devicePath, uint connectionIndex)
    {
        USB_NODE_CONNECTION_NAME bResponse = new();
        Win32ResponseDataStruct nodeConnectionNameHandle = Kernel32Functions.CreateDeviceHandle(devicePath);
        if (nodeConnectionNameHandle.Status)
        {
            USB_NODE_CONNECTION_NAME nodeConnectionName = new()
            {
                ConnectionIndex = connectionIndex
            };
            Win32ResponseDataStruct decviceIOControl = Kernel32Functions.GetDeviceIoControl((SafeFileHandle)nodeConnectionNameHandle.Data,
                nodeConnectionName, USB_IOCTL[USB_IOCTL_Enum.IOCTL_USB_GET_NODE_CONNECTION_NAME]);
            if (decviceIOControl.Status)
            {
                bResponse = ((USB_NODE_CONNECTION_NAME)decviceIOControl.Data);
            }
            ((SafeFileHandle)nodeConnectionNameHandle.Data).Close();
        }
        return bResponse;
    }

    public static USB_NODE_CONNECTION_ATTRIBUTES GetNodeConnectionAttributes(string devicePath, uint connectionIndex)
    {
        USB_NODE_CONNECTION_ATTRIBUTES bResponse = new();
        Win32ResponseDataStruct nodeConnectionAttributesHandle = Kernel32Functions.CreateDeviceHandle(devicePath);
        if (nodeConnectionAttributesHandle.Status)
        {
            USB_NODE_CONNECTION_ATTRIBUTES nodeConnectionAttributes = new()
            {
                ConnectionIndex = connectionIndex
            };
            Win32ResponseDataStruct decviceIOControl = Kernel32Functions.GetDeviceIoControl((SafeFileHandle)nodeConnectionAttributesHandle.Data,
                nodeConnectionAttributes, USB_IOCTL[USB_IOCTL_Enum.IOCTL_USB_GET_NODE_CONNECTION_ATTRIBUTES]);
            if (decviceIOControl.Status)
            {
                bResponse = ((USB_NODE_CONNECTION_ATTRIBUTES)decviceIOControl.Data);
            }
            ((SafeFileHandle)nodeConnectionAttributesHandle.Data).Close();
        }
        return bResponse;
    }

    public static USB_CONFIGURATION_DESCRIPTOR GetNodeConnectionConfigurationDescriptor(string devicePath, uint connectionIndex, byte configurationIndex)
    {
        USB_CONFIGURATION_DESCRIPTOR bResponse = new();
        WINUSB_SETUP_PACKET setupPacket = new()
        {
            Request = (byte)RequestType_DirectionFlags.DataTransferDirectionDeviceToHost,
            RequestType = (byte)StandardRequestCodes.GET_DESCRIPTOR,
            Value = (ushort)(((ushort)USBDescriptorTypes.CONFIGURATION << 8) | configurationIndex),
            Index = 0,
            Length = (ushort)Marshal.SizeOf(typeof(USB_CONFIGURATION_DESCRIPTOR)),
        };
        byte[] requestedDescriptor = GetNodeConnectionDescriptor(devicePath, connectionIndex, setupPacket);
        if (requestedDescriptor.Length == 0) return bResponse;
        IntPtr unmanagedPointer = Marshal.AllocHGlobal(requestedDescriptor.Length);
        Marshal.Copy(requestedDescriptor, 0, unmanagedPointer, requestedDescriptor.Length);
        bResponse = Marshal.PtrToStructure<USB_CONFIGURATION_DESCRIPTOR>(unmanagedPointer);
        Marshal.FreeHGlobal(unmanagedPointer);
        return bResponse;
    }

    public static byte[] GetNodeConnectionDescriptor(string devicePath, uint connectionIndex, WINUSB_SETUP_PACKET setupPaccket)
    {
        byte[] bResponse = [];
        Win32ResponseDataStruct nodeConnectionDescriptorHandle = Kernel32Functions.CreateDeviceHandle(devicePath);
        if (nodeConnectionDescriptorHandle.Status)
        {
            USB_DESCRIPTOR_REQUEST nodeConnectionDescriptor = new()
            {
                ConnectionIndex = connectionIndex,
                SetupPacket = setupPaccket
            };
            Win32ResponseDataStruct decviceIOControl = Kernel32Functions.GetDeviceIoControl((SafeFileHandle)nodeConnectionDescriptorHandle.Data,
                nodeConnectionDescriptor, USB_IOCTL[USB_IOCTL_Enum.IOCTL_USB_GET_DESCRIPTOR_FROM_NODE_CONNECTION]);
            if (decviceIOControl.Status)
            {
                bResponse = ((USB_DESCRIPTOR_REQUEST)decviceIOControl.Data).Data;
            }
            ((SafeFileHandle)nodeConnectionDescriptorHandle.Data).Close();
        }
        return bResponse;
    }

    public static USB_NODE_CONNECTION_INFORMATION_EX GetNodeConnectionInformation(string devicePath, uint connectionIndex)
    {
        USB_NODE_CONNECTION_INFORMATION_EX bResponse = new();
        Win32ResponseDataStruct nodeConnectionInformationHandle = Kernel32Functions.CreateDeviceHandle(devicePath);
        if (nodeConnectionInformationHandle.Status)
        {
            USB_NODE_CONNECTION_INFORMATION_EX nodeConnectionInformation = new()
            {
                ConnectionIndex = connectionIndex
            };
            Win32ResponseDataStruct decviceIOControl = Kernel32Functions.GetDeviceIoControl((SafeFileHandle)nodeConnectionInformationHandle.Data,
                nodeConnectionInformation, USB_IOCTL[USB_IOCTL_Enum.IOCTL_USB_GET_NODE_CONNECTION_INFORMATION_EX]);
            if (decviceIOControl.Status)
            {
                bResponse = ((USB_NODE_CONNECTION_INFORMATION_EX)decviceIOControl.Data);
            }
            ((SafeFileHandle)nodeConnectionInformationHandle.Data).Close();
        }
        return bResponse;
    }

    public static USB_NODE_CONNECTION_INFORMATION_EX_V2 GetNodeConnectionInformationV2(string devicePath, uint connectionIndex)
    {
        USB_NODE_CONNECTION_INFORMATION_EX_V2 bResponse = new();
        Win32ResponseDataStruct nodeConnectionInformationV2Handle = Kernel32Functions.CreateDeviceHandle(devicePath);
        if (nodeConnectionInformationV2Handle.Status)
        {
            USB_NODE_CONNECTION_INFORMATION_EX_V2 nodeConnectionInformationV2 = new()
            {
                // https://learn.microsoft.com/en-us/windows-hardware/drivers/ddi/usbioctl/ni-usbioctl-ioctl_usb_get_node_connection_information_ex_v2
                ConnectionIndex = connectionIndex,
                SupportedUsbProtocols = USB_PROTOCOLS.Usb300
            };
            nodeConnectionInformationV2.Length = (uint)Marshal.SizeOf(nodeConnectionInformationV2);
            Win32ResponseDataStruct decviceIOControl = Kernel32Functions.GetDeviceIoControl((SafeFileHandle)nodeConnectionInformationV2Handle.Data,
                nodeConnectionInformationV2, USB_IOCTL[USB_IOCTL_Enum.IOCTL_USB_GET_NODE_CONNECTION_INFORMATION_EX_V2]);
            if (decviceIOControl.Status)
            {
                bResponse = ((USB_NODE_CONNECTION_INFORMATION_EX_V2)decviceIOControl.Data);
            }
            ((SafeFileHandle)nodeConnectionInformationV2Handle.Data).Close();
        }
        return bResponse;
    }

    public static USB_INTERFACE_DESCRIPTOR GetNodeConnectionInterfaceDescriptor(string devicePath, uint connectionIndex, byte interfaceIndex)
    {
        USB_INTERFACE_DESCRIPTOR bResponse = new();
        WINUSB_SETUP_PACKET setupPacket = new()
        {
            Request = (byte)RequestType_DirectionFlags.DataTransferDirectionDeviceToHost,
            RequestType = (byte)StandardRequestCodes.GET_DESCRIPTOR,
            Value = (ushort)(((ushort)USBDescriptorTypes.INTERFACE << 8) | interfaceIndex),
            Index = 0,
            Length = (ushort)Marshal.SizeOf(typeof(USB_INTERFACE_DESCRIPTOR)),
        };
        byte[] requestedDescriptor = GetNodeConnectionDescriptor(devicePath, connectionIndex, setupPacket);
        if (requestedDescriptor.Length == 0) return bResponse;
        IntPtr unmanagedPointer = Marshal.AllocHGlobal(requestedDescriptor.Length);
        Marshal.Copy(requestedDescriptor, 0, unmanagedPointer, requestedDescriptor.Length);
        bResponse = Marshal.PtrToStructure<USB_INTERFACE_DESCRIPTOR>(unmanagedPointer);
        Marshal.FreeHGlobal(unmanagedPointer);
        return bResponse;
    }

    public static ushort GetDefaultLanguageID(string devicePath, uint connectionIndex)
    {
        ushort bResponse = 0;
        ushort numberOfLanguages = 0;
        List<ushort> languageIDs = [];
        WINUSB_SETUP_PACKET setupPacket = new()
        {
            Request = (byte)RequestType_DirectionFlags.DataTransferDirectionDeviceToHost,
            RequestType = (byte)StandardRequestCodes.GET_DESCRIPTOR,
            Value = (ushort)(((ushort)USBDescriptorTypes.STRING << 8) | 0),
            // If set 'stringIndex' and 'Index' to zero (0), device returend number of languages and ids in data
            // The first two byte not a language data. first byte (index[0]) contain the lenght of descriptor. to find number of languages should 
            // minus 2 from index[0] (first two bytes)
            // then devide 2 (size of language id).
            // Number Of Languages = ( index[0] - 2 ) / 2  
            // Language IDs comes after index[1]
            // Language id size is 2 bytes.
            // For example the first language id in index[2] and index[3]
            // index[2] is a low-byte and index[3] is a high-byte. 
            // LANGID = (index[3] << 8) | index[2]
            // The https://www.usb.org use Microsoft LANGID definitions
            // https://learn.microsoft.com/en-us/windows/win32/intl/language-identifier-constants-and-strings
            // https://learn.microsoft.com/en-us/openspecs/windows_protocols/ms-lcid
            // https://winprotocoldoc.blob.core.windows.net/productionwindowsarchives/MS-LCID/%5bMS-LCID%5d.pdf
            // Page 14 : 0x0409 en-US
            Index = 0,
            Length = 0xFF,
        };
        byte[] requestedDescriptor = GetNodeConnectionDescriptor(devicePath, connectionIndex, setupPacket);
        if (requestedDescriptor == null || requestedDescriptor.Length < 5) return bResponse;
        numberOfLanguages = (ushort)((requestedDescriptor[0] - 2) / 2);
        if (numberOfLanguages < 1) return bResponse;
        for (int indexofLangID = 2; indexofLangID < requestedDescriptor[0]; indexofLangID++)
        {
            ushort langID = requestedDescriptor[indexofLangID];
            indexofLangID++;
            langID |= (ushort)(requestedDescriptor[indexofLangID] << 8);
            languageIDs.Add(langID);
        }
        if (languageIDs.Count > 0)
            bResponse = languageIDs[0];
        return bResponse;
    }

    public static string[] GetNodeConnectionStringDescriptor(string devicePath, uint connectionIndex, byte stringIndex)
    {
        string[] bResponse = ["0", "Unknown"];
        ushort defaultLangID = GetDefaultLanguageID(devicePath, connectionIndex);
        WINUSB_SETUP_PACKET setupPacket = new()
        {
            Request = (byte)RequestType_DirectionFlags.DataTransferDirectionDeviceToHost,
            RequestType = (byte)StandardRequestCodes.GET_DESCRIPTOR,
            Value = (ushort)(((ushort)USBDescriptorTypes.STRING << 8) | stringIndex),
            Index = defaultLangID, // LANGID (Language Identifiers)
            Length = 0xFF, // Maximum Lenght of string descriptor
        };
        byte[] requestedDescriptor = GetNodeConnectionDescriptor(devicePath, connectionIndex, setupPacket);
        if (requestedDescriptor.Length < 2) return bResponse;
        // The first two byte of descriptor not a string data. the first byte (index [0]) of descriptor contain the length of a descriptor.
        // descritor[0] - 2 = string bytes count.  2 means first two bytes.
        if (requestedDescriptor[0] < 2) return bResponse;
        bResponse[0] = (requestedDescriptor[0] - 2).ToString();
        IntPtr unmanagedPointer = Marshal.AllocHGlobal(requestedDescriptor.Length);
        Marshal.Copy(requestedDescriptor, 0, unmanagedPointer, requestedDescriptor.Length);
        string? str = Marshal.PtrToStringUni(unmanagedPointer);
        Marshal.FreeHGlobal(unmanagedPointer);
        if (!string.IsNullOrEmpty(str))
        {
            // Remove first charachter (explained above).
            str = str.Remove(0, 1);
            // If string have a control charachters, Then converted to hex string
            // ASCII Table : https://www.asciitable.com/ (Hex)0 - (Hex)1F
            // UniCode: https://www.unicode.org/charts/PDF/U0000.pdf Page 3: C0 controls 0000 - 001F
            if (str.ToCharArray().Any(c => c < 0x20))
            {
                byte[] strByte = Encoding.ASCII.GetBytes(str);
                bResponse[1] = Convert.ToHexString(strByte).ToUpper();
            }
            else
                bResponse[1] = str;
        }
        return bResponse;
    }

    public static USB_NODE_INFORMATION GetNodeInformation(string devicePath)
    {
        USB_NODE_INFORMATION bResponse = new();
        Win32ResponseDataStruct nodeInformationHandle = Kernel32Functions.CreateDeviceHandle(devicePath);
        if (nodeInformationHandle.Status)
        {
            USB_NODE_INFORMATION usbNodeInfo = new()
            {
                NodeType = USB_HUB_NODE.UsbHub
            };
            Win32ResponseDataStruct decviceIOControl = Kernel32Functions.GetDeviceIoControl((SafeFileHandle)nodeInformationHandle.Data, usbNodeInfo, USB_IOCTL[USB_IOCTL_Enum.IOCTL_USB_GET_NODE_INFORMATION]);
            if (decviceIOControl.Status)
            {
                bResponse = (USB_NODE_INFORMATION)decviceIOControl.Data;
            }
            ((SafeFileHandle)nodeInformationHandle.Data).Close();
        }
        return bResponse;
    }

    public static string GetRootHubName(string devicePath)
    {
        string bResponse = string.Empty;
        Win32ResponseDataStruct controllerHandle = Kernel32Functions.CreateDeviceHandle(devicePath);
        if (controllerHandle.Status)
        {
            USB_ROOT_HUB_NAME rootHubName = new();
            Win32ResponseDataStruct decviceIOControl = Kernel32Functions.GetDeviceIoControl((SafeFileHandle)controllerHandle.Data, rootHubName, USB_IOCTL[USB_IOCTL_Enum.IOCTL_USB_GET_ROOT_HUB_NAME]);
            if (decviceIOControl.Status)
            {
                bResponse = ("\\\\?\\" + ((USB_ROOT_HUB_NAME)decviceIOControl.Data).RootHubName).ToLower();
            }
            ((SafeFileHandle)controllerHandle.Data).Close();
        }
        return bResponse;
    }

    public static void UpdateControllerCollection(ObservableCollection<USBController> usbControllers)
    {
        uint flags = (uint)(DIGCF.DIGCF_PRESENT | DIGCF.DIGCF_DEVICEINTERFACE);
        Guid deviceClassGuid = ClassGuid[GUID_DEVCLASS.GUID_DEVINTERFACE_USB_HOST_CONTROLLER];
        ObservableCollection<Device> usbControllersFromSetupAPI = DeviceHelpers.GetClassDevices(deviceClassGuid, string.Empty, flags);
        foreach (Device itemDevice in usbControllersFromSetupAPI)
        {
            USBController controller = new(/*itemDevice*/)
            {
                DevicePath = itemDevice.DevicePath
            };
            // Get Root Hub Name
            controller.RootHubName = GetRootHubName(controller.DevicePath);
            usbControllers.Add(controller);
        }
    }

    public static void UpdateHubCollection(ObservableCollection<USBHub> usbHubs)
    {
        uint flags = (uint)(DIGCF.DIGCF_PRESENT | DIGCF.DIGCF_DEVICEINTERFACE);
        Guid deviceClassGuid = ClassGuid[GUID_DEVCLASS.GUID_DEVINTERFACE_USB_HUB];
        ObservableCollection<Device> usbHubsFromSetupAPI = DeviceHelpers.GetClassDevices(deviceClassGuid, string.Empty, flags);
        foreach (Device itemDevice in usbHubsFromSetupAPI)
        {
            USBHub hub = new()
            {
                DevicePath = itemDevice.DevicePath,
                DeviceID = itemDevice.DeviceProperties.Device_InstanceId
            };
            // Node Information
            USB_NODE_INFORMATION nodeInfo = GetNodeInformation(hub.DevicePath);
            hub.HubCharacteristics = ExtractHubCharachteristics(nodeInfo.u.HubInformation.HubDescriptor.wHubCharacteristics);
            hub.NumberOfPorts = nodeInfo.u.HubInformation.HubDescriptor.bNumberOfPorts;
            hub.PowerOnToPowerGood = nodeInfo.u.HubInformation.HubDescriptor.bPowerOnToPowerGood;
            hub.HubControlCurrent = nodeInfo.u.HubInformation.HubDescriptor.bHubControlCurrent;
            USB_HUB_CAP_FLAGS hubCaps = GetHubCapabilities(hub.DevicePath);
            {
                if ((hubCaps & USB_HUB_CAP_FLAGS.HubIsHighSpeedCapable) != 0) hub.HubIsHighSpeedCapable = true;
                if ((hubCaps & USB_HUB_CAP_FLAGS.HubIsHighSpeed) != 0) hub.HubIsHighSpeed = true;
                if ((hubCaps & USB_HUB_CAP_FLAGS.HubIsMultiTtCapable) != 0) hub.HubIsMultiTtCapable = true;
                if ((hubCaps & USB_HUB_CAP_FLAGS.HubIsMultiTt) != 0) hub.HubIsMultiTt = true;
                if ((hubCaps & USB_HUB_CAP_FLAGS.HubIsRoot) != 0) hub.HubIsRoot = true;
                if ((hubCaps & USB_HUB_CAP_FLAGS.HubIsArmedWakeOnConnect) != 0) hub.HubIsArmedWakeOnConnect = true;
                if ((hubCaps & USB_HUB_CAP_FLAGS.HubIsBusPowered) != 0) hub.HubIsBusPowered = true;
            }
            usbHubs.Add(hub);
        }
    }

    public static void UpdateUSBDevicesCollection(ObservableCollection<USBHub> usbHubs, ObservableCollection<Device> usbDevicesFromSetupAPI, ObservableCollection<USBDevice> usbDevices)
    {
        foreach (Device itemDevice in usbDevicesFromSetupAPI)
        {
            usbDevices.Add(CreateUSBDevice(usbHubs, itemDevice));
        }
    }

    public static USBDevice CreateUSBDevice(ObservableCollection<USBHub> usbHubs, Device device)
    {
        USBDevice usbDevice = new(device);
        if (!string.IsNullOrEmpty(usbDevice.BaseDeviceProperties.Device_LocationInfo))
        {
            string[] location = usbDevice.BaseDeviceProperties.Device_LocationInfo.Split(".");
            if (location.Length > 0 && !string.IsNullOrEmpty(location[0]))
            {
                if (location[0].Contains("port_#", StringComparison.OrdinalIgnoreCase))
                {
                    usbDevice.PortIndex = Convert.ToUInt32(location[0].Replace("port_#", "", StringComparison.OrdinalIgnoreCase));
                }
            }
        }
        foreach (USBHub itemUSBHUB in usbHubs)
        {
            if (usbDevice.BaseDeviceProperties.Device_Parent.Equals(itemUSBHUB.DeviceID, StringComparison.OrdinalIgnoreCase))
            {
                usbDevice.DevicePath_ParentHub = itemUSBHUB.DevicePath;
            }
        }
        uint connectionIndex = usbDevice.PortIndex;
        string devicePath = usbDevice.DevicePath_ParentHub;

        // Fill Data From USB_NODE_CONNECTION_INFORMATION_EX
        USB_NODE_CONNECTION_INFORMATION_EX nodeConnectionInformation = GetNodeConnectionInformation(devicePath, connectionIndex);
        {
            usbDevice.Rev_USB = string.Format("{0:X4}", nodeConnectionInformation.DeviceDescriptor.bcdUSB);
            usbDevice.ClassCode_DeviceClass = nodeConnectionInformation.DeviceDescriptor.bDeviceClass;
            usbDevice.ClassCode_DeviceSubClass = nodeConnectionInformation.DeviceDescriptor.bDeviceSubClass;
            usbDevice.ClassCode_DeviceProtocol = nodeConnectionInformation.DeviceDescriptor.bDeviceProtocol;
            usbDevice.MaxPacketSize0 = nodeConnectionInformation.DeviceDescriptor.bMaxPacketSize0;
            usbDevice.IDVendor = nodeConnectionInformation.DeviceDescriptor.idVendor;
            usbDevice.IDProduct = nodeConnectionInformation.DeviceDescriptor.idProduct;
            usbDevice.Rev_Device = string.Format("{0:X4}", nodeConnectionInformation.DeviceDescriptor.bcdDevice);
            usbDevice.IndexOfManufacturer = nodeConnectionInformation.DeviceDescriptor.iManufacturer;
            usbDevice.IndexOfProduct = nodeConnectionInformation.DeviceDescriptor.iProduct;
            usbDevice.IndexOfSerialNumber = nodeConnectionInformation.DeviceDescriptor.iSerialNumber;
            usbDevice.NumberOfConfigurations = nodeConnectionInformation.DeviceDescriptor.bNumConfigurations;
            usbDevice.DeviceAddress = nodeConnectionInformation.DeviceAddress;
            usbDevice.NumberOfOpenPipes = nodeConnectionInformation.NumberOfOpenPipes;
            usbDevice.Speed = nodeConnectionInformation.Speed;
        }

        USB_NODE_CONNECTION_INFORMATION_EX_V2 nodeConnectionInformationV2 = GetNodeConnectionInformationV2(devicePath, connectionIndex);
        {
            if ((nodeConnectionInformationV2.SupportedUsbProtocols & USB_PROTOCOLS.Usb110) != 0) usbDevice.SupportedUsb_110_Protocol = true;
            if ((nodeConnectionInformationV2.SupportedUsbProtocols & USB_PROTOCOLS.Usb200) != 0) usbDevice.SupportedUsb_200_Protocol = true;
            if ((nodeConnectionInformationV2.SupportedUsbProtocols & USB_PROTOCOLS.Usb300) != 0) usbDevice.SupportedUsb_300_Protocol = true;
            if ((nodeConnectionInformationV2.Flags & USB_NODE_CONNECTION_INFORMATION_EX_V2_FLAGS.DeviceIsOperatingAtSuperSpeedOrHigher) != 0) usbDevice.DeviceIsOperatingAtSuperSpeedOrHigher = true;
            if ((nodeConnectionInformationV2.Flags & USB_NODE_CONNECTION_INFORMATION_EX_V2_FLAGS.DeviceIsSuperSpeedCapableOrHigher) != 0) usbDevice.DeviceIsSuperSpeedCapableOrHigher = true;
            if ((nodeConnectionInformationV2.Flags & USB_NODE_CONNECTION_INFORMATION_EX_V2_FLAGS.DeviceIsOperatingAtSuperSpeedPlusOrHigher) != 0) usbDevice.DeviceIsOperatingAtSuperSpeedPlusOrHigher = true;
            if ((nodeConnectionInformationV2.Flags & USB_NODE_CONNECTION_INFORMATION_EX_V2_FLAGS.DeviceIsSuperSpeedPlusCapableOrHigher) != 0) usbDevice.DeviceIsSuperSpeedPlusCapableOrHigher = true;
        }

        if (usbDevice.IndexOfManufacturer > 0)
        {
            string[] strManufacturer = GetNodeConnectionStringDescriptor(devicePath, connectionIndex, usbDevice.IndexOfManufacturer);
            if (!string.IsNullOrEmpty(strManufacturer[0]))
                usbDevice.StringDescriptor_Manufacturer_Length = Convert.ToUInt32(strManufacturer[0]);
            usbDevice.StringDescriptor_Manufacturer = strManufacturer[1];
        }
        if (usbDevice.IndexOfProduct > 0)
        {
            string[] strProduct = GetNodeConnectionStringDescriptor(devicePath, connectionIndex, usbDevice.IndexOfProduct);
            if (!string.IsNullOrEmpty(strProduct[0]))
                usbDevice.StringDescriptor_Product_Length = Convert.ToUInt32(strProduct[0]);
            usbDevice.StringDescriptor_Product = strProduct[1];
            usbDevice.Name = usbDevice.StringDescriptor_Product;
        }
        if (usbDevice.IndexOfSerialNumber > 0)
        {
            string[] strSerialNumber = GetNodeConnectionStringDescriptor(devicePath, connectionIndex, usbDevice.IndexOfSerialNumber);
            if (!string.IsNullOrEmpty(strSerialNumber[0]))
                usbDevice.StringDescriptor_SerialNumber_Length = Convert.ToUInt32(strSerialNumber[0]);
            usbDevice.StringDescriptor_SerialNumber = strSerialNumber[1];
        }

        for (byte indexofConfiguration = 0; indexofConfiguration < usbDevice.NumberOfConfigurations; indexofConfiguration++)
        {
            USBConfigurationDescriptor configurationDescriptor = new(GetNodeConnectionConfigurationDescriptor(devicePath, connectionIndex, indexofConfiguration));
            if (configurationDescriptor.IndexOfConfiguration > 0)
            {
                string[] strConfigDescription = GetNodeConnectionStringDescriptor(devicePath, connectionIndex, configurationDescriptor.IndexOfConfiguration);
                configurationDescriptor.StringDescriptor_Configuration = strConfigDescription[1];
            }
            usbDevice.ConfigurationDescriptors.Add(configurationDescriptor);
        }
        if (CheckInterfacesStatus && usbDevice.BaseDeviceProperties.Device_Service.Contains("USBSTOR", StringComparison.OrdinalIgnoreCase))
        {
            GetDiskDriveInterface(usbDevice);
        }
        return usbDevice;
    }

    public static void UpdateUSBDevicesFromSetupAPICollection(ObservableCollection<Device> usbDevicesFromSetupAPI)
    {
        uint flags = (uint)(DIGCF.DIGCF_PRESENT | DIGCF.DIGCF_DEVICEINTERFACE);
        Guid deviceClassGuid = ClassGuid[GUID_DEVCLASS.GUID_DEVINTERFACE_USB_DEVICE];
        usbDevicesFromSetupAPI.Clear();
        foreach (Device itemDevice in DeviceHelpers.GetClassDevicesWithProperties(deviceClassGuid, string.Empty, flags, FilterDeviceStatus, USBDevicesFilterData))
        {
            usbDevicesFromSetupAPI.Add(itemDevice);
        }
    }

    public static void GetDiskDriveInterface(USBDevice usbDevice)
    {
        uint flags = (uint)(DIGCF.DIGCF_PRESENT | DIGCF.DIGCF_DEVICEINTERFACE);
        Guid deviceClassGuid = ClassGuid[GUID_DEVCLASS.GUID_DEVINTERFACE_DISK];
        ObservableCollection<Device> diskDrivesFromSetupAPI = DeviceHelpers.GetClassDevicesWithProperties(deviceClassGuid, string.Empty, flags, parentID: usbDevice.BaseDeviceProperties.Device_InstanceId);

        if (diskDrivesFromSetupAPI.Count > 1 || diskDrivesFromSetupAPI.Count == 0) return;

        string? devicePath = diskDrivesFromSetupAPI[0].DevicePath;
        if (string.IsNullOrEmpty(devicePath)) return;

        DiskDriveInterface diskDrive = new();

        diskDrive.DevicePath = devicePath;
        diskDrive.BaseClassProperties = diskDrivesFromSetupAPI[0].ClassProperties;
        diskDrive.BaseDeviceProperties = diskDrivesFromSetupAPI[0].DeviceProperties;

        Win32ResponseDataStruct diskDriveGeometryEx = StorageInterfaceHelpers.GetDiskDriveGeometryEx(devicePath);
        if (diskDriveGeometryEx.Status)
        {
            DISK_GEOMETRY_EX diskDriveGeoEx = (DISK_GEOMETRY_EX)diskDriveGeometryEx.Data;
            diskDrive.DiskSize = diskDriveGeoEx.DiskSize;
            diskDrive.BytesPerSector = diskDriveGeoEx.Geometry.BytesPerSector;
            diskDrive.TotalCylinders = diskDriveGeoEx.Geometry.Cylinders;
            diskDrive.MediaType = diskDriveGeoEx.Geometry.MediaType;
            diskDrive.SectorsPerTrack = diskDriveGeoEx.Geometry.SectorsPerTrack;
            diskDrive.TracksPerCylinder = diskDriveGeoEx.Geometry.TracksPerCylinder;
            diskDrive.PartitionStyle = diskDriveGeoEx.PatitionInfo.PartitionStyle;
            if (diskDrive.PartitionStyle == PARTITION_STYLE.PARTITION_STYLE_MBR)
            {
                diskDrive.MBR_Signature = diskDriveGeoEx.PatitionInfo.PartitionLayoutInfo.MBR_Signature;
                diskDrive.MBR_CheckSum = diskDriveGeoEx.PatitionInfo.PartitionLayoutInfo.MBR_CheckSum;
            }
            else if (diskDrive.PartitionStyle == PARTITION_STYLE.PARTITION_STYLE_GPT)
            {
                diskDrive.GPT_DiskId = diskDriveGeoEx.PatitionInfo.PartitionLayoutInfo.GPT_DiskId;
            }
            diskDrive.TotalHeads = diskDriveGeoEx.Geometry.TracksPerCylinder;
            diskDrive.TotalTracks = diskDrive.TracksPerCylinder * diskDrive.TotalCylinders;
            diskDrive.TotalSectors = diskDrive.SectorsPerTrack * diskDrive.TotalTracks;
            if (diskDrive.MediaType == MEDIA_TYPE.RemovableMedia || diskDrive.MediaType == MEDIA_TYPE.FixedMedia)
                diskDrive.MediaLoaded = true;
        }

        Win32ResponseDataStruct diskDriveNumber = StorageInterfaceHelpers.GetDiskDriveNumber(devicePath);
        if (diskDriveNumber.Status)
        {
            STORAGE_DEVICE_NUMBER diskDriveNum = (STORAGE_DEVICE_NUMBER)diskDriveNumber.Data;
            diskDrive.DiskNumber = diskDriveNum.DeviceNumber;
            diskDrive.Name = $@"\\.\PHYSICALDRIVE{diskDrive.DiskNumber}";
        }

        Win32ResponseDataStruct diskDriveStorageDescriptor = StorageInterfaceHelpers.GetStorageDeviceDescriptor(devicePath);
        if (diskDriveStorageDescriptor.Status)
        {
            StorageDeviceDescriptor diskDriveDescriptor = (StorageDeviceDescriptor)diskDriveStorageDescriptor.Data;
            diskDrive.DeviceRevision = diskDriveDescriptor.ProductRevision;
            diskDrive.SerialNumber = diskDriveDescriptor.SerialNumber;
            diskDrive.VendorId = diskDriveDescriptor.VendorId;
            diskDrive.ProductId = diskDriveDescriptor.ProductId;
            diskDrive.BusType = diskDriveDescriptor.BusType;
            diskDrive.RemovableMedia = diskDriveDescriptor.RemovableMedia;
        }

        Win32ResponseDataStruct driveLayoutInformationEx = StorageInterfaceHelpers.GetDiskDriveLayoutInformationEx(devicePath);
        if (driveLayoutInformationEx.Status)
        {
            DRIVE_LAYOUT_INFORMATION_EX driveLayoutInfoEx = (DRIVE_LAYOUT_INFORMATION_EX)driveLayoutInformationEx.Data;
            if (driveLayoutInfoEx.PartitionStyle == PARTITION_STYLE.PARTITION_STYLE_GPT)
            {
                diskDrive.GPT_MaxPartitionCount = driveLayoutInfoEx.DriveInfo.GPT_MaxPartitionCount;
                diskDrive.GPT_StartingUsableOffset = driveLayoutInfoEx.DriveInfo.GPT_StartingUsableOffset;
                diskDrive.GPT_UsableLength = driveLayoutInfoEx.DriveInfo.GPT_UsableLength;
            }

            ObservableCollection<DiskPartitionInterface> partitions = StorageInterfaceHelpers.GetDiskDrivePartitions(driveLayoutInfoEx, diskDrive);
            if (partitions.Count > 0)
            {
                foreach (DiskPartitionInterface itemPartition in partitions)
                {
                    diskDrive.Add(itemPartition);
                }
            }
        }

        usbDevice.Add(diskDrive);
    }

    

}
