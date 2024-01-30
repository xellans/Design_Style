using System.Windows.Controls;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using static System.Net.Mime.MediaTypeNames;

namespace Design_Style
{
    /// <summary>
    /// Перечисление для хранения доступных типов времени
    /// </summary>
    public enum TimeIntervals
    {
        Minute,
        Second,
        Hour,
        Millisecond
    }
    public class NumericUpDownTime : NumericUpDown
    {
        public static readonly IReadOnlyDictionary<TimeIntervals, string> TimeIntervalsRus
           = new ReadOnlyDictionary<TimeIntervals, string>(new Dictionary<TimeIntervals, string>()
       {
            {TimeIntervals.Millisecond, "мс"},
            {TimeIntervals.Second, "сек"},
            {TimeIntervals.Minute, "мин"},
            {TimeIntervals.Hour, "час"},
       });

        public IReadOnlyDictionary<TimeIntervals, string> TimeIntervalsDisplay
        {
            get => (IReadOnlyDictionary<TimeIntervals, string>)GetValue(MyPropertyProperty);
            set => SetValue(MyPropertyProperty, value);
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyPropertyProperty =
            DependencyProperty.Register(nameof(TimeIntervalsDisplay), typeof(IReadOnlyDictionary<TimeIntervals, string>), typeof(NumericUpDownTime), new PropertyMetadata(TimeIntervalsRus));

        static NumericUpDownTime() => DefaultStyleKeyProperty.OverrideMetadata(typeof(NumericUpDownTime), new FrameworkPropertyMetadata(typeof(NumericUpDownTime)));

        #region TimeType 

        /// <summary>
        /// Выбранный тип времени пользователем
        /// </summary>
        public TimeIntervals TimeType
        {
            get { return (TimeIntervals)GetValue(TimeTypeProperty); }
            set { SetValue(TimeTypeProperty, value); }
        }

        public static readonly DependencyProperty TimeTypeProperty =
         DependencyProperty.Register(nameof(TimeType),
             typeof(TimeIntervals),
             typeof(NumericUpDownTime),
             new FrameworkPropertyMetadata(TimeIntervals.Millisecond));
        #endregion
    }
}
