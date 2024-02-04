using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Design_Style
{
    /// <summary>Делегат конвертера значения в представляемую единицу измерения, или в истиную.</summary>
    /// <param name="value">Конвертируемое значение.</param>
    /// <param name="culture">Культура конвертайии.</param>
    /// <param name="toTarget"><see langword="true"/> - конвертация из истиных единиц в представляемые,
    /// <see langword="false"/> - обратная конвертация.</param>
    /// <returns>Конвертированное значение.</returns>
    public delegate double UnitConverter(double value, CultureInfo culture, bool toTarget);

    /// <summary>Класс для создания связи между единицей измерения, её представлением и конвертацией в "истинные" единицы измерений.</summary>
    public record UnitData(object Unit, object Display, UnitConverter Converter);

    /// <summary>Конвертер (для привязок) значений в разных единицих измерений.</summary>
    public class ValueUnitConverter : IValueConverter
    {
        public UnitConverter? UnitConverter { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => ConvertTwo(value, targetType, parameter, culture, true);

        private object ConvertTwo(object value, Type targetType, object parameter, CultureInfo culture, bool toTarget)
        {
            object result;
            if (UnitConverter is null)
            {
                result = value;
            }
            else
            {
                double number = (double)value;
                result = UnitConverter(number, culture, toTarget);
            }
            return result;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => ConvertTwo(value, targetType, parameter, culture, false);
    }

    /// <summary>Словарь для хранения единиц измерения.</summary>
    [TypeConverter(typeof(UnitsDictionaryConverter))]
    public class UnitsCollection : ReadOnlyCollection<UnitData>
    {
        public static List<UnitData> ToList(IEnumerable<UnitData> datas)
        {
            int count = 5;
            if (datas is ICollection collection)
            {
                count = collection.Count;
            }
            List<UnitData> list = new(count);
            HashSet<object> units = new(count);
            foreach (var data in datas)
            {
                if (!units.Add(data.Unit))
                {
                    throw new ArgumentException("Все item.Unit должны быть уникальны.", nameof(datas));
                }
                list.Add(data);
            }
            list.TrimExcess();
            return list;
        }
        public UnitsCollection(IEnumerable<UnitData> datas)
            : base(ToList(datas))
        { }
    }

    /// <summary>Конвертер для конвертации "по умолчанию" в словарь единиц измерений.</summary>
    public class UnitsDictionaryConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
        {
            if (sourceType == typeof(UnitsCollection) ||
                sourceType.IsAssignableTo(typeof(IEnumerable<UnitData>)))
                return true;
            return base.CanConvertFrom(context, sourceType);
        }

        public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
        {
            if (value is UnitsCollection)
            {
                return value;
            }
            else if (value is IEnumerable<UnitData> units)
            {
                return new UnitsCollection(units);
            }
            return base.ConvertFrom(context, culture, value);
        }
    }

    public class NumericUpDownUnitsProxy : Freezable
    {
        /// <summary>Для упрощения тестирования. Потом нужно удалить.<br/>
        /// Список единиц длины с конвертацией в метры</summary>
        public static readonly UnitData[] Units
            =
            {
                new ("in", "дюйм", (v, c, t) => t? Convert.ToInt32(v / 0.0254)   : v * 0.0254 ),
                new ("mi", "миля", (v, c, t) => t? Convert.ToInt32(v / 1609.344) : v * 1609.344),
                new ("m", "метр",  (v, c, t) => Convert.ToInt32(v)),
                new ("ft", "фут",  (v, c, t) => t? Convert.ToInt32(v / 0.3048)   : v * 0.3048),
            };

        protected override Freezable CreateInstanceCore() => new NumericUpDownUnitsProxy();

        /// <summary>Оригинальное, истиное значение.</summary>
        public double OriginalValue
        {
            get { return (double)GetValue(OriginalValueProperty); }
            set { SetValue(OriginalValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OriginalValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OriginalValueProperty =
            DependencyProperty.Register(nameof(OriginalValue), typeof(double), typeof(NumericUpDownUnitsProxy), new PropertyMetadata(0.0));

        // Конверетер для привязки представляемого значения к истиному значению.
        private readonly ValueUnitConverter converter = new ValueUnitConverter();
        // Выражение привязки представляемого значения к истиному значению.
        private readonly BindingExpression BindingDisplayToOriginalExpression;

        public NumericUpDownUnitsProxy()
        {
            // Создание привязки представляемого значения к истиному значению.
            Binding bindingDisplayToOriginal = new()
            {
                RelativeSource = RelativeSource.Self,
                Path = new(OriginalValueProperty),
                Converter = converter,
                Mode = BindingMode.TwoWay
            };
            BindingOperations.SetBinding(this, DisplayValueProperty, bindingDisplayToOriginal);

            // Сохранение выражения привязки представляемого значения к истиному значению.
            BindingDisplayToOriginalExpression = BindingOperations.GetBindingExpression(this, DisplayValueProperty);
        }

        /// <summary>Представляемое значение.</summary>
        public double DisplayValue
        {
            get { return (double)GetValue(DisplayValueProperty); }
            set { SetValue(DisplayValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DisplayValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DisplayValueProperty =
            DependencyProperty.Register(nameof(DisplayValue), typeof(double), typeof(NumericUpDownUnitsProxy),
                new PropertyMetadata(0.0)
                {
                    PropertyChangedCallback = (_, e) => { object val = e.NewValue; }
                });

        /// <summary>Выбранная единица измерения.</summary>
        public UnitData SelectedUnit
        {
            get { return (UnitData)GetValue(SelectedUnitProperty); }
            set { SetValue(SelectedUnitProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedUnit.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedUnitProperty =
            DependencyProperty.Register(nameof(SelectedUnit),
                                        typeof(UnitData),
                                        typeof(NumericUpDownUnitsProxy),
                                        new PropertyMetadata(null)
                                        {
                                            PropertyChangedCallback = SelectedUnitChanged
                                        });

        private static void SelectedUnitChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // Получение конвертера выбранной единицы измерения и его запись в конвертер привязки.
            NumericUpDownUnitsProxy proxy = (NumericUpDownUnitsProxy)d;
            var selectedUnit = (UnitData)e.NewValue;
            proxy.converter.UnitConverter = selectedUnit?.Converter;

            // Обновление источники привязки (истинного значения) для новой единицы измерения.
            proxy.BindingDisplayToOriginalExpression.UpdateSource();
        }
    }
    public class NumericUpDownUnits : NumericUpDown
    {
        static NumericUpDownUnits() => DefaultStyleKeyProperty.OverrideMetadata(typeof(NumericUpDownUnits), new FrameworkPropertyMetadata(typeof(NumericUpDownUnits)));

        /// <summary>Словарь единиц измерений с их представлением и конвертерами.</summary>
        /// <remarks>Возможно следует разделить на два словаря: представления и конвертера.</remarks>
        public UnitsCollection Units
        {
            get { return (UnitsCollection)GetValue(UnitsProperty); }
            set { SetValue(UnitsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Units.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UnitsProperty =
            DependencyProperty.Register(nameof(Units), typeof(UnitsCollection), typeof(NumericUpDownUnits), new PropertyMetadata(null));



        public UnitData SelectedUnit
        {
            get { return (UnitData)GetValue(SelectedUnitProperty); }
            set { SetValue(SelectedUnitProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedUnit.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedUnitProperty =
            DependencyProperty.Register("SelectedUnit", typeof(UnitData), typeof(NumericUpDownUnits), new PropertyMetadata(null));


    }


    public class UpDownCommandBinding : CommandBinding
    {
        public UpDownCommandBinding()
        {
            Executed += OnUpDown;
            CanExecute += (s, e) => e.CanExecute = true;
        }

        private void OnUpDown(object sender, ExecutedRoutedEventArgs e)
        {
            if ((e.Command == UpDownButton.UpCommand || e.Command == UpDownButton.DownCommand) && e.Parameter is double step)
            {
                NumericUpDownUnitsProxy proxy = (NumericUpDownUnitsProxy)((FrameworkElement)sender).Resources[nameof(proxy)];
                if (e.Command == UpDownButton.DownCommand)
                {
                    step = -step;
                }
                proxy.DisplayValue += step;
            }
            e.Handled = true;
        }
    }
}
