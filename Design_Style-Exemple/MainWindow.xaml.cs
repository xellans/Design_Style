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
            DataContext = this;
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
            var updatedDictionary = new Dictionary<TimeIntervals, string>();
            updatedDictionary[TimeIntervals.Hour] = "ч";
            updatedDictionary[TimeIntervals.Minute] = "м";
            updatedDictionary[TimeIntervals.Second] = "с";
            updatedDictionary[TimeIntervals.Millisecond] = "мс";
            NumericTime.TimeIntervalsDisplay = updatedDictionary;
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
}