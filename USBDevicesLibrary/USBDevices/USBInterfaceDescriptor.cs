using Microsoft.Win32.SafeHandles;
using System.Buffers.Text;
using System.Collections.ObjectModel;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Configuration;
using static USBDevicesLibrary.Win32API.USBSpec;
using System.Windows.Documents;
using USBDevicesLibrary.Devices;

namespace USBDevicesLibrary.USBDevices;

public class USBInterfaceDescriptor
{
    public USBInterfaceDescriptor()
    {
        Description = string.Empty;
        Pipes = [];
    }

    public USBInterfaceDescriptor(USB_INTERFACE_DESCRIPTOR usbInterfaceDescriptor) : this()
    {
        InterfaceNumber = usbInterfaceDescriptor.bInterfaceNumber;
        AlternateSetting = usbInterfaceDescriptor.bAlternateSetting;
        NumberOfEndpoints = usbInterfaceDescriptor.bNumEndpoints;
        InterfaceClass = usbInterfaceDescriptor.bInterfaceClass;
        InterfaceSubClass = usbInterfaceDescriptor.bInterfaceSubClass;
        InterfaceProtocol = usbInterfaceDescriptor.bInterfaceProtocol;
        IInterface = usbInterfaceDescriptor.iInterface;
    }

    // Number of this interface. Zero-based value identifying the index in the array of concurrent interfaces supported by this configuration.
    public byte InterfaceNumber { get; set; }
    // Value used to select this alternate setting for the interface identified in the prior field
    public byte AlternateSetting { get; set; }
    // Number of endpoints used by this interface (excluding endpoint zero). If this value is zero,
    // this interface only uses the Default Control Pipe.
    public byte NumberOfEndpoints { get; set; }
    // Class code (assigned by the USB-IF). A value of zero is reserved for future standardization.
    // If this field is set to FFH, the interface class is vendor-specific.
    // All other values are reserved for assignment by the USB-IF.
    public byte InterfaceClass { get; set; }
    // Subclass code (assigned by the USB-IF). These codes are qualified by the value of the bInterfaceClass field.
    // If the bInterfaceClass field is reset to zero, this field must also be reset to zero.
    // If the bInterfaceClass field is not set to FFH, all values are reserved for assignment by the USB-IF.
    public byte InterfaceSubClass { get; set; }
    // Protocol code (assigned by the USB). These codes are qualified by the value of the bInterfaceClass and the bInterfaceSubClass fields.
    // If an interface supports class-specific requests, this code identifies the protocols that the device uses as defined by
    // the specification of the device class. If this field is reset to zero, the device does not use a class-specific
    // protocol on this interface. If this field is set to FFH, the device uses a vendor-specific protocol for this interface.
    public byte InterfaceProtocol { get; set; }
    // Index of string descriptor describing this interface
    public byte IInterface { get; set; }

    public string  Description {  get; set; }

    public ObservableCollection<USBPipeDescriptor> Pipes {  get; set; }

}
