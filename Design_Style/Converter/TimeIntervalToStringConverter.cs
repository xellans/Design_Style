using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Design_Style.Converter
{
    public class TimeIntervalToStringConverter : IMultiValueConverter
    {
        public static readonly IReadOnlyList<TimeIntervals> Intervals = Array.AsReadOnly(Enum.GetValues<TimeIntervals>());

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            TimeIntervals interval = (TimeIntervals)values[0];
            IReadOnlyDictionary<TimeIntervals, string> displays = (IReadOnlyDictionary<TimeIntervals, string>)values[0];
            return displays[interval];
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }


        private TimeIntervalToStringConverter() { }

        public static TimeIntervalToStringConverter Instance { get; } = new TimeIntervalToStringConverter();
    }
}
