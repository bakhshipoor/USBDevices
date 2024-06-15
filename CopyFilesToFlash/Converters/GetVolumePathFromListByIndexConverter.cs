using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace CopyFilesToFlash.Converters;

public class GetVolumePathFromListByIndexConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not null && parameter!=null)
        {
            var listVolumePaths = ((ListView)parameter).ItemsSource as List<string>;
            if (listVolumePaths!=null && listVolumePaths.Count>0)
            {
                uint indexOfPath = (uint)value;
                return indexOfPath>0?listVolumePaths[(int)indexOfPath-1]:"Not In Progress";
            }
        }
        return string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return new List<string>();
    }
}
