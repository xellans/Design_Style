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

        public static readonly DependencyProperty TimeTypeProperty = DependencyProperty.Register(nameof(TimeType),
                 typeof(TimeIntervals),
                 typeof(NumericUpDownTime),
                 new FrameworkPropertyMetadata(TimeIntervals.Millisecond)
                 {
                     PropertyChangedCallback = OnTimeTypeChanged,
                     BindsTwoWayByDefault = true,
                     DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                 });

        private static void OnTimeTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            NumericUpDownTime numeric = (NumericUpDownTime)d;
            numeric.ValueResult = numeric.Value;
            numeric.ValueResult =  e.NewValue switch
            {
                TimeIntervals.Millisecond => numeric.ValueResult,
                TimeIntervals.Second => numeric.ValueResult * 1000,
                TimeIntervals.Minute => numeric.ValueResult * 60_000,
                TimeIntervals.Hour => numeric.ValueResult * 3_600_000,
                _ => throw new NotImplementedException(),
            };
        }
        #endregion

        /// <summary>
        /// Выходной результат Vaule, возвращает единицы измерения в Millisecond.
        /// </summary>
        #region ValueResult
        public double ValueResult
        {
            get { return (double)GetValue(ValueResultProperty); }
            private set { SetValue(ValueResultProperty, value); }
        }
        public static readonly DependencyProperty ValueResultProperty = DependencyProperty.Register(nameof(ValueResult),
                typeof(double),
                typeof(NumericUpDownTime),
                new FrameworkPropertyMetadata(0.0)
                {
                    BindsTwoWayByDefault = true,
                    DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                });
        protected override void OnValueChanged(double oldValue, double newValue)
        {
            base.OnValueChanged(oldValue, newValue);
            ValueResult = TimeType switch
            {
                TimeIntervals.Millisecond => newValue,
                TimeIntervals.Second => newValue * 1000,
                TimeIntervals.Minute => newValue * 60_000,
                TimeIntervals.Hour => newValue * 3_600_000,
                _ => throw new NotImplementedException(),
            };
            }
        }
    #endregion
}
