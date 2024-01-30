using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace Design_Style
{
    public class CheckBoxBase
    {
        #region BackgroundChecked цвет фона CheckBox при событии Checked
        public static readonly DependencyProperty BackgroundCheckedProperty =
            DependencyProperty.RegisterAttached(
                "BackgroundChecked",
                typeof(SolidColorBrush),
                typeof(CheckBoxBase),
                new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#46BCFF"))));

        public static SolidColorBrush GetBackgroundChecked(UIElement element) => (SolidColorBrush)element.GetValue(BackgroundCheckedProperty);

        public static void SetBackgroundChecked(UIElement element, SolidColorBrush value) => element.SetValue(BackgroundCheckedProperty, value);
        #endregion

        #region ToggleableChecked цвет фона круга|стрелочки
        public static readonly DependencyProperty ToggleableCheckedProperty =
            DependencyProperty.RegisterAttached(
                "ToggleableChecked",
                typeof(SolidColorBrush),
                typeof(CheckBoxBase),
                new PropertyMetadata(new SolidColorBrush(Colors.White)));

        public static SolidColorBrush GetToggleableChecked(UIElement element) => (SolidColorBrush)element.GetValue(ToggleableCheckedProperty);

        public static void SetToggleableChecked(UIElement element, SolidColorBrush value) => element.SetValue(ToggleableCheckedProperty, value);
        #endregion

        #region CornerRadius радиус закругления CheckBox
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.RegisterAttached(
                nameof(GetCornerRadius)[3..],
                typeof(CornerRadius),
                typeof(CheckBoxBase),
                new PropertyMetadata(new CornerRadius(6.0)));

        public static CornerRadius GetCornerRadius(UIElement element) => (CornerRadius)element.GetValue(CornerRadiusProperty);

        public static void SetCornerRadius(UIElement element, CornerRadius value) => element.SetValue(CornerRadiusProperty, value);
        #endregion
    }
}
