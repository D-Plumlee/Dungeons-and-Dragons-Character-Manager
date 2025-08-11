using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace GuiApplication;

[ValueConversion(typeof(string), typeof(string))]
public class FileNameConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        //checbox.Name = fil.Substring(fil.LastIndexOf(System.IO.Path.DirectorySeparatorChar) + 1).Split(".")[0];
        //checbox.Content = fil.Substring(fil.LastIndexOf(System.IO.Path.DirectorySeparatorChar) + 1);

        string fullPath = value as string;
        return fullPath.Substring(fullPath.LastIndexOf(System.IO.Path.DirectorySeparatorChar) + 1);
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        string shortPath = value as string;
        return Directory.GetCurrentDirectory() + $"{System.IO.Path.DirectorySeparatorChar}" + shortPath;
    }
}
