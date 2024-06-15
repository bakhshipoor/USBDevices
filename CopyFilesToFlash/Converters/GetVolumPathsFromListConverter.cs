using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace CopyFilesToFlash.Converters;

public class GetVolumPathsFromListConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value !=null)
        {
            var listVolumePaths = ((ListView)value).ItemsSource as List<string>;
            if (listVolumePaths != null && listVolumePaths.Count > 0)
            {
                string strPaths = string.Join(" , ", listVolumePaths);
                return strPaths;
            }
        }
        return string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return new List<string>();
    }
}
