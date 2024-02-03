using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace Design_Style
{
    public class NumericUpDown : RangeBase
    {
        static NumericUpDown()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NumericUpDown), new FrameworkPropertyMetadata(typeof(NumericUpDown)));
            SmallChangeProperty.OverrideMetadata(typeof(NumericUpDown), new FrameworkPropertyMetadata(1.0));

            CornerRadiusProperty.OverrideMetadata(typeof(NumericUpDown), new FrameworkPropertyMetadata(CornerRadiusProperty.GetMetadata(typeof(FrameworkElement)).DefaultValue));
            BackgroundButtonProperty.OverrideMetadata(typeof(NumericUpDown), new FrameworkPropertyMetadata(BackgroundButtonProperty.GetMetadata(typeof(FrameworkElement)).DefaultValue));
            BackgroundButtonMouseOverProperty.OverrideMetadata(typeof(NumericUpDown), new FrameworkPropertyMetadata(BackgroundButtonMouseOverProperty.GetMetadata(typeof(FrameworkElement)).DefaultValue));
            BackgroundArrowProperty.OverrideMetadata(typeof(NumericUpDown), new FrameworkPropertyMetadata(BackgroundButtonProperty.GetMetadata(typeof(FrameworkElement)).DefaultValue));
            BackgroundArrowMouseOverProperty.OverrideMetadata(typeof(NumericUpDown), new FrameworkPropertyMetadata(BackgroundArrowMouseOverProperty.GetMetadata(typeof(FrameworkElement)).DefaultValue));
        }

        #region Dp свойства для персонализации

        #region CornerRadius радиус закругления
        public static readonly DependencyProperty CornerRadiusProperty = StyleDataHelper.CornerRadiusProperty;

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        #endregion

        #region BackgroundButton цвет кнопки
        public static readonly DependencyProperty BackgroundButtonProperty = StyleDataHelper.BackgroundButtonProperty;

        public SolidColorBrush BackgroundButton
        {
            get { return (SolidColorBrush)GetValue(BackgroundButtonProperty); }
            set { SetValue(BackgroundButtonProperty, value); }
        }
        #endregion

        #region BackgroundButtonMouseOver цвет кнопки при наведение мыши
        public static readonly DependencyProperty BackgroundButtonMouseOverProperty = StyleDataHelper.BackgroundButtonMouseOverProperty;

        public SolidColorBrush BackgroundButtonMouseOver
        {
            get { return (SolidColorBrush)GetValue(BackgroundButtonMouseOverProperty); }
            set { SetValue(BackgroundButtonMouseOverProperty, value); }
        }
        #endregion

        #region BackgroundArrow цвет стрелочки
        public static readonly DependencyProperty BackgroundArrowProperty = StyleDataHelper.BackgroundArrowProperty;

        public SolidColorBrush BackgroundArrow
        {
            get { return (SolidColorBrush)GetValue(BackgroundArrowProperty); }
            set { SetValue(BackgroundArrowProperty, value); }
        }
        #endregion

        #region BackgroundArrowMouseOver цвет стрелочки при наведение мыши
        public static readonly DependencyProperty BackgroundArrowMouseOverProperty = StyleDataHelper.BackgroundArrowMouseOverProperty;

        public SolidColorBrush BackgroundArrowMouseOver
        {
            get { return (SolidColorBrush)GetValue(BackgroundArrowMouseOverProperty); }
            set { SetValue(BackgroundArrowMouseOverProperty, value); }
        }
        #endregion
        #endregion
    }
    public static class StyleDataHelper
    {

        #region Dp свойства для персонализации

        #region CornerRadius радиус закругления
        public static readonly DependencyProperty CornerRadiusProperty
            = DependencyProperty.RegisterAttached(nameof(SetCornerRadius)[3..],
                                          typeof(CornerRadius),
                                          typeof(FrameworkElement),
                                          new PropertyMetadata(new CornerRadius(6.0)));

        public static void SetCornerRadius(UIElement element, CornerRadius value) => element.SetValue(CornerRadiusProperty, value);

        public static CornerRadius GetCornerRadius(UIElement element) => (CornerRadius)element.GetValue(CornerRadiusProperty);
        #endregion

        #region BackgroundButton цвет кнопки
        public static readonly DependencyProperty BackgroundButtonProperty
            = DependencyProperty.RegisterAttached(nameof(SetBackgroundButton)[3..],
                                                  typeof(SolidColorBrush),
                                                  typeof(FrameworkElement),
                                                  new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F1F1F1"))));

        public static void SetBackgroundButton(UIElement element, SolidColorBrush value) => element.SetValue(BackgroundButtonProperty, value);

        public static SolidColorBrush GetBackgroundButton(UIElement element) => (SolidColorBrush)element.GetValue(BackgroundButtonProperty);
        #endregion

        #region BackgroundButtonMouseOver цвет кнопки при наведение мыши
        public static readonly DependencyProperty BackgroundButtonMouseOverProperty
            = DependencyProperty.RegisterAttached(nameof(SetBackgroundButtonMouseOver)[3..],
                                          typeof(SolidColorBrush),
                                          typeof(FrameworkElement),
                                          new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#46BCFF"))));
        public static void SetBackgroundButtonMouseOver(UIElement element, SolidColorBrush value) => element.SetValue(BackgroundButtonMouseOverProperty, value);

        public static SolidColorBrush GetBackgroundButtonMouseOver(UIElement element) => (SolidColorBrush)element.GetValue(BackgroundButtonMouseOverProperty);
        #endregion

        #region BackgroundArrow цвет стрелочки
        public static readonly DependencyProperty BackgroundArrowProperty
            = DependencyProperty.RegisterAttached(nameof(SetBackgroundArrow)[3..],
                                                  typeof(SolidColorBrush),
                                                  typeof(FrameworkElement),
                                                  new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#BBBBBB"))));

        public static void SetBackgroundArrow(UIElement element, SolidColorBrush value) => element.SetValue(BackgroundArrowProperty, value);

        public static SolidColorBrush GetBackgroundArrow(UIElement element) => (SolidColorBrush)element.GetValue(BackgroundArrowProperty);
        #endregion

        #region BackgroundArrowMouseOver цвет стрелочки при наведение мыши
        public static readonly DependencyProperty BackgroundArrowMouseOverProperty
            = DependencyProperty.RegisterAttached(nameof(SetBackgroundArrowMouseOver)[3..],
                                                  typeof(SolidColorBrush),
                                                  typeof(FrameworkElement),
                                                  new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"))));
        public static void SetBackgroundArrowMouseOver(UIElement element, SolidColorBrush value) => element.SetValue(BackgroundArrowMouseOverProperty, value);

        public static SolidColorBrush GetBackgroundArrowMouseOver(UIElement element) => (SolidColorBrush)element.GetValue(BackgroundArrowMouseOverProperty);
        #endregion
        #endregion
    }

    public class DoubleTextBox : TextBox
    {
        private RangeBase? templatedParent;
        protected override void OnVisualParentChanged(DependencyObject oldParent)
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

            string text = Text;
            if (!string.IsNullOrWhiteSpace(text))
            {
                bool isNegative = IsNegative;
                if (isNegative && Text == "-") // Если минимум отрицательное число, тогда позволяем пользователю ввести знак минус.
                    return;
                if (!long.TryParse(text, out _))
                {
                    if (isNegative && text == "-") // Если минимум отрицательное число, тогда позволяем пользователю ввести знак минус.
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
                command.Execute(null, target);
        }

        protected override void OnPreviewKeyUp(KeyEventArgs e)
        {
            base.OnPreviewKeyUp(e);
            RaiseCommand(this, e.Key == Key.Up ? true : e.Key == Key.Down ? false : null);
            SavePositionCursor(this);
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
