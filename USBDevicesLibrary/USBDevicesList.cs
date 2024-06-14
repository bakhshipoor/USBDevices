using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using USBDevicesLibrary.Devices;
using USBDevicesLibrary.Events;
using USBDevicesLibrary.USBDevices;
using static USBDevicesLibrary.Events.EventTypesEnum;

namespace USBDevicesLibrary;

public class USBDevicesList 
{
    public event EventHandler<USBDevicesEventArgs>? DeviceChanged;
    public event EventHandler? InitialCollectionsComplete;

    public ObservableCollection<USBHub> USBHubs { get; set; }
    public ObservableCollection<Device> USBDevicesFromSetupAPI { get; set; }
    public ObservableCollection<USBDevice> USBDevices { get; set; }

    private readonly USBDevicesEventManager eventMnager;

    public bool InitialCompleted { get; set; }

    public bool ConnectedEventStatus 
    { 
        get { return USBDevicesListHelpers.ConnectedEventStatus; } 
        set { USBDevicesListHelpers.ConnectedEventStatus = value; } 
    }
    public bool DisconnectedEventStatus
    {
        get { return USBDevicesListHelpers.DisconnectedEventStatus; }
        set { USBDevicesListHelpers.DisconnectedEventStatus = value; }
    }
    public bool ModifiedEventStatus
    {
        get { return USBDevicesListHelpers.ModifiedEventStatus; }
        set { USBDevicesListHelpers.ModifiedEventStatus = value; }
    }
    public bool FilterDeviceStatus
    {
        get { return USBDevicesListHelpers.FilterDeviceStatus; }
        set { USBDevicesListHelpers.FilterDeviceStatus = value; }
    }

    public bool CheckInterfacesStatus
    {
        get { return USBDevicesListHelpers.CheckInterfacesStatus; }
        set { USBDevicesListHelpers.CheckInterfacesStatus = value; }
    }

    public USBDevicesList()
    {
        USBHubs = [];
        USBDevicesFromSetupAPI = [];
        USBDevices = [];
        eventMnager = new(this);
        InitialCompleted = false;

        DisconnectedEventStatus = true;
        ConnectedEventStatus = true;
        ModifiedEventStatus = true;
        FilterDeviceStatus = false;
        CheckInterfacesStatus = true;

        USBDevices.CollectionChanged += USBDevices_CollectionChanged;
    }

    private void USBDevices_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.Action==NotifyCollectionChangedAction.Add)
        {
            if (ConnectedEventStatus)
            {
                if (e.NewItems != null)
                {
                    foreach (USBDevice itemUSBDevice in e.NewItems)
                    {
                        OnDeviceChanged(new USBDevicesEventArgs(itemUSBDevice, EventTypeEnum.Connected));
                    }
                }
            }
        }
        else if (e.Action == NotifyCollectionChangedAction.Remove)
        {
            if (DisconnectedEventStatus)
            {
                if (e.OldItems != null)
                {
                    foreach (USBDevice itemUSBDevice in e.OldItems)
                    {
                        OnDeviceChanged(new USBDevicesEventArgs(itemUSBDevice, EventTypeEnum.Disconnected));
                    }
                }
            }
        }
    }

    public void Start()
    {
        USBHubs.Clear();
        USBDevicesFromSetupAPI.Clear();
        USBDevices.Clear();
        USBDevicesListHelpers.UpdateHubCollection(USBHubs);
        USBDevicesListHelpers.UpdateUSBDevicesFromSetupAPICollection(USBDevicesFromSetupAPI);
        USBDevicesListHelpers.UpdateUSBDevicesCollection(USBHubs, USBDevicesFromSetupAPI, USBDevices);
        InitialCompleted = true;
        OnInitialCollectionsComplete(EventArgs.Empty);
    }

    protected virtual void OnInitialCollectionsComplete(EventArgs e)
    {
        InitialCollectionsComplete?.Invoke(this, e);
    }

    internal virtual void OnDeviceChanged(USBDevicesEventArgs e)
    {
        DeviceChanged?.Invoke(this, e);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="idVendor">String just for 'Hexadecimal' value. UInt16 or ushort for decimal values.</param>
    /// <param name="idProduct">String just for 'Hexadecimal' value. UInt16 or ushort for decimal values.</param>
    /// <returns>Return True if Add successful or False if unsuccessfull</returns>
    public void SetDeviceToFilter(object idVendor, [AllowNull] object? idProduct=null, [AllowNull] string? service=null)
    {
        string? strVID = string.Format("{0:X4}", idVendor).ToUpper().Trim();
        
            if (strVID.ToCharArray().All(char.IsAsciiHexDigit))
                USBDevicesListHelpers.USBDevicesFilterData.IDVendor = strVID;
        
        string? strPID = string.Format("{0:X4}", idProduct).ToUpper().Trim();
        
            if (strPID.ToCharArray().All(char.IsAsciiHexDigit))
                USBDevicesListHelpers.USBDevicesFilterData.IDProduct = strPID;
        
        if (service != null)
        {
            USBDevicesListHelpers.USBDevicesFilterData.Service = service;
        }
    }

    
}
