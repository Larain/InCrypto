using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace icApplication.Converter
{
    [ValueConversion(typeof(List<string>), typeof(string))]
    public class MultilineConverter : IValueConverter
    {
        public object Convert(object values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null)
                return null;

            string[] text = (string[])values;
            if (text.Length < 2)
                return text[0];
            else return string.Join(Environment.NewLine, text);
        }

        public object ConvertBack(object value, Type targetTypes, object parameter, CultureInfo culture)
        {
            string stringValue = (string)value;
            string[] sad = stringValue.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            return sad;
        }
    }
}
