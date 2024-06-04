using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static USBDevicesLibrary.Win32API.USBSpec;

namespace USBDevicesLibrary.USBDevices;

public class USBDeviceDescriptor
{
    public USBDeviceDescriptor()
    {
        
    }

    public USBDeviceDescriptor(USB_DEVICE_DESCRIPTOR deviceDescriptor) : this()
    {
        bLength = deviceDescriptor.bLength;
        bDescriptorType = deviceDescriptor.bDescriptorType;
        bcdUSB = deviceDescriptor.bcdUSB;
        bDeviceClass = deviceDescriptor.bDeviceClass;
        bDeviceSubClass = deviceDescriptor.bDeviceSubClass;
        bDeviceProtocol = deviceDescriptor.bDeviceProtocol;
        bMaxPacketSize0 = deviceDescriptor.bMaxPacketSize0;
        idVendor = deviceDescriptor.idVendor;
        idProduct = deviceDescriptor.idProduct;
        bcdDevice = deviceDescriptor.bcdDevice;
        iManufacturer = deviceDescriptor.iManufacturer;
        iProduct = deviceDescriptor.iProduct;
        iSerialNumber = deviceDescriptor.iSerialNumber;
        bNumConfigurations = deviceDescriptor.bNumConfigurations;
    }

    public byte bLength { get; set; }
    public byte bDescriptorType { get; set; }
    public ushort bcdUSB { get; set; }
    public byte bDeviceClass { get; set; }
    public byte bDeviceSubClass { get; set; }
    public byte bDeviceProtocol { get; set; }
    public byte bMaxPacketSize0 { get; set; }
    public ushort idVendor { get; set; }
    public ushort idProduct { get; set; }
    public ushort bcdDevice { get; set; }
    public byte iManufacturer { get; set; }
    public byte iProduct { get; set; }
    public byte iSerialNumber { get; set; }
    public byte bNumConfigurations { get; set; }
}
