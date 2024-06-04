namespace USBDevicesLibrary.Devices;

public class Device
{
    public Device()
    {
        DevicePath = string.Empty;
        DeviceProperties = new();
        ClassProperties = new();
        //InterfaceProperties = new();
        //ContainerProperties = new();
        //DriverProperties = new();
    }

    public string DevicePath { get; set; }
    public DeviceProperties DeviceProperties { get; set; }
    public DeviceClassProperties ClassProperties { get; set; }
    //public DeviceInterfaceProperties InterfaceProperties { get; set; }
    //public DeviceContainerProperties ContainerProperties { get; set; }
    //public DeviceDriverProperties DriverProperties { get; set; }

}
