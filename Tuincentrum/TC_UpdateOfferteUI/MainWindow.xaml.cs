using Microsoft.Win32;
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
using TC_BL.Interfaces;
using TC_BL.Model;
using TC_BL.Manager;
using TC_SQL;
using System.Numerics;

namespace TC_UpdateOfferteUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Offerte Offerte;

        OpenFileDialog openFileDialog = new OpenFileDialog();
      
        ITCRepository TCRepository;
        TCManager TCManager;
        string connectionString = @"Data Source=Radion\sqlexpress;Initial Catalog=Tuin;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
        Dictionary<Product, int> Eersteprodlijst = new Dictionary<Product, int>();
        Dictionary<Product, int> produpdate = new Dictionary<Product, int>();
        Dictionary<Product, int> proddel = new Dictionary<Product, int>();
        Dictionary<Product, int> prodnew = new Dictionary<Product, int>();

        public MainWindow()
        {
            InitializeComponent();
           
            TCRepository = new TCRepository(connectionString);
            TCManager = new TCManager(TCRepository);
            cbProducten.ItemsSource = TCManager.GeefProducten();
            Offerte = new Offerte();
        }

        public MainWindow(Offerte offerte)
        {
            InitializeComponent();
            
            TCRepository = new TCRepository(connectionString);
            TCManager = new TCManager(TCRepository);
            cbProducten.ItemsSource = TCManager.GeefProducten();
            Offerte = offerte;
            this.DataContext = Offerte;
            lbProducten.ItemsSource = Offerte.Producten;
            Eersteprodlijst = new Dictionary<Product, int>(Offerte.Producten);
        }

        private void zoekoffertebutton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int offerteId = int.Parse(OfferteIdInput.Text);
                Klant klant = null;
                DateTime datum = new();
                List<Offerte> offertes = TCManager.ToonOffertes(offerteId, klant, datum);
                Offerte = offertes[0];
                this.DataContext = Offerte;
                lbProducten.ItemsSource = Offerte.Producten;
                Eersteprodlijst = new Dictionary<Product, int>(Offerte.Producten);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Offerte niet gevonden: {ex.Message}");
            }
        }

        private void btnProductToevoegen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbProducten.SelectedItem is Product geselecteerdProduct && int.TryParse(tbProductAantal.Text, out int aantal))
                {
                    Offerte.VoegProductToe(geselecteerdProduct, aantal);
                    RefreshProductenListBox();
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

        private void btnAantalProductVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lbProducten.SelectedItem is KeyValuePair<Product, int> geselecteerdItem && int.TryParse(tbAantalVerwijderen.Text, out int aantal))
                {
                    Offerte.VerwijderProduct(geselecteerdItem.Key, aantal);
                    RefreshProductenListBox();
                }
                else
                {
                    MessageBox.Show("Selecteer een product en voer een geldig aantal in.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fout bij het verwijderen van het product: {ex.Message}");
            }
        }

        private void btnEenProductVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lbProducten.SelectedItem is KeyValuePair<Product, int> geselecteerdItem)
                {
                    int aantal = Offerte.Producten[geselecteerdItem.Key];
                    Offerte.VerwijderProduct(geselecteerdItem.Key, geselecteerdItem.Value);
                    RefreshProductenListBox();
                }
                else
                {
                    MessageBox.Show("Selecteer een product.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fout bij het verwijderen van het product: {ex.Message}");
            }
        }

        private void btnAlleProductenVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            foreach (Product product in Offerte.Producten.Keys.ToList())
            {
                Offerte.VerwijderProduct(product, Offerte.Producten[product]);
            }
            RefreshProductenListBox();
        }

        private void btnOfferteUpdaten_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime datum = dpDatum.SelectedDate ?? DateTime.Now;

                bool afhalen = cbAfhalen.IsChecked ?? false;
                bool plaatsen = cbPlaatsen.IsChecked ?? false;

                Klant klant = Offerte.Klant;
                Offerte.Datum = datum;
                Offerte.Klant = klant;
                Offerte.AfhalenBool = afhalen;
                Offerte.PlaatsenBool = plaatsen;

                if (Offerte.Producten == null || Offerte.Producten.Count == 0)
                {
                    throw new Exception("De productenlijst kan niet leeg zijn");
                }
                else
                {
                    proddel = delprod(Eersteprodlijst);
                    produpdate = editprod(Eersteprodlijst);
                    prodnew = newprod(Eersteprodlijst);
                    
                    try
                    {
                        TCManager.SchrijfUpdateOfferte(Offerte, proddel, produpdate, prodnew);
                        string message = "Offerte Upgedated voor klant: " + klant.Naam +
                                    "\nDatum: " + datum.ToShortDateString() +
                                    "\nAfhalen: " + afhalen +
                                    "\nPlaatsen: " + plaatsen +
                                    "\nPrijs: €" + Offerte.prijsberekenen().ToString("F2");

                        message += "\n\nProducten Verwijderd:\n" + string.Join("\n", proddel.Select(kvp => kvp.Key.Nednaam + ": " + kvp.Value));
                        message += "\n\nProducten Toegevoegd:\n" + string.Join("\n", prodnew.Select(kvp => kvp.Key.Nednaam + ": " + kvp.Value));
                        message += "\n\nProducten Aangepast:\n" + string.Join("\n", produpdate.Select(kvp => kvp.Key.Nednaam + ": " + kvp.Value));

                        MessageBox.Show(message);
                    }
                    catch 
                    {
                        throw new Exception("De offerte kon niet upgedated worden");
                    }

                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fout bij het updaten van de offerte: {ex.Message}");
            }
        }

        private Dictionary<Product, int> editprod(Dictionary<Product, int> eersteprodlijst)
        {
            var editedProducts = new Dictionary<Product, int>();

            foreach (var kvp in Offerte.Producten)
            {
                if (eersteprodlijst.TryGetValue(kvp.Key, out int initialQuantity) && kvp.Value != initialQuantity)
                {
                    editedProducts.Add(kvp.Key, kvp.Value);
                }
            }

            return editedProducts;
        }

        private Dictionary<Product, int> delprod(Dictionary<Product, int> eersteprodlijst)
        {
            var deletedProducts = new Dictionary<Product, int>();

            foreach (var kvp in eersteprodlijst)
            {
                if (!Offerte.Producten.ContainsKey(kvp.Key))
                {
                    deletedProducts.Add(kvp.Key, kvp.Value);
                }
            }

            return deletedProducts;
        }

        private Dictionary<Product, int> newprod(Dictionary<Product, int> eersteprodlijst)
        {
            var newProducts = new Dictionary<Product, int>();

            foreach (var kvp in Offerte.Producten)
            {
                if (!eersteprodlijst.ContainsKey(kvp.Key))
                {
                    newProducts.Add(kvp.Key, kvp.Value);
                }
            }

            return newProducts;
        }
        private void RefreshProductenListBox()
        {
            lbProducten.ItemsSource = null;
            lbProducten.ItemsSource = Offerte.Producten.Select(kvp => new KeyValuePair<Product, int>(kvp.Key, kvp.Value)).ToList();
            prijslabel.Content = null;
            prijslabel.Content = Offerte.Berekenendeprijs;
        }
    }
}