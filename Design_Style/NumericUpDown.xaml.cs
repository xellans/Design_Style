using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Design_Style
{
    public class NumericUpDown : RangeBase
    {
        static NumericUpDown()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NumericUpDown), new FrameworkPropertyMetadata(typeof(NumericUpDown)));
            SmallChangeProperty.OverrideMetadata(typeof(NumericUpDown), new FrameworkPropertyMetadata(1.0));
        }

        #region Dp свойства для персонализации

        #region CornerRadius радиус закругления
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
        nameof(CornerRadius),
        typeof(CornerRadius),
        typeof(NumericUpDown), new PropertyMetadata(new CornerRadius(6.0)));

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        public static void SetCornerRadius(UIElement element, CornerRadius value) => element.SetValue(CornerRadiusProperty, value);

        public static CornerRadius GetCornerRadius(UIElement element) => (CornerRadius)element.GetValue(CornerRadiusProperty);
        #endregion

        #region BackgroundButton цвет кнопки
        public static readonly DependencyProperty BackgroundButtonProperty = DependencyProperty.Register(
        nameof(BackgroundButton),
        typeof(SolidColorBrush),
        typeof(NumericUpDown), new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F1F1F1"))));

        public SolidColorBrush BackgroundButton
        {
            get { return (SolidColorBrush)GetValue(BackgroundButtonProperty); }
            set { SetValue(BackgroundButtonProperty, value); }
        }
        public static void SetBackgroundButton(UIElement element, SolidColorBrush value) => element.SetValue(BackgroundButtonProperty, value);

        public static SolidColorBrush GetBackgroundButton(UIElement element) => (SolidColorBrush)element.GetValue(BackgroundButtonProperty);
        #endregion

        #region BackgroundButtonMouseOver цвет кнопки при наведение мыши
        public static readonly DependencyProperty BackgroundButtonMouseOverProperty = DependencyProperty.Register(
        nameof(BackgroundButtonMouseOver),
        typeof(SolidColorBrush),
        typeof(NumericUpDown), new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#46BCFF"))));

        public SolidColorBrush BackgroundButtonMouseOver
        {
            get { return (SolidColorBrush)GetValue(BackgroundButtonMouseOverProperty); }
            set { SetValue(BackgroundButtonMouseOverProperty, value); }
        }
        public static void SetBackgroundButtonMouseOver(UIElement element, SolidColorBrush value) => element.SetValue(BackgroundButtonMouseOverProperty, value);

        public static SolidColorBrush GetBackgroundButtonMouseOver(UIElement element) => (SolidColorBrush)element.GetValue(BackgroundButtonMouseOverProperty);
        #endregion

        #region BackgroundArrow цвет стрелочки
        public static readonly DependencyProperty BackgroundArrowProperty = DependencyProperty.Register(
        nameof(BackgroundArrow),
        typeof(SolidColorBrush),
        typeof(NumericUpDown), new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#BBBBBB"))));

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
        typeof(NumericUpDown), new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"))));

        public SolidColorBrush BackgroundArrowMouseOver
        {
            get { return (SolidColorBrush)GetValue(BackgroundArrowMouseOverProperty); }
            set { SetValue(BackgroundArrowMouseOverProperty, value); }
        }
        public static void SetBackgroundArrowMouseOver(UIElement element, SolidColorBrush value) => element.SetValue(BackgroundArrowMouseOverProperty, value);

        public static SolidColorBrush GetBackgroundArrowMouseOver(UIElement element) => (SolidColorBrush)element.GetValue(BackgroundArrowMouseOverProperty);
        #endregion
        #endregion
    }

    public class DoubleTextBox : TextBox
    {
        public DoubleTextBox()
        {
            SetBinding(MaximumProperty, MaximumBinding);
            SetBinding(MinimumProperty, MinimumBinding);
            SetBinding(ValueProperty, ValueBinding);
            SetBinding(SmallChangeProperty, SmallChangeBinding);
            SetBinding(TextProperty, TextBinding);
        }

        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        public static readonly DependencyProperty ValueProperty = RangeBase.ValueProperty;

        public double Maximum
        {
            get { return (double)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }
        public static readonly DependencyProperty MaximumProperty = RangeBase.MaximumProperty;

        public double Minimum
        {
            get { return (double)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }
        public static readonly DependencyProperty MinimumProperty = RangeBase.MinimumProperty;
        public double SmallChange
        {
            get { return (double)GetValue(SmallChangeProperty); }
            set { SetValue(SmallChangeProperty, value); }
        }
        public static readonly DependencyProperty SmallChangeProperty = RangeBase.SmallChangeProperty;


        private static readonly Binding MaximumBinding = new Binding()
        {
            Path = new PropertyPath(MaximumProperty),
            RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor, typeof(RangeBase), 1)
        };
        private static readonly Binding MinimumBinding = new Binding()
        {
            Path = new PropertyPath(MinimumProperty),
            RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor, typeof(RangeBase), 1)
        };
        private static readonly Binding ValueBinding = new Binding()
        {
            Path = new PropertyPath(ValueProperty),
            RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor, typeof(RangeBase), 1),
            Mode = BindingMode.TwoWay
        };
        private static readonly Binding SmallChangeBinding = new Binding()
        {
            Path = new PropertyPath(SmallChangeProperty),
            RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor, typeof(RangeBase), 1)
        };
        private static readonly Binding TextBinding = new Binding()
        {
            Path = new PropertyPath(ValueProperty),
            RelativeSource = RelativeSource.Self,
            Mode = BindingMode.TwoWay
        };


        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);

            long temp = 0;
            string text = Text;
            if (!string.IsNullOrWhiteSpace(text))
            {
                if (Minimum < 0 && Text == "-") // Если минимум отрицательное число, тогда позволяем пользователю ввести знак минус.
                    return;
                if (long.TryParse(text, out temp))
                {
                    if (text.Length > 1 && text[0] == '0') //Если пользователь ввёл значение по типу: 01, тогда удаляем 0 в самом начале
                    {
                        Value = temp;
                        text = Value.ToString();
                        SavePositionCursor(this);
                    }
                    if (temp < Minimum) // Если значение превышает минимум сбрасываем Value до минимального значения
                    {
                        Value = Minimum;
                        text = Value.ToString();
                        SavePositionCursor(this);
                    }
                    if (temp > Maximum) // Если значение превышает максим сбрасываем Value до максимального значения
                    {
                        Value = Maximum;
                        text = Value.ToString();
                        SavePositionCursor(this);
                    }
                }
                else
                {
                    if (Minimum < 0 && text == "-") // Если минимум отрицательное число, тогда позволяем пользователю ввести знак минус.
                        return;
                    else
                    {
                        Text = text.Remove(text.Length - 1); //Если пользователь пишет любой символ кроме цифры, то удаляем этот символ
                        SavePositionCursor(this);
                    }
                }
            }
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);

            if (e.Delta > 0)
            {
                Value += SmallChange;
                SavePositionCursor(this);
            }
            else
            {
                Value -= SmallChange;
                SavePositionCursor(this);
            }
        }

        protected override void OnPreviewKeyUp(KeyEventArgs e)
        {
            base.OnPreviewKeyUp(e);

            if (e.Key == Key.Up)
            {
                Value += SmallChange;
                SavePositionCursor(this);
            }
            if (e.Key == Key.Down)
            {
                Value -= SmallChange;
                SavePositionCursor(this);
            }

        }

        private static void SavePositionCursor(TextBox textbox) => textbox.Select(textbox.Text.Length, 0);

    }
    public static class UpDownButton
    {
        public static RoutedCommand UpCommand { get; } = new("Up", typeof(UpDownButton));
        public static RoutedCommand DownCommand { get; } = new("Down", typeof(UpDownButton));

        static UpDownButton()
        {
            CommandManager.RegisterClassCommandBinding(typeof(RangeBase), new(UpCommand, OnUpValueExecute, OnUpDownValueCanExecute));
            CommandManager.RegisterClassCommandBinding(typeof(RangeBase), new(DownCommand, OnDownValueExecute, OnUpDownValueCanExecute));

            CommandManager.RegisterClassCommandBinding(typeof(Selector), new(UpCommand, OnUpIndexExecute));
            CommandManager.RegisterClassCommandBinding(typeof(Selector), new(DownCommand, OnDownIndexExecute));

            CommandManager.RegisterClassCommandBinding(typeof(ToggleButton), new(UpCommand, OnUpToggleExecute));
            CommandManager.RegisterClassCommandBinding(typeof(ToggleButton), new(DownCommand, OnDownToogleExecute));
        }

        #region Логика Up-Down ToggleButton
        private static void OnDownToogleExecute(object sender, ExecutedRoutedEventArgs e)
        {
            ToggleButton toggle = (ToggleButton)sender;
            toggle.IsChecked = toggle.IsChecked switch
            {
                false => true,
                null => false,
                true => null
            };
        }

        private static void OnUpToggleExecute(object sender, ExecutedRoutedEventArgs e)
        {
            ToggleButton toggle = (ToggleButton)sender;
            toggle.IsChecked = toggle.IsChecked switch
            {
                false => null,
                null => true,
                true => false
            };
        }
        #endregion

        #region Логика Up-Down RangeBase
        private static void OnUpValueExecute(object sender, ExecutedRoutedEventArgs e)
        {
            RangeBase numericUpDown = (RangeBase)sender;
            numericUpDown.Value += numericUpDown.SmallChange;
        }

        private static void OnUpDownValueCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            RangeBase numericUpDown = (RangeBase)sender;
            e.CanExecute = numericUpDown.SmallChange > 0.0;
        }

        private static void OnDownValueExecute(object sender, ExecutedRoutedEventArgs e)
        {
            RangeBase numericUpDown = (RangeBase)sender;
            numericUpDown.Value -= numericUpDown.SmallChange;
        }
        #endregion

        #region Логика Up-Down Selector
        private static void OnUpIndexExecute(object sender, ExecutedRoutedEventArgs e)
        {
            Selector selector = (Selector)sender;
            if (selector.SelectedIndex >= selector.Items.Count - 1)
            {
                selector.SelectedIndex = -1;
            }
            else
            {
                selector.SelectedIndex++;
            }
        }

        private static void OnUpDownIndexCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            Selector selector = (Selector)sender;
            e.CanExecute = selector.Items.Count > 0;
        }

        private static void OnDownIndexExecute(object sender, ExecutedRoutedEventArgs e)
        {
            Selector selector = (Selector)sender;
            if (selector.SelectedIndex < 0)
            {
                selector.SelectedIndex = selector.Items.Count - 1;
            }
            else
            {
                selector.SelectedIndex--;
            }
        }
        #endregion
    }

}
