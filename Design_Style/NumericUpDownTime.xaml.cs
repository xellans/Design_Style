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
using System.Collections;

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
    public class NumericUpDownTime : RangeBase
    {
        static NumericUpDownTime()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NumericUpDownTime), new FrameworkPropertyMetadata(typeof(NumericUpDownTime)));
            SmallChangeProperty.OverrideMetadata(typeof(NumericUpDownTime), new FrameworkPropertyMetadata(1.0));
        }

        #region Dp свойства для персонализации

        #region CornerRadius радиус закругления
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
        nameof(CornerRadius),
        typeof(CornerRadius),
        typeof(NumericUpDownTime), new PropertyMetadata(new CornerRadius(6.0)));

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        public static void SetCornerRadius(UIElement element, CornerRadius value) => element.SetValue(CornerRadiusProperty, value);

        public static CornerRadius GetCornerRadius(UIElement element) => (CornerRadius)element.GetValue(CornerRadiusProperty);
        #endregion

        #region BackgroundArrow цвет стрелочки
        public static readonly DependencyProperty BackgroundArrowProperty = DependencyProperty.Register(
        nameof(BackgroundArrow),
        typeof(SolidColorBrush),
        typeof(NumericUpDownTime), new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#BBBBBB"))));

        public SolidColorBrush BackgroundArrow
        {
            get { return (SolidColorBrush)GetValue(BackgroundArrowProperty); }
            set { SetValue(BackgroundArrowProperty, value); }
        }
        public static void SetBackgroundArrow(UIElement element, SolidColorBrush value) => element.SetValue(BackgroundArrowProperty, value);

        public static SolidColorBrush GetBackgroundArrow(UIElement element) => (SolidColorBrush)element.GetValue(BackgroundArrowProperty);
        #endregion

        #region BackgroundArrowMouseOver цвет стрелочки при наведение мыши
        public static readonly DependencyProperty BackgroundArrowMouseOverProperty = DependencyProperty.Register(
        nameof(BackgroundArrowMouseOver),
        typeof(SolidColorBrush),
        typeof(NumericUpDownTime), new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"))));

        public SolidColorBrush BackgroundArrowMouseOver
        {
            get { return (SolidColorBrush)GetValue(BackgroundArrowMouseOverProperty); }
            set { SetValue(BackgroundArrowMouseOverProperty, value); }
        }
        public static void SetBackgroundArrowMouseOver(UIElement element, SolidColorBrush value) => element.SetValue(BackgroundArrowMouseOverProperty, value);

        public static SolidColorBrush GetBackgroundArrowMouseOver(UIElement element) => (SolidColorBrush)element.GetValue(BackgroundArrowMouseOverProperty);
        #endregion
        #endregion


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

        public static readonly IReadOnlyDictionary<TimeIntervals, string> TimeIntervalsRus
          = new ReadOnlyDictionary<TimeIntervals, string>(new Dictionary<TimeIntervals, string>()
          {
                {TimeIntervals.Millisecond, "мс"},
                {TimeIntervals.Second, "сек"},
                {TimeIntervals.Minute, "мин"},
                {TimeIntervals.Hour, "час"}
          });

        public IReadOnlyDictionary<TimeIntervals, string> TimeIntervalsDisplay
        {
            get => (IReadOnlyDictionary<TimeIntervals, string>)GetValue(TimeIntervalsDisplayProperty);
            set => SetValue(TimeIntervalsDisplayProperty, value);
        }

        public static readonly DependencyProperty TimeIntervalsDisplayProperty =
            DependencyProperty.Register(
                nameof(TimeIntervalsDisplay),
                typeof(IReadOnlyDictionary<TimeIntervals, string>),
                typeof(NumericUpDownTime),
                new PropertyMetadata(TimeIntervalsRus));

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
        #endregion

    }
    public static class SelectorHelper
    {
        public static IEnumerable GetItemsSource(Selector selector)
        {
            return (IEnumerable)selector.GetValue(ItemsSourceProperty);
        }

        public static void SetItemsSource(Selector selector, IEnumerable value)
        {
            selector.SetValue(ItemsSourceProperty, value);
        }

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.RegisterAttached(
                nameof(GetItemsSource)[3..],
                typeof(IEnumerable),
                typeof(SelectorHelper),
                new PropertyMetadata(null)
                {
                    PropertyChangedCallback = (d, e) =>
                    {
                        Selector selector = (Selector)d;
                        selector.SetBinding(ItemsControl.ItemsSourceProperty, ItemsSourceBinding);
                        var exp = BindingOperations.GetBindingExpressionBase(selector, Selector.SelectedValueProperty);
                        exp?.UpdateTarget();
                    }
                });
        private static readonly Binding ItemsSourceBinding = new Binding()
        {
            Path = new PropertyPath(ItemsSourceProperty),
            RelativeSource = RelativeSource.Self
        };

    }

    public class TimeIntervalsDictionary : Dictionary<TimeIntervals, string>
    { }

}
