using System.Collections;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Design_Style;

namespace Design_Style_Exemple
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //DataContext = this;
            numericUpDownUnits.Value = 62;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NumericUpDown.Value = 100.0;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(NumericUpDown.Value.ToString());
        }
        string language = "Dictionary1.xaml";
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //  NumericTime.TimeType = TimeIntervals.Hour;
            MessageBox.Show(NumericTime.TimeType.ToString());
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            //NumericTime.TimeType = TimeIntervals.Hour;
            //ChangeLanguage();
            var oldTimeType = NumericTime.TimeType;
            NumericTime.TimeIntervalsDisplay = new Dictionary<TimeIntervals, string>()
            {
                { TimeIntervals.Hour, "ч"},
                { TimeIntervals.Minute, "м"},
                { TimeIntervals.Second, "с"},
                { TimeIntervals.Millisecond, "мс"},
            };
            //  NumericTime.TimeType = updatedDictionary.Keys.FirstOrDefault(x => x != oldTimeType);
            // NumericTime.TimeType = updatedDictionary.Keys.FirstOrDefault(x => x == oldTimeType);
        }
        private void SetLanguePack(string path)
        {
            var dictionariesToRemove = Application.Current.Resources.MergedDictionaries.Where(d => d.Source != null && d.Source.OriginalString.Contains("Dictionary")).ToList();
            foreach (var dictionary in dictionariesToRemove)
                Application.Current.Resources.MergedDictionaries.Remove(dictionary);
            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri(path, UriKind.Relative) });
        }
        public void ChangeLanguage()
        {
            if (language == "Dictionary1.xaml")
            {
                SetLanguePack("Dictionary2.xaml");
                language = "Dictionary2.xaml";
                return;
            }
            if (language == "Dictionary2.xaml")
            {
                SetLanguePack("Dictionary1.xaml");
                language = "Dictionary1.xaml";
                return;
            }
        }
    }

    public class SomeViewModel
    {
        public double SomeNumber { get; set; }    
    }

    #region Это часть реализуется в View
    public class LengthUnitConverter : DoubleUnitConverter
    {
        protected override double GetDirectConversionFactor(object? unitTarget)
        {
            if (unitTarget is not string target)
            {
                target = string.Empty;
            }
            return target switch
            {
                "in" => 0.0254,
                "mi" => 1609.344,
                "m" => 1,
                "ft" => 0.3048,
                _ => 1
            };
        }
    }

    // Какой-то источник единиц измерения.
    // Они могут быть в любом типе: перечисление, строка, картинки.
    // Как конкретно их выводить будет решаться на уровне View.
    public static class SomeSource
    {
        public static readonly IDictionary Units = "in,дюйм mi,миля m,метр ft,фут"
            .Split()
            .Select(pair => pair.Split(','))
            .ToDictionary(pair => pair[0], pair => pair[1]);
    }
    #endregion

}