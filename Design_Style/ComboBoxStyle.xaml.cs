using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;
using System.Globalization;
using System.Windows.Data;

namespace Design_Style
{
    public class ComboBoxStyle
    {

        #region BackgroundMouseOver цвет фона ComboBox при наведение мыши
        public static readonly DependencyProperty BackgroundMouseOverProperty =
            DependencyProperty.RegisterAttached(
                nameof(GetBackgroundMouseOver)[3..],
                typeof(SolidColorBrush),
                typeof(ComboBoxStyle),
                new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));

        public static SolidColorBrush GetBackgroundMouseOver(UIElement element) => (SolidColorBrush)element.GetValue(BackgroundMouseOverProperty);

        public static void SetBackgroundMouseOver(UIElement element, SolidColorBrush value) => element.SetValue(BackgroundMouseOverProperty, value);
        #endregion

        #region BorderBrushMouseOver цвет обводки ComboBox при наведние мыши
        public static readonly DependencyProperty BorderBrushMouseOverProperty =
            DependencyProperty.RegisterAttached(
                nameof(GetBorderBrushMouseOver)[3..],
                typeof(SolidColorBrush),
                typeof(ComboBoxStyle),
                new PropertyMetadata(new SolidColorBrush((Color)(ColorConverter.ConvertFromString("#46BCFF")))));

        public static SolidColorBrush GetBorderBrushMouseOver(UIElement element) => (SolidColorBrush)element.GetValue(BorderBrushMouseOverProperty);

        public static void SetBorderBrushMouseOver(UIElement element, SolidColorBrush value) => element.SetValue(BorderBrushMouseOverProperty, value);
        #endregion

        #region CornerRadius радиус закругления ComboBox
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.RegisterAttached(
                nameof(GetCornerRadius)[3..],
                typeof(CornerRadius),
                typeof(ComboBoxStyle),
                new PropertyMetadata(new CornerRadius(6.0)));

        public static CornerRadius GetCornerRadius(UIElement element) => (CornerRadius)element.GetValue(CornerRadiusProperty);

        public static void SetCornerRadius(UIElement element, CornerRadius value) => element.SetValue(CornerRadiusProperty, value);
        #endregion


        //Свойства Arrow(Стрелочки)
        #region Background_Arrow_Unchecked цвет фона стрелочки в обычном состоянии
        public static readonly DependencyProperty Background_Arrow_UncheckedProperty =
            DependencyProperty.RegisterAttached(
                nameof(GetBackground_Arrow_Unchecked)[3..],
                typeof(Color),
                typeof(ComboBoxStyle),
                new PropertyMetadata(ColorConverter.ConvertFromString("#BBBBBB")));

        public static Color GetBackground_Arrow_Unchecked(UIElement element) => (Color)element.GetValue(Background_Arrow_UncheckedProperty);

        public static void SetBackground_Arrow_Unchecked(UIElement element, Color value) => element.SetValue(Background_Arrow_UncheckedProperty, value);
        #endregion

        #region Background_Arrow_Checked цвет фона стрелочки при нажатии
        public static readonly DependencyProperty Background_Arrow_CheckedProperty =
            DependencyProperty.RegisterAttached(
                nameof(GetBackground_Arrow_Checked)[3..],
                typeof(Color),
                typeof(ComboBoxStyle),
                new PropertyMetadata(ColorConverter.ConvertFromString("#46BCFF")));

        public static Color GetBackground_Arrow_Checked(UIElement element) => (Color)element.GetValue(Background_Arrow_CheckedProperty);

        public static void SetBackground_Arrow_Checked(UIElement element, Color value) => element.SetValue(Background_Arrow_CheckedProperty, value);
        #endregion

        #region MarginArrow отступ стрелочки, по умолчанию 0, 10, 12, 12
        public static readonly DependencyProperty MarginArrowProperty =
            DependencyProperty.RegisterAttached(
                nameof(SetMarginArrow)[3..],
                typeof(Thickness),
                typeof(ComboBoxStyle),
                new PropertyMetadata(new Thickness(0, 10, 12, 12)));

        public static Thickness GetMarginArrow(UIElement element) => (Thickness)element.GetValue(MarginArrowProperty);

        public static void SetMarginArrow(UIElement element, Thickness value) => element.SetValue(MarginArrowProperty, value);
        #endregion

        //Свойства выпадающего списка(dropDownBorder)
        #region CornerRadius_DropDown радиус закругления выпадающего списка(dropDownBorder)
        public static readonly DependencyProperty CornerRadius_DropDownProperty =
            DependencyProperty.RegisterAttached(
                nameof(GetCornerRadius_DropDown)[3..],
                typeof(double),
                typeof(ComboBoxStyle),
                new PropertyMetadata(10.0));

        public static double GetCornerRadius_DropDown(UIElement element) => (double)element.GetValue(CornerRadius_DropDownProperty);

        public static void SetCornerRadius_DropDown(UIElement element, double value) => element.SetValue(CornerRadius_DropDownProperty, value);
        #endregion

        #region Background_DropDown цвет фона выпадающего списка(dropDownBorder)
        public static readonly DependencyProperty Background_DropDownProperty =
            DependencyProperty.RegisterAttached(
                nameof(GetBackground_DropDown)[3..],
                typeof(SolidColorBrush),
                typeof(ComboBoxStyle),
                new PropertyMetadata(new SolidColorBrush(Colors.White)));

        public static SolidColorBrush GetBackground_DropDown(UIElement element) => (SolidColorBrush)element.GetValue(Background_DropDownProperty);

        public static void SetBackground_DropDown(UIElement element, SolidColorBrush value) => element.SetValue(Background_DropDownProperty, value);
        #endregion

        #region BorderBrush_DropDown цвет обводки выпадающего списка(dropDownBorder)
        public static readonly DependencyProperty BorderBrush_DropDownProperty =
            DependencyProperty.RegisterAttached(
                nameof(GetBorderBrush_DropDown)[3..],
                typeof(SolidColorBrush),
                typeof(ComboBoxStyle),
                new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));

        public static SolidColorBrush GetBorderBrush_DropDown(UIElement element) => (SolidColorBrush)element.GetValue(BorderBrush_DropDownProperty);

        public static void SetBorderBrush_DropDown(UIElement element, SolidColorBrush value) => element.SetValue(BorderBrush_DropDownProperty, value);
        #endregion

        #region Shadow_DropDown цвет тени выпадающего списка(dropDownBorder)
        public static readonly DependencyProperty Shadow_DropDownProperty =
            DependencyProperty.RegisterAttached(
                nameof(GetShadow_DropDown)[3..],
                typeof(Color),
                typeof(ComboBoxStyle),
                new PropertyMetadata(Colors.Black));

        public static Color GetShadow_DropDown(UIElement element) => (Color)element.GetValue(Shadow_DropDownProperty);

        public static void SetShadow_DropDown(UIElement element, Color value) => element.SetValue(Shadow_DropDownProperty, value);
        #endregion

        #region Shadow_DropDown_BlurRadius радиус тени у выпадающего списка(dropDownBorder)
        public static readonly DependencyProperty Shadow_DropDown_BlurRadiusProperty =
            DependencyProperty.RegisterAttached(
                nameof(GetShadow_DropDown_BlurRadius)[3..],
                typeof(double),
                typeof(ComboBoxStyle),
                new PropertyMetadata(10.0));

        public static double GetShadow_DropDown_BlurRadius(UIElement element) => (double)element.GetValue(Shadow_DropDown_BlurRadiusProperty);

        public static void SetShadow_DropDown_BlurRadius(UIElement element, double value) => element.SetValue(Shadow_DropDown_BlurRadiusProperty, value);
        #endregion

        #region Shadow_DropDown_Opacity значение прозрачности тени у выпадающего списка(dropDownBorder)
        public static readonly DependencyProperty Shadow_DropDown_OpacityProperty =
            DependencyProperty.RegisterAttached(
                nameof(GetShadow_DropDown_Opacity)[3..],
                typeof(double),
                typeof(ComboBoxStyle),
                new PropertyMetadata(0.9));

        public static double GetShadow_DropDown_Opacity(UIElement element) => (double)element.GetValue(Shadow_DropDown_OpacityProperty);

        public static void SetShadow_DropDown_Opacity(UIElement element, double value) => element.SetValue(Shadow_DropDown_OpacityProperty, value);
        #endregion

        #region MarginItem отступы для Item в списке, по умолчанию 10, 8, 0, 8
        public static readonly DependencyProperty MarginItemProperty =
            DependencyProperty.RegisterAttached(
                nameof(GetMarginItem)[3..],
                typeof(Thickness),
                typeof(ComboBoxStyle),
                new PropertyMetadata(new Thickness(10, 8, 0, 8)));

        public static Thickness GetMarginItem(UIElement element) => (Thickness)element.GetValue(MarginItemProperty);

        public static void SetMarginItem(UIElement element, Thickness value) => element.SetValue(MarginItemProperty, value);
        #endregion

        #region Background_Selected_Item цвет фона при выборе Item
        public static readonly DependencyProperty Background_Selected_ItemProperty =
            DependencyProperty.RegisterAttached(
                nameof(GetBackground_Selected_Item)[3..],
                typeof(SolidColorBrush),
                typeof(ComboBoxStyle),
                new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EFEFEF"))));

        public static SolidColorBrush GetBackground_Selected_Item(UIElement element) => (SolidColorBrush)element.GetValue(Background_Selected_ItemProperty);

        public static void SetBackground_Selected_Item(UIElement element, SolidColorBrush value) => element.SetValue(Background_Selected_ItemProperty, value);
        #endregion

        #region Background_ItemMouseOver цвет фона при наведении на Item
        public static readonly DependencyProperty Background_ItemMouseOverProperty =
            DependencyProperty.RegisterAttached(
                nameof(GetBackground_ItemMouseOver)[3..],
                typeof(SolidColorBrush),
                typeof(ComboBoxStyle),
                new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EFEFEF"))));

        public static SolidColorBrush GetBackground_ItemMouseOver(UIElement element) => (SolidColorBrush)element.GetValue(Background_ItemMouseOverProperty);

        public static void SetBackground_ItemMouseOver(UIElement element, SolidColorBrush value) => element.SetValue(Background_ItemMouseOverProperty, value);
        #endregion
    }
}
