using System.Collections.ObjectModel;
using System.Collections.Specialized;
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

    public static bool ConnectedEventStatus 
    { 
        get { return USBDevicesListHelpers.ConnectedEventStatus; } 
        set { USBDevicesListHelpers.ConnectedEventStatus = value; } 
    }
    public static bool DisconnectedEventStatus
    {
        get { return USBDevicesListHelpers.DisconnectedEventStatus; }
        set { USBDevicesListHelpers.DisconnectedEventStatus = value; }
    }
    public static bool ModifiedEventStatus
    {
        get { return USBDevicesListHelpers.ModifiedEventStatus; }
        set { USBDevicesListHelpers.ModifiedEventStatus = value; }
    }
    public static bool FilterDeviceStatus
    {
        get { return USBDevicesListHelpers.FilterDeviceStatus; }
        set { USBDevicesListHelpers.FilterDeviceStatus = value; }
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

        USBDevices.CollectionChanged += USBDevices_CollectionChanged;

        FilterDeviceStatus = true;
        AddDeviceToFilter("10C4");
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
    /// <param name="idVendor">String just for 'Hexadecimal' value. UInt16, ushort for decimal values.</param>
    /// <param name="idProduct">String just for 'Hexadecimal' value. UInt16, ushort for decimal values.</param>
    /// <returns>Return True if Add successful or False if unsuccessfull</returns>
    public static bool AddDeviceToFilter(object idVendor, object? idProduct=null)
    {
        bool find = false;
        uint _VID = 0;
        uint _PID = 0;
        bool bResponse = false;

        if (idVendor == null) return bResponse;

        string? strVID = string.Format("{0:X4}", idVendor).ToUpper().Trim();
        if (!string.IsNullOrEmpty(strVID) && strVID.Length < 5 )
            if (strVID.ToCharArray().All(char.IsAsciiHexDigit))
                _VID = Convert.ToUInt16(strVID, 16);
        
        if (idProduct != null)
        {
            string? strPID = string.Format("{0:X4}", idProduct).ToUpper().Trim();
            if (!string.IsNullOrWhiteSpace(strPID) && strPID.Length < 5)
                if (strPID.ToCharArray().All(char.IsAsciiHexDigit))
                    _PID = Convert.ToUInt16(strPID, 16);
        }

        if (_VID==0) return bResponse;

        foreach (USBDevicesFilter itemFilter in USBDevicesListHelpers.USBDevicesFilterList)
        {
            if (itemFilter.IDVendor == _VID && itemFilter.IDProduct == _PID)
            {
                find = true;
                break;
            }
        }

        if (!find)
        {
            USBDevicesListHelpers.USBDevicesFilterList.Add(new USBDevicesFilter { IDVendor = (ushort)_VID, IDProduct = (ushort)_PID });
            bResponse = true;
        }

        return bResponse;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="idVendor">String just for 'Hexadecimal' value. UInt16, ushort for decimal values.</param>
    /// <param name="idProduct">String just for 'Hexadecimal' value. UInt16, ushort for decimal values.</param>
    /// <returns>Return True if Remove successful or False if unsuccessfull</returns>
    public static bool RemoveDeviceFromFilter(object idVendor, object? idProduct=null)
    {
        bool find = false;
        uint _VID = 0;
        uint _PID = 0;
        bool bResponse = false;

        if (idVendor == null) return bResponse;

        string? strVID = string.Format("{0:X4}", idVendor).ToUpper().Trim();
        if (!string.IsNullOrEmpty(strVID) && strVID.Length < 5)
            if (strVID.ToCharArray().All(char.IsAsciiHexDigit))
                _VID = Convert.ToUInt16(strVID, 16);

        if (idProduct != null)
        {
            string? strPID = string.Format("{0:X4}", idProduct).ToUpper().Trim();
            if (!string.IsNullOrWhiteSpace(strPID) && strPID.Length < 5)
                if (strPID.ToCharArray().All(char.IsAsciiHexDigit))
                    _PID = Convert.ToUInt16(strPID, 16);
        }

        if (_VID == 0) return bResponse;

        USBDevicesFilter devicesFilter = new();
        foreach (USBDevicesFilter item in USBDevicesListHelpers.USBDevicesFilterList)
        {
            if (item.IDVendor == (ushort)_VID && item.IDProduct == (ushort)_PID)
            {
                find = true;
                devicesFilter = item;
                break;
            }
        }
        if (find)
        {
            USBDevicesListHelpers.USBDevicesFilterList.Remove(devicesFilter);
            bResponse = true;
        }
        return bResponse;
    }

    public static List<USBDevicesFilter> GetDevicesFromFilter()
    {
        return [.. USBDevicesListHelpers.USBDevicesFilterList];
    }
}
