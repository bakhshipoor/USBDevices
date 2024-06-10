using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USBDevicesLibrary.USBDevices;

namespace USBDevicesLibrary.Interfaces;

public class InterfaceBaseClass : ObservableCollection<InterfaceBaseClass>
{
    public InterfaceBaseClass()
    {

    }

    public IEnumerable Children
    {
        get
        {
            return this;
        }
    }

    public virtual List<PropertiesToList> PropertiesToList()
    {
        List<PropertiesToList> filedsToList =
        [
            
        ];
        return filedsToList;
    }
}
