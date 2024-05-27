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

namespace TC_AlgemeenOverzichtUI
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

        private void Dtoev_Click(object sender, RoutedEventArgs e)
        {
            TC_DatauploadenUI.MainWindow window1 = new TC_DatauploadenUI.MainWindow();
            window1.Show();
        }

        private void Offerte_Click(object sender, RoutedEventArgs e)
        {
            TC_OfferteOverzicht.MainWindow window2 = new TC_OfferteOverzicht.MainWindow();
            window2.Show();
        }

        private void Klanten_Click(object sender, RoutedEventArgs e)
        {
            TC_KlantenOverzicht.MainWindow window3 = new TC_KlantenOverzicht.MainWindow();
            window3.Show();

        }
    }
}