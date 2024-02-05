using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows;

namespace Design_Style
{
    public class Custom_TextBox : TextBox
    {
        static Custom_TextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Custom_TextBox), new FrameworkPropertyMetadata(typeof(Custom_TextBox)));
        }
        #region Персонализация

        #region PlaceText (текст подсказки)
        public static readonly DependencyProperty PlaceTextProperty =
         DependencyProperty.Register(nameof(PlaceText), typeof(string), typeof(Custom_TextBox),
             new PropertyMetadata(""));

        public string PlaceText
        {
            get => (string)GetValue(PlaceTextProperty);
            set => SetValue(PlaceTextProperty, value);
        }
        public static string GetPlaceText(UIElement element) => (string)element.GetValue(PlaceTextProperty);

        public static void SetPlaceText(UIElement element, string value) => element.SetValue(PlaceTextProperty, value);
        #endregion

        #region BorderBrushMouseOver цвет BorderBrush у TextBox при наведение мыши
        public static readonly DependencyProperty BorderBrushMouseOverProperty =
            DependencyProperty.RegisterAttached(
                nameof(BorderBrushMouseOver),
                typeof(SolidColorBrush),
                typeof(Custom_TextBox),
                new PropertyMetadata(new SolidColorBrush((Color)(ColorConverter.ConvertFromString("#46BCFF")))));
        public SolidColorBrush BorderBrushMouseOver
        {
            get => (SolidColorBrush)GetValue(BorderBrushMouseOverProperty);
            set => SetValue(BorderBrushMouseOverProperty, value);
        }
        public static SolidColorBrush GetBorderBrushMouseOver(UIElement element) => (SolidColorBrush)element.GetValue(BorderBrushMouseOverProperty);

        public static void SetBorderBrushMouseOver(UIElement element, SolidColorBrush value) => element.SetValue(BorderBrushMouseOverProperty, value);
        #endregion

        #region BorderBrushError цвет BorderBrush у TextBox, если текст не подходит под тип long(если включено IsParseLongNumeric или IsError)
        public static readonly DependencyProperty BorderBrushErrorProperty =
            DependencyProperty.RegisterAttached(
                nameof(BorderBrushError),
                typeof(SolidColorBrush),
                typeof(Custom_TextBox),
                new PropertyMetadata(new SolidColorBrush(Colors.Red)));
        public SolidColorBrush BorderBrushError
        {
            get => (SolidColorBrush)GetValue(BorderBrushErrorProperty);
            set => SetValue(BorderBrushErrorProperty, value);
        }
        public static SolidColorBrush GetBorderBrushError(UIElement element) => (SolidColorBrush)element.GetValue(BorderBrushErrorProperty);

        public static void SetBorderBrushError(UIElement element, SolidColorBrush value) => element.SetValue(BorderBrushErrorProperty, value);
        #endregion

        #region CornerRadius радиус закругления у TextBox
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.RegisterAttached(
                nameof(CornerRadius),
                typeof(CornerRadius),
                typeof(Custom_TextBox),
                new PropertyMetadata(new CornerRadius(6.0)));
        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(PlaceTextProperty, value);
        }
        public static CornerRadius GetCornerRadius(UIElement element) => (CornerRadius)element.GetValue(CornerRadiusProperty);

        public static void SetCornerRadius(UIElement element, CornerRadius value) => element.SetValue(CornerRadiusProperty, value);
        #endregion

        #endregion

        #region IsError включает или выключает проверку текста является ли он типом Long
        public static readonly DependencyProperty IsErrorProperty =
            DependencyProperty.RegisterAttached(
                nameof(IsError),
                typeof(bool),
                typeof(Custom_TextBox),
                new PropertyMetadata(false));
        /// <summary>
        ///Если включено меняется BorderBrush TextBox на BorderBrushError
        /// </summary>
        public bool IsError
        {
            get => (bool)GetValue(IsErrorProperty);
            set => SetValue(IsErrorProperty, value);
        }
        #endregion

        #region IsParseLongNumeric включает или выключает проверку текста является ли он типом Long
        public static readonly DependencyProperty IsParseLongNumericProperty =
            DependencyProperty.RegisterAttached(
                nameof(IsParseLongNumeric),
                typeof(bool),
                typeof(Custom_TextBox),
                new PropertyMetadata(false));
        /// <summary>
        ///Включает проверку текста является ли он типом Long
        /// </summary>
        public bool IsParseLongNumeric
        {
            get => (bool)GetValue(IsParseLongNumericProperty);
            set => SetValue(IsParseLongNumericProperty, value);
        }
        public static bool GetIsParseLongNumeric(DependencyObject element) => (bool)element.GetValue(IsParseLongNumericProperty);
        public static void SetIsParseLongNumeric(UIElement element, bool value) => element.SetValue(IsParseLongNumericProperty, value);
        #endregion

        #region IsLongNumeric сравнивает текст является он типом long

        private static readonly DependencyPropertyKey IsLongNumericPropertyKey =
            DependencyProperty.RegisterAttachedReadOnly(
                nameof(IsLongNumeric),
                typeof(bool),
                typeof(Custom_TextBox),
                new PropertyMetadata(true));
        private static readonly DependencyProperty IsLongNumericProperty = IsLongNumericPropertyKey.DependencyProperty;
        /// <summary>
        ///Возвращает true если текст подходит под тип long
        /// </summary>
        private bool IsLongNumeric
        {
            get => (bool)GetValue(IsLongNumericProperty);
            set => SetValue(IsLongNumericProperty, value);
        }
        public static bool GetIsLongNumeric(DependencyObject obj) => (bool)obj.GetValue(IsLongNumericProperty);

        public static void SetIsLongNumeric(DependencyObject obj, bool value) => obj.SetValue(IsLongNumericPropertyKey, value);
        #endregion

        #region IsTextEmpty сравнивает текст является пустым
        private static readonly DependencyPropertyKey IsTextEmptyPropertyKey =
            DependencyProperty.RegisterAttachedReadOnly(
                nameof(IsTextEmpty),
                typeof(bool),
                typeof(Custom_TextBox),
                new PropertyMetadata(true));
        private static readonly DependencyProperty IsTextEmptyProperty = IsTextEmptyPropertyKey.DependencyProperty;
        private bool IsTextEmpty
        {
            get => (bool)GetValue(IsTextEmptyProperty);
            set => SetValue(IsTextEmptyProperty, value);
        }
        public static bool GetIsTextEmpty(DependencyObject obj) => (bool)obj.GetValue(IsTextEmptyProperty);

        public static void SetIsTextEmpty(DependencyObject obj, bool value) => obj.SetValue(IsTextEmptyPropertyKey, value);
        #endregion

        public static readonly DependencyProperty TextExistProperty =
            DependencyProperty.RegisterAttached(
                "TextExist",
                typeof(DependencyProperty),
                typeof(Custom_TextBox),
                new PropertyMetadata(null, OnTextPropertyChanged));
        public static DependencyProperty GetTextExist(DependencyObject obj) => (DependencyProperty)obj.GetValue(TextExistProperty);

        public static void SetTextExist(DependencyObject obj, DependencyProperty value) => obj.SetValue(TextExistProperty, value);

        private static void OnTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue is DependencyProperty oldProp)
            {
                DependencyPropertyDescriptor descriptor = DependencyPropertyDescriptor.FromProperty(oldProp, typeof(DependencyObject));
                descriptor.RemoveValueChanged(d, OnTextChanged);
            }
            if (e.NewValue is DependencyProperty newProp)
            {
                DependencyPropertyDescriptor descriptor = DependencyPropertyDescriptor.FromProperty(newProp, typeof(DependencyObject));
                descriptor.AddValueChanged(d, OnTextChanged);
            }
        }

        private static void OnTextChanged(object? sender, EventArgs e)
        {
            DependencyObject d = (DependencyObject)sender!;
            string? text = d.GetValue(GetTextExist(d))?.ToString();
            bool _IsTextEmpty = string.IsNullOrEmpty(text) && string.IsNullOrWhiteSpace(text);
            SetIsTextEmpty(d, _IsTextEmpty);

            var _IsParseLongNumeric = GetIsParseLongNumeric(d);
            if (_IsParseLongNumeric)
            {
                bool isLong = long.TryParse(text, NumberStyles.Integer, ((XmlLanguage)d.GetValue(LanguageProperty))?.GetSpecificCulture(), out long _);
                SetIsLongNumeric(d, isLong);
            }
        }
    }
}
