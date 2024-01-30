using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace Design_Style
{
    public class RadioButtonRipple
    {
        #region RippleColor цвет анимации Ripple effect
        public static readonly DependencyProperty RippleColorProperty =
            DependencyProperty.RegisterAttached(
                nameof(GetRippleColor)[3..],
                typeof(Brush),
                typeof(RadioButtonRipple),
                new PropertyMetadata(Brushes.White));

        public static Brush GetRippleColor(UIElement element) => (Brush)element.GetValue(RippleColorProperty);

        public static void SetRippleColor(UIElement element, Brush value) => element.SetValue(RippleColorProperty, value);
        #endregion

        #region BackgroundMouseOver цвет фона при наведении мыши
        public static readonly DependencyProperty BackgroundMouseOverProperty =
            DependencyProperty.RegisterAttached(
                nameof(GetBackgroundMouseOver)[3..],
                typeof(SolidColorBrush),
                typeof(RadioButtonRipple),
                new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EFEFEF"))));

        public static SolidColorBrush GetBackgroundMouseOver(UIElement element) => (SolidColorBrush)element.GetValue(BackgroundMouseOverProperty);

        public static void SetBackgroundMouseOver(UIElement element, SolidColorBrush value) => element.SetValue(BackgroundMouseOverProperty, value);
        #endregion

        #region BackgroundChecked цвет фона при срабатывание события Checked
        public static readonly DependencyProperty BackgroundCheckedProperty =
            DependencyProperty.RegisterAttached(
                nameof(GetBackgroundChecked)[3..],
                typeof(SolidColorBrush),
                typeof(RadioButtonRipple),
                new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EFEFEF"))));

        public static SolidColorBrush GetBackgroundChecked(UIElement element) => (SolidColorBrush)element.GetValue(BackgroundCheckedProperty);

        public static void SetBackgroundChecked(UIElement element, SolidColorBrush value) => element.SetValue(BackgroundCheckedProperty, value);
        #endregion
    }
}
