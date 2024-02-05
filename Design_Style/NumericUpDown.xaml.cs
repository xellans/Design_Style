using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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
        private RangeBase? templatedParent;
        protected override async void OnVisualParentChanged(DependencyObject oldParent)
        {
            base.OnVisualParentChanged(oldParent);

            var _parent = TemplatedParent as RangeBase;
            if (templatedParent != _parent)
            {
                if (templatedParent != null)
                {
                    if (BindingOperations.GetBinding(this, TextProperty) == TextTemplateBinding)
                    {
                        BindingOperations.ClearBinding(this, TextProperty);
                    }
                    if (BindingOperations.GetBinding(this, IsNegativeProperty) == IsNegativeTemplateBinding)
                    {
                        BindingOperations.ClearBinding(this, IsNegativeProperty);
                    }
                }
                templatedParent = _parent;
                if (templatedParent != null)
                {
                    if (!IsLoaded)
                    {
                        TaskCompletionSource taskSource = new();
                        Loaded += delegate { taskSource.SetResult(); };
                        await taskSource.Task;
                    }
                    if (DependencyPropertyHelper.GetValueSource(this, TextProperty).BaseValueSource == BaseValueSource.Default)
                        SetBinding(TextProperty, TextTemplateBinding);

                    if (DependencyPropertyHelper.GetValueSource(this, IsNegativeProperty).BaseValueSource == BaseValueSource.Default)
                        SetBinding(IsNegativeProperty, IsNegativeTemplateBinding);
                }
            }
        }

        private static readonly Binding TextTemplateBinding = new Binding()
        {
            Path = new PropertyPath(RangeBase.ValueProperty),
            RelativeSource = RelativeSource.TemplatedParent,
            Mode = BindingMode.TwoWay
        };

        private static readonly Binding IsNegativeTemplateBinding = new Binding()
        {
            Path = new PropertyPath(RangeBase.MinimumProperty),
            RelativeSource = RelativeSource.TemplatedParent,
            Mode = BindingMode.OneWay
        };

        public IInputElement CommandTarget
        {
            get { return (IInputElement)GetValue(CommandTargetProperty); }
            set { SetValue(CommandTargetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CommandTarget.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandTargetProperty =
            DependencyProperty.Register(nameof(CommandTarget), typeof(IInputElement), typeof(DoubleTextBox), new PropertyMetadata(null));



        [TypeConverter(typeof(IsNegativeConverter))]
        public bool IsNegative
        {
            get { return (bool)GetValue(IsNegativeProperty); }
            set { SetValue(IsNegativeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsNegative.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsNegativeProperty =
            DependencyProperty.Register(nameof(IsNegative), typeof(bool), typeof(DoubleTextBox), new PropertyMetadata(false));

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);
            long temp = 0;
            if (templatedParent == null)
                throw new InvalidOperationException("Error binding Range, templatedParent = null");
            if (!string.IsNullOrWhiteSpace(Text))
            {
                if (templatedParent.Minimum < 0 && Text == "-") // Если минимум отрицательное число, тогда позволяем пользователю ввести знак минус.
                    return;
                if (long.TryParse(Text, out temp))
                {
                    if (Text.Length > 1 && Text[0] == '0') //Если пользователь ввёл значение по типу: 01, тогда удаляем 0 в самом начале
                    {
                        templatedParent.Value = temp;
                        Text = templatedParent.Value.ToString();
                        SavePositionCursor(this);
                        return;
                    }
                    if (temp < templatedParent.Minimum) // Если значение превышает минимум сбрасываем Value до минимального значения
                    {
                        templatedParent.Value = templatedParent.Minimum;
                        Text = templatedParent.Value.ToString();
                        SavePositionCursor(this);
                        return;
                    }
                    if (temp > templatedParent.Maximum) // Если значение превышает максим сбрасываем Value до максимального значения
                    {
                        templatedParent.Value = templatedParent.Maximum;
                        Text = templatedParent.Value.ToString();
                        SavePositionCursor(this);
                        return;
                    }
                    templatedParent.Value = temp;
                }
                else
                {
                    if (templatedParent.Minimum < 0 && Text == "-") // Если минимум отрицательное число, тогда позволяем пользователю ввести знак минус.
                        return;
                    else
                    {
                        Text = Text.Remove(Text.Length - 1); //Если пользователь пишет любой символ кроме цифры, то удаляем этот символ
                        SavePositionCursor(this);
                    }
                }
            }
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);
            RaiseCommand(this, e.Delta > 0);
            SavePositionCursor(this);
        }

        private static void RaiseCommand(DoubleTextBox tbox, bool? isUp)
        {
            if (isUp is null)
                return;
            RoutedCommand command = isUp.Value
                ? UpDownButton.UpCommand
                : UpDownButton.DownCommand;
            IInputElement target = tbox.CommandTarget ?? tbox;

            if (command.CanExecute(null, target))
            {
                command.Execute(null, target);
                SavePositionCursor(tbox);
            }
        }
        protected override void OnPreviewKeyUp(KeyEventArgs e)
        {
            base.OnPreviewKeyUp(e);
            RaiseCommand(this, e.Key == Key.Up ? true : e.Key == Key.Down ? false : null);
        }

        private static void SavePositionCursor(TextBox textbox) => textbox.Select(textbox.Text.Length, 0);

    }

    public class IsNegativeConverter : BooleanConverter
    {
        private static readonly DoubleConverter doubleConverter = new DoubleConverter();
        public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
        {
            if (!CanConvertFrom(value.GetType()) && doubleConverter.CanConvertFrom(value.GetType()))
            {

                try
                {
                    double number = (double)(doubleConverter.ConvertFrom(context, culture, value) ?? 0.0);
                    return number < 0.0;
                }
                catch { }
            }

            return base.ConvertFrom(context, culture, value);
        }

        public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
        {
            return base.CanConvertFrom(context, sourceType)
                ? true : doubleConverter.CanConvertFrom(context, sourceType);
        }

        public override bool IsValid(ITypeDescriptorContext? context, object? value)
        {
            return base.IsValid(context, value)
                ? true : doubleConverter.IsValid(context, value);
        }
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
