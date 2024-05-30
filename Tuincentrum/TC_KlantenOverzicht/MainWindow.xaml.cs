using Microsoft.Win32;
using System.Collections.ObjectModel;
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
using TC_BL.Manager;
using TC_BL.Model;
using TC_SQL;

namespace TC_KlantenOverzicht
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
        
        public MainWindow()
        {
            InitializeComponent();
            fileProcessor = new TC_Fileprocessor();
            TCRepository = new TCRepository(connectionString);
            TCManager = new TCManager(fileProcessor, TCRepository);
        }


        private void Toonklant_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<int, Klant> klantenopid = TCRepository.LeesAlleKlanten();
            List<Klant> klantenlist = klantenopid.Values.ToList();
            Dictionary<string, Klant> klantenopnaam = new Dictionary<string, Klant>();
            foreach (Klant klant in klantenlist) { 
                klantenopnaam.Add(klant.Naam, klant);
            }
            string input = searchTextBox.Text;
            

            if (string.IsNullOrWhiteSpace(input))
            {
                MessageBox.Show("geen Klanten gevonden", "Error");
                
            }

            else if (int.TryParse(input, out int klantId))
            {
                if (!klantenopid.ContainsKey(klantId))
                {
                    MessageBox.Show("geen Klanten gevonden", "Error");
                }

                else 
                {
                 
                    Klantengeg klantengeg = TCManager.GeefKlantengegevensbyid(klantId);

                    KlantopzoekenUI w = new KlantopzoekenUI(klantengeg);
                    w.ShowDialog();
                }
            }
            else
            {
                if (!klantenopnaam.ContainsKey(input))
                {
                    MessageBox.Show("geen Klanten gevonden", "Error");
                }
                else
                {
                   Klantengeg klantengeg = TCManager.GeefKlantengegevensbynaam(input);

                    KlantopzoekenUI w = new KlantopzoekenUI(klantengeg);
                    w.ShowDialog(); 
                }
            }
        }
    }
}