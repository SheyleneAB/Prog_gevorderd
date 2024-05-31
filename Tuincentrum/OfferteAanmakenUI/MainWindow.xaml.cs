using Microsoft.Win32;
using System.Collections.Generic;
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
using TC_BL.Interfaces;
using TC_BL.Manager;
using TC_BL.Model;
using TC_SQL;

namespace OfferteAanmakenUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        IFileProcessor fileProcessor;
        ITCRepository TCRepository;
        TCManager TCManager;
        string connectionString = @"Data Source=Radion\sqlexpress;Initial Catalog=Tuin;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
        private Offerte huidigeOfferte;
        private Dictionary<Product, int> productLijst;

        public MainWindow()
        {
            InitializeComponent();
            fileProcessor = new TC_Fileprocessor();
            TCRepository = new TCRepository(connectionString);
            TCManager = new TCManager(fileProcessor, TCRepository);
            huidigeOfferte = new Offerte();
            productLijst = new Dictionary<Product, int>();
            cbProducten.ItemsSource = TCManager.GeefProducten();
            cbKlanten.ItemsSource = TCManager.GeefAlleKlanten();


        }

        private void btnProductToevoegen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbProducten.SelectedItem is Product geselecteerdProduct && int.TryParse(tbProductAantal.Text, out int aantal))
                {
                    huidigeOfferte.VoegProductToe(geselecteerdProduct, aantal);
                    productLijst.Add(geselecteerdProduct, aantal);
                    UpdateProductList();
                }
                else
                {
                    MessageBox.Show("Selecteer een product en voer een geldig aantal in.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fout bij het toevoegen van het product: {ex.Message}");
            }
        }

        private void btnProductVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            if (lbProducten.SelectedItem is List<Product> geselecteerdItem)
            {
                productLijst.Clear();
                
            }
            else
            {
                MessageBox.Show("Selecteer een product om te verwijderen.");
            }

        }

        private void btnOfferteAanmaken_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime datum = dpDatum.SelectedDate ?? DateTime.Now;
                
                bool afhalen = cbAfhalen.IsChecked ?? false;
                bool plaatsen = cbPlaatsen.IsChecked ?? false;

                

                Klant klant = cbKlanten.SelectedItem as Klant;
                huidigeOfferte.Datum = datum;
                huidigeOfferte.Klant = klant;
                huidigeOfferte.AfhalenBool = afhalen;
                huidigeOfferte.PlaatsenBool = plaatsen;

                TCManager.SchrijfeenOfferte(huidigeOfferte);

                MessageBox.Show($"Offerte aangemaakt voor klant: {klant.Naam}\nDatum: {datum.ToShortDateString()}\nAfhalen: {afhalen}\nPlaatsen: {plaatsen}\nPrijs: €{huidigeOfferte.prijsberekenen():F2}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fout bij het aanmaken van de offerte: {ex.Message}");
            }

        }
        private void UpdateProductList()
        {
            lbProducten.ItemsSource = null;
            lbProducten.ItemsSource = productLijst;
        }
    }
}