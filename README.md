# USB Devices Library
This Library List And Event USB Devices and Filter them


- This library based on C++ codes. Using Windows IOCTL and C++ header files. So you can easily convert this library to C++.
- Using `setupapi.dll`, `kernel32.dll` and `fmifs.dll` files.
- .Net Core 8.0
- Windows Desktop 
- List of USB Devices in pc
- Supported interfaces:
  - Disk Drive
  - Disk Partition
  - Logical Disk

- Event Types:
  - Connected
  - Disconnected

## Usage
 1. Create new instance of class `USBDevicesList`
    ```sh
    public USBDevicesList USBDevicesCollection { get; set; }
    ```

    ```sh
    USBDevicesCollection = new();
    ```

2. By default Connected and Disconnected events are enabled if you want you can disable each of them by:
    ```sh
   USBDevicesCollection.ConnectedEventStatus = false;;
   USBDevicesCollection.DisconnectedEventStatus = true;
   ```

3. You can set filter to monitor usb devices. In this case you should enable the filter status by: 
   ```sh
   USBDevicesCollection.EnableFilterDevice();
   ```
   you can diable filter status by:
   ```sh
   USBDevicesCollection.FilterDeviceStatus = true;
   ```
   after enabling, set VID, PID or Service to filter. You can fillter by any of VID, PID, Service. Such as `SetDeviceToFilter("xxxx", "yyyy", "Service Name")`. The VID value cant be null any other nullable .
   ```sh
   USBDevicesCollection.SetDeviceToFilter("xxxx", "yyyy", "Service Name");
   ```
   

4. Add Events :
   ```sh
   USBDevicesCollection.InitialCollectionsComplete += USBDevicesCollections_InitialCollectionsComplete;
   USBDevicesCollection.DeviceChanged += USBDevicesCollections_DeviceChanged;
   ```

5. After initial `USBDevices` class you should start the monitiring by `Start()`:
   ```sh
   USBDevicesCollection.Start();
   ```

6. `USBDevicesList` have property `ObservableCollection<USBDevice> USBDevices` you can access list of usb device can enumrate this observable collection.

This Library have two example of usage of this library.
1. `USBDevicesDemo` a very simple `WPF` example to view properties of devices.
2. `CopyFilesToFlash` a simple `WPF` `MVVM` example to work with sorage interface. This example copy batch of files to usb flash disk automatically. This examples not prepared to publish just for undrestanding how to work.  

![USBDevicesDemo-1](https://github.com/bakhshipoor/USBDevices/assets/2270529/0b873117-bf46-4962-bb34-6ecd9cfd1d1d)

![USBDevicesDemo-2](https://github.com/bakhshipoor/USBDevices/assets/2270529/3c2f4b65-bc51-49db-af03-b87e48fb7605)

![USBDevicesDemo-3](https://github.com/bakhshipoor/USBDevices/assets/2270529/059dc2db-2c80-4772-a1e8-9885403dbda2)

![USBDevicesDemo-4](https://github.com/bakhshipoor/USBDevices/assets/2270529/c09e81ab-59dd-4a22-9622-dee38b414f92)

![CopyFilesToFlash-1](https://github.com/bakhshipoor/USBDevices/assets/2270529/bd04ac13-bc25-4e69-b69e-e45ce0874791)






