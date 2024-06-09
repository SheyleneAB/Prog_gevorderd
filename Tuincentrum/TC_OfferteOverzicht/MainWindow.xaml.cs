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

namespace TC_OfferteOverzicht
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

        private void Otoev_Click(object sender, RoutedEventArgs e)
        {
            OfferteAanmakenUI.MainWindow w = new OfferteAanmakenUI.MainWindow();
            w.Show();
        }

        private void Offerte_Click(object sender, RoutedEventArgs e)
        {
            TC_ZoekOfferteUI.MainWindow w = new TC_ZoekOfferteUI.MainWindow();
            w.Show();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            TC_UpdateOfferteUI.MainWindow w = new TC_UpdateOfferteUI.MainWindow();
            w.Show();
        }
    }
}