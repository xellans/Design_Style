using Microsoft.VisualBasic;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Design_Style
{
    //public record UnitToDisplay(object? Unit, object Display);
    //public class UnitToDisplayCollection : ReadOnlyCollection<UnitToDisplay>
    //{
    //    public UnitToDisplayCollection(IEnumerable<UnitToDisplay> units)
    //        : base(CopyToList(units))
    //    { }

    //    public static List<UnitToDisplay> CopyToList(IEnumerable<UnitToDisplay> units)
    //    {
    //        int count = 5;
    //        if (units is ICollection collection)
    //        {
    //            count = collection.Count;
    //        }
    //        HashSet<object?> set = new(count);
    //        List<UnitToDisplay> list = new(count);

    //        foreach (UnitToDisplay unit in units)
    //        {
    //            if (!set.Add(unit.Unit))
    //            {
    //                throw new ArgumentException("Все единицы измерения должны быть уникальны.", nameof(units));
    //            }
    //            list.Add(unit);
    //        }
    //        list.TrimExcess();
    //        return list;
    //    }
    //}

    public class DoubleUnitConverter
    {
        /// <summary>Возвращает значение в целевых единицах измерения.</summary>
        /// <param name="value">Значение в оригинальных единицах измерения.</param>
        /// <param name="targetUnit">Целевая единица измерения.</param>
        /// <returns>Значение в целевых единицах измерения.</returns>
        public double Convert(double value, object? targetUnit)
        {
            double result = value / GetDirectConversionFactor(targetUnit);
            return result;
        }

        /// <summary>Возвращает значение в оригинальных единицах измерения.</summary>
        /// <param name="value">Значение в целевых единицах измерения.</param>
        /// <param name="targetUnit">Целевая единица измерения.</param>
        /// <returns>Значение в оригинальных единицах измерения.</returns>
        public double ConvertBack(double value, object? targetUnit)
        {
            double result = value * GetDirectConversionFactor(targetUnit);
            return result;
        }

        /// <summary>Возвращает коэфициент конвертации из указанной единицы измерения в оригинальную.<br/>
        /// Базовая реализация конвертирует 1 к 1.</summary>
        /// <param name="targetUnit">Единица измерения.</param>
        /// <returns>Коэффициент конвертации.<br/>
        /// Для базовой реализации - всегда 1.0.</returns>
        protected virtual double GetDirectConversionFactor(object? targetUnit) => 1.0;

        /// <summary>Базовый экземпляр конвертера по умолчанию. Обеспечивает конвертацию всегда 1 к 1.</summary>
        public static DoubleUnitConverter Default { get; } = new();
    }
    public partial class NumericUpDownUnits1 : NumericUpDown
    {

        /// <summary>Оригинальное, истиное значение.</summary>
        public double OriginalValue
        {
            get => (double)GetValue(OriginalValueProperty);
            set => SetValue(OriginalValueProperty, value);
        }

        // Using a DependencyProperty as the backing store for OriginalValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OriginalValueProperty =
            DependencyProperty.Register(
                nameof(OriginalValue),
                typeof(double),
                typeof(NumericUpDownUnits1),
                new FrameworkPropertyMetadata(0.0)
                {
                    BindsTwoWayByDefault = true,
                    PropertyChangedCallback = OnOriginalValueChanged
                });

        private double privateOriginalValue;
        private static void OnOriginalValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            NumericUpDownUnits1 nudu = (NumericUpDownUnits1)d;
            double value = (double)e.NewValue;
            nudu.privateOriginalValue = value;
            nudu.OnOriginalValueChanged();
        }

        private void OnOriginalValueChanged()
        {
            Value = privateConverter.Convert(privateOriginalValue, privateSelectedUnit);
        }

        public DoubleUnitConverter Converter
        {
            get => (DoubleUnitConverter)GetValue(ConverterProperty);
            set => SetValue(ConverterProperty, value);
        }

        // Using a DependencyProperty as the backing store for Converter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ConverterProperty =
            DependencyProperty.Register(
                nameof(Converter),
                typeof(DoubleUnitConverter),
                typeof(NumericUpDownUnits1),
                new PropertyMetadata(null)
                {
                    CoerceValueCallback = (_, v) => v ?? DoubleUnitConverter.Default,
                    PropertyChangedCallback = OnConverterChanged
                });

        // Поле для оптимицаци: Текущее значение конвертера.
        // Такие же поля добавлены для всех свойств.
        private DoubleUnitConverter privateConverter;

        private static void OnConverterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            NumericUpDownUnits1 nudu = (NumericUpDownUnits1)d;
            nudu.privateConverter = (DoubleUnitConverter)e.NewValue;
            nudu.OnValueOrUnitOrConverterChanged();
        }

        public NumericUpDownUnits1()
        {
            privateConverter = DoubleUnitConverter.Default;
            ValueProperty.OverrideMetadata(typeof(NumericUpDownUnits1), new FrameworkPropertyMetadata()
            {
                PropertyChangedCallback = OnValueChanged,
            });
        }

        private void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            NumericUpDownUnits1 nudu = (NumericUpDownUnits1)d;
            double value = (double)e.NewValue;
            nudu.privateValue = value;
            nudu.OnValueOrUnitOrConverterChanged();
        }

        private double privateValue;
        private void OnValueOrUnitOrConverterChanged()
        {
            if (IsLoaded)
            {
                OriginalValue = privateConverter.ConvertBack(privateValue, privateSelectedUnit);
            }
        }

        /// <summary>Выбранная единица измерения.</summary>
        public object SelectedUnit
        {
            get => GetValue(SelectedUnitProperty);
            set => SetValue(SelectedUnitProperty, value);
        }

        // Using a DependencyProperty as the backing store for SelectedUnit.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedUnitProperty =
            DependencyProperty.Register(nameof(SelectedUnit),
                                        typeof(object),
                                        typeof(NumericUpDownUnits1),
                                        new FrameworkPropertyMetadata(null)
                                        {
                                            BindsTwoWayByDefault = true,
                                            PropertyChangedCallback = SelectedUnitChanged
                                        });

        private object? privateSelectedUnit;
        private static void SelectedUnitChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // Сохранение выбранной единицы измеренеие и вызов пересчёта единиц.
            NumericUpDownUnits1 nudu = (NumericUpDownUnits1)d;
            nudu.privateSelectedUnit = e.NewValue;

            nudu.OnValueOrUnitOrConverterChanged();
        }
        static NumericUpDownUnits1() => DefaultStyleKeyProperty.OverrideMetadata(typeof(NumericUpDownUnits1), new FrameworkPropertyMetadata(typeof(NumericUpDownUnits1)));

        /// <summary>Словарь единиц измерений с их представлением и конвертерами.</summary>
        /// <remarks>Возможно следует разделить на два словаря: представления и конвертера.</remarks>
        public IDictionary Units
        {
            get { return (IDictionary)GetValue(UnitsProperty); }
            set { SetValue(UnitsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Units.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UnitsProperty =
            DependencyProperty.Register(nameof(Units), typeof(IDictionary), typeof(NumericUpDownUnits1), new FrameworkPropertyMetadata(null));
    }
}
