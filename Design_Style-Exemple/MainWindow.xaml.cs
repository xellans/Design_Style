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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var updatedDictionary = new Dictionary<TimeIntervals, string>();
            updatedDictionary[TimeIntervals.Hour] = "ч";
            updatedDictionary[TimeIntervals.Minute] = "м";
            updatedDictionary[TimeIntervals.Second] = "с";
            updatedDictionary[TimeIntervals.Millisecond] = "мс";
            NumericUpDownTime.TimeIntervalsDisplay = updatedDictionary;
        }
    }
}