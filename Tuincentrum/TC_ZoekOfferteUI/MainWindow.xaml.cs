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
using TC_BL;
using TC_BL.Model;
using TC_BL.Manager;
using Microsoft.Win32;
using TC_BL.Interfaces;
using TC_SQL;

namespace TC_ZoekOfferteUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        
        ITCRepository TCRepository;
        TCManager TCManager;
        string connectionString = @"Data Source=Radion\sqlexpress;Initial Catalog=Tuin;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
        Offerte huidigeOfferte;
        Dictionary<Product, int> productLijst;
        public MainWindow()
        {
            InitializeComponent();
            
            TCRepository = new TCRepository(connectionString);
            TCManager = new TCManager(TCRepository);
            huidigeOfferte = new Offerte();
            productLijst = new Dictionary<Product, int>();
            
        }


        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            int? offerteId = null;
            if (int.TryParse(OfferteIdTextBox.Text, out int id))
            {
                offerteId = id;
            }

            Klant klant = null;
            if (cboKlanten.SelectedIndex != 0)
            {
                klant = cboKlanten.SelectedItem as Klant;
            }

            DateTime? datum = DatumPicker.SelectedDate;

            try
            {
                List<Offerte> offertes = TCManager.ToonOffertes(offerteId, klant, datum);
                VindOfferte w = new VindOfferte(offertes);
                w.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cboKlanten.ItemsSource = TCManager.GeefAlleKlanten();
        }
        private void OfferteIdTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(OfferteIdTextBox.Text))
            {
                cboKlanten.IsEnabled = false;
                DatumPicker.IsEnabled = false;
            }
            else
            {
                cboKlanten.IsEnabled = true;
                DatumPicker.IsEnabled = true;
            }
        }

        private void CboKlanten_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboKlanten.SelectedItem != null)
            {
                OfferteIdTextBox.IsEnabled = false;
                DatumPicker.IsEnabled = false;
            }
            else
            {
                OfferteIdTextBox.IsEnabled = true;
                DatumPicker.IsEnabled = true;
            }
        }

        private void DatumPicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DatumPicker.SelectedDate.HasValue)
            {
                OfferteIdTextBox.IsEnabled = false;
                cboKlanten.IsEnabled = false;
            }
            else
            {
                OfferteIdTextBox.IsEnabled = true;
                cboKlanten.IsEnabled = true;
            }
        }
    }
}