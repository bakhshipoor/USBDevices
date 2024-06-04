﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using USBDevicesLibrary.Devices;
using static USBDevicesLibrary.Win32API.USBIOCtl;

namespace USBDevicesLibrary.USBDevices;

public class USBDevice
{
    public USBDevice()
    {
        Rev_Device = string.Empty;
        Rev_USB = string.Empty;
        DevicePath_ParentHub = string.Empty;
        StringDescriptor_Manufacturer = string.Empty;
        StringDescriptor_Product = string.Empty;
        StringDescriptor_SerialNumber = string.Empty;

        ConfigurationDescriptors = new();
        //Interfaces = new();

        DevicePath = string.Empty;
        BaseDeviceProperties = new();
        BaseClassProperties = new();
    }

    public USBDevice(Device device) : this()
    {
        DevicePath = device.DevicePath;
        BaseDeviceProperties = device.DeviceProperties;
        BaseClassProperties = device.ClassProperties;
    }

    // USB Specification Release Number in Binary-Coded Decimal(i.e., 2.10 is 210H).
    // This field identifies the release of the USB Specification with which the device and its descriptors are compliant.
    public string Rev_USB { get; set; }
    // Class code (assigned by the USB-IF). If this field is reset to zero, each interface within a configuration specifies
    // its own class information and the various interfaces operate independently. If this field is set to a value between 1 and FEH,
    // the device supports different class specifications on different interfaces and the interfaces may not operate independently.
    // This value identifies the class definition used for the aggregate interfaces. If this field is set to FFH, the device class is vendor-specific.
    // https://www.usb.org/defined-class-codes
    public byte ClassCode_DeviceClass { get; set; }
    // Subclass code(assigned by the USB-IF). These codes are qualified by the value of the bDeviceClass field.
    // If the bDeviceClass field is reset to zero, this field must also be reset to zero. If the bDeviceClass field is not set to FFH,
    // all values are reserved for assignment by the USB-IF.
    public byte ClassCode_DeviceSubClass { get; set; }
    // Protocol code (assigned by the USB-IF). These codes are qualified by the value of the bDeviceClass and the bDeviceSubClass fields.If a device
    // supports class-specific protocols on a device basis as opposed to an interface basis, this code identifies the protocols that the device uses as defined by the
    // specification of the device class. If this field is reset to zero, the device does not use class-specific protocols on a device basis.However, it may use classspecific
    // protocols on an interface basis. If this field is set to FFH, the device uses a vendor-specific protocol on a device basis.
    public byte ClassCode_DeviceProtocol { get; set; }
    // Maximum packet size for endpoint zero (only 8, 16, 32, or 64 are valid)
    public byte MaxPacketSize0 { get; set; }
    // Vendor ID (assigned by the USB-IF) **[ VID ]**
    public ushort IDVendor { get; set; }
    // Product ID (assigned by the manufacturer) **[ PID ]**
    public ushort IDProduct { get; set; }
    // Device release number in binary-coded decimal
    public string Rev_Device { get; set; }
    // Index of string descriptor describing manufacturer
    public byte IndexOfManufacturer { get; set; }
    // Index of string descriptor describing product
    public byte IndexOfProduct { get; set; }
    // Index of string descriptor describing the device’s serial number
    public byte IndexOfSerialNumber { get; set; }
    // Number of possible configurations
    public byte NumberOfConfigurations { get; set; }
    
    public ushort DeviceAddress {  get; set; }
    public uint NumberOfOpenPipes {  get; set; }
    public USB_DEVICE_SPEED Speed {  get; set; }

    public bool SupportedUsb_110_Protocol {  get; set; }
    public bool SupportedUsb_200_Protocol { get; set; }
    public bool SupportedUsb_300_Protocol { get; set; }

    public bool DeviceIsOperatingAtSuperSpeedOrHigher { get; set; }
    public bool DeviceIsSuperSpeedCapableOrHigher { get; set; }
    public bool DeviceIsOperatingAtSuperSpeedPlusOrHigher { get; set; }
    public bool DeviceIsSuperSpeedPlusCapableOrHigher { get; set; }

    // Mnaufacturer string descriptor from usb device
    public string StringDescriptor_Manufacturer {  get; set; }
    // This is not a string lenght.
    // The first two byte of descriptor not a string data. the first byte (index [0]) of descriptor contain the length of a descriptor.
    // descritor[0] - 2 = string bytes count.  2 means first two bytes.
    // Usefull for some purposes
    public uint StringDescriptor_Manufacturer_Length { get; set; }
    // Product string descriptor from usb device
    public string StringDescriptor_Product { get; set; }
    // This is not a string lenght.
    // The first two byte of descriptor not a string data. the first byte (index [0]) of descriptor contain the length of a descriptor.
    // descritor[0] - 2 = string bytes count.  2 means first two bytes.
    // Usefull for some purposes
    public uint StringDescriptor_Product_Length { get; set; }
    // Serial Number string descriptor from usb device
    public string StringDescriptor_SerialNumber { get; set; }
    // This is not a string lenght.
    // The first two byte of descriptor not a string data. the first byte (index [0]) of descriptor contain the length of a descriptor.
    // descritor[0] - 2 = string bytes count.  2 means first two bytes.
    // Usefull for some purposes
    public uint StringDescriptor_SerialNumber_Length { get; set; }

    public ObservableCollection<USBConfigurationDescriptor> ConfigurationDescriptors { get; set; }
    //public ObservableCollection<USBInterface> Interfaces { get; set; }

    public string DevicePath { get; set; }
    public DeviceProperties BaseDeviceProperties { get; set; }
    public DeviceClassProperties BaseClassProperties { get; set; }
    public string DevicePath_ParentHub { get; set; }
    public uint PortIndex { get; set; }

}
