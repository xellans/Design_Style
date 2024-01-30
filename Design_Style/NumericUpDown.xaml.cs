using System;
using System.Collections.Generic;
using System.Linq;
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
            CommandManager.RegisterClassCommandBinding(typeof(NumericUpDown), new(UpValueCommand, OnUpValueExecute, OnUpDownValueCanExecute));
            CommandManager.RegisterClassCommandBinding(typeof(NumericUpDown), new(DownValueCommand, OnDownValueExecute, OnUpDownValueCanExecute));
        }
        public static readonly RoutedCommand UpValueCommand = new("UpValue", typeof(NumericUpDown));
        public static readonly RoutedCommand DownValueCommand = new("DownValue", typeof(NumericUpDown));

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

        #region Логика NumericUpDown
        private static void OnUpValueExecute(object sender, ExecutedRoutedEventArgs e)
        {
            NumericUpDown numericUpDown = (NumericUpDown)sender;
            numericUpDown.Value += numericUpDown.SmallChange;
        }

        private static void OnUpDownValueCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            NumericUpDown numericUpDown = (NumericUpDown)sender;
            e.CanExecute = numericUpDown.SmallChange > 0.0;
        }

        private static void OnDownValueExecute(object sender, ExecutedRoutedEventArgs e)
        {
            NumericUpDown numericUpDown = (NumericUpDown)sender;
            numericUpDown.Value -= numericUpDown.SmallChange;
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            var NumericTextBox = GetTemplateChild("NumericTextBox") as TextBox;
            if (NumericTextBox != null)
            {
                NumericTextBox.PreviewKeyDown += TextBox_PreviewKeyUpEvent;
                NumericTextBox.TextChanged += TextBox_TextChanged;
                NumericTextBox.MouseWheel += TextBox_MouseWheel;
            }
        }
        /// <summary>
        /// Отлов вращения колесика мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            var textbox = (TextBox)sender;
            if (e.Delta > 0)
            {
                Value += SmallChange;
                SavePositionCursor(textbox);
            }
            else
            {
                Value -= SmallChange;
                SavePositionCursor(textbox);
            }
        }
        /// <summary>
        /// Отлов нажатий клавиш Up и Down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_PreviewKeyUpEvent(object sender, KeyEventArgs e)
        {
            var textbox = (TextBox)sender;
            if (e.Key == Key.Up)
            {
                Value += SmallChange;
                SavePositionCursor(textbox);
            }
            if (e.Key == Key.Down)
            {
                Value -= SmallChange;
                SavePositionCursor(textbox);
            }
        }
        /// <summary>
        /// Проверка изменения текста, если текст не подходит под тип double, то сбрасываем значение в 0
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textbox = (TextBox)sender;
            if (textbox == null)
                return;
            long temp = 0;
            if (!string.IsNullOrWhiteSpace(textbox.Text))
            {
                if (Minimum < 0 && textbox.Text == "-") // Если минимум отрицательное число, тогда позволяем пользователю ввести знак минус.
                    return;
                if (long.TryParse(textbox.Text, out temp))
                {
                    if (textbox.Text.Length > 1 && textbox.Text[0] == '0') //Если пользователь ввёл значение по типу: 01, тогда удаляем 0 в самом начале
                    {
                        Value = temp;
                        textbox.Text = Value.ToString();
                        SavePositionCursor(textbox);
                    }
                    if (temp < Minimum) // Если значение превышает минимум сбрасываем Value до минимального значения
                    {
                        Value = Minimum;
                        textbox.Text = Value.ToString();
                        SavePositionCursor(textbox);
                    }
                    if (temp > Maximum) // Если значение превышает максим сбрасываем Value до максимального значения
                    {
                        Value = Maximum;
                        textbox.Text = Value.ToString();
                        SavePositionCursor(textbox);
                    }
                }
                else
                {
                    if (Minimum < 0 && textbox.Text == "-") // Если минимум отрицательное число, тогда позволяем пользователю ввести знак минус.
                        return;
                    else
                    {
                        textbox.Text = textbox.Text.Remove(textbox.Text.Length - 1); //Если пользователь пишет любой символ кроме цифры, то удаляем этот символ
                        SavePositionCursor(textbox);
                    }
                }
            }
        }
        // сохраняем текущую позицию курсора  устанавливаем курсор в конец строки
        private void SavePositionCursor(TextBox textbox) => textbox.Select(textbox.Text.Length, 0);
        #endregion
    }
}
