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
            cbProducten.ItemsSource = TCManager.GeefProducten();
            cbKlanten.ItemsSource = TCManager.GeefAlleKlanten();
        }

        private void btnProductToevoegen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbProducten.SelectedItem is Product geselecteerdProduct && int.TryParse(tbProductAantal.Text, out int aantal))
                {
                    if (productLijst.ContainsKey(geselecteerdProduct))
                    {
                        productLijst[geselecteerdProduct] += aantal;
                    }
                    else
                    {
                        productLijst.Add(geselecteerdProduct, aantal);
                    }

                    lbProducten.ItemsSource = null; 
                    lbProducten.ItemsSource = productLijst.ToList();
                    prijsberekenen();
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

                foreach (Product product in productLijst.Keys) { 
                    huidigeOfferte.VoegProductToe(product,productLijst[product]);
                }
                TCManager.SchrijfeenOfferte(huidigeOfferte);

                MessageBox.Show($"Offerte aangemaakt voor klant: {klant.Naam}\nDatum: {datum.ToShortDateString()}\nAfhalen: {afhalen}\nPlaatsen: {plaatsen}\nPrijs: €{huidigeOfferte.prijsberekenen():F2}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fout bij het aanmaken van de offerte: {ex.Message}");
            }

        }

        private void btneenProductVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            if (lbProducten.SelectedItem is KeyValuePair<Product, int> geselecteerdItem)
            {
                productLijst.Remove(geselecteerdItem.Key);

                lbProducten.ItemsSource = null;
                lbProducten.ItemsSource = productLijst.ToList();
                prijsberekenen();
            }
            else
            {
                MessageBox.Show("Selecteer een product om te verwijderen.");
            }
        }

        private void btnAlleProductenVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            productLijst.Clear();
            lbProducten.ItemsSource = null;
            prijsberekenen();

        }

        private void btnAantalProductVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            if (lbProducten.SelectedItem is KeyValuePair<Product, int> geselecteerdItem)
            {
                int aantal = int.Parse(tbAantalVerwijderen.Text);
                
                if (productLijst[geselecteerdItem.Key] <= aantal) 
                {
                    productLijst.Remove(geselecteerdItem.Key);
                }
                else
                {
                    productLijst[geselecteerdItem.Key] -= aantal;
                }

                lbProducten.ItemsSource = null;
                lbProducten.ItemsSource = productLijst.ToList();
                prijsberekenen();
            }
            else
            {
                MessageBox.Show("Selecteer een product om te verwijderen.");
            }

        }
        public void prijsberekenen()
         {
             double prijs = 0;
             foreach (Product product in productLijst.Keys)
             {
                 prijs = prijs + (product.Prijs * productLijst[product]);
             }
             if (prijs > 5000)
             {
                 prijs = prijs * 0.90;
             }
             if (prijs > 2000 && prijs < 5000)
             {
                 prijs = prijs * 0.95;
             }
             if (cbAfhalen.IsPressed )
             {
                 if (prijs < 500)
                 {
                     prijs = prijs + 100;
                 }
                 if (prijs < 1000 && prijs > 500)
                 {
                     prijs = prijs + 50;
                 }
             }
             if (cbPlaatsen.IsPressed)
             {
                 if (prijs < 2000)
                 {
                     prijs = prijs * 1.15;
                 }
                 if (prijs > 2000 && prijs < 5000)
                 {
                     prijs = prijs * 1.10;
                 }
                 else
                 {
                     prijs = prijs * 1.05;
                 }
             }
            prijslabel.Content = prijs;
        }
    }
}