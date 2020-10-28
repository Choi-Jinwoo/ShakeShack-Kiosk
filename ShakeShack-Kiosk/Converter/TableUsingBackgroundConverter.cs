using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ShakeShack_Kiosk.Converter
{
    static class Constants
    {
        public const string USING_COLOR = "#808080";
        public const string UNUSING_COLOR = "#ffffff";
    }

    class TableUsingBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool boolValue = (bool) value;

            if (boolValue)
            {
                return Constants.USING_COLOR;
            }

            return Constants.UNUSING_COLOR;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
