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
using TC_BL.Manager;
using TC_SQL;


namespace TC_DatauploadenUI
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
        string connectionString = "Data Source=Radion\\sqlexpress;Initial Catalog=Tuin;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";

        public MainWindow()
        {
            InitializeComponent();
            openFileDialog.DefaultExt = ".txt";
            openFileDialog.Filter = "Text documents (.txt)|*.txt";
            openFileDialog.InitialDirectory = @"C:\Users\elyne\OneDrive\Documents\School-Documenten\Programmeren gevorderd\Tuin";
            openFileDialog.Multiselect = true;
            fileProcessor = new TC_Fileprocessor();
            TCRepository = new TCRepository(connectionString);
            TCManager = new TCManager(fileProcessor, TCRepository);
        }

        private void Button_Click_UploadKlanten(object sender, RoutedEventArgs e)
        {
            foreach (string fileName in KlantenFileListBox.ItemsSource)
            {
                TCManager.UploadKlanten(fileName);
            }
            MessageBox.Show("Upload klaar", "Klanten");


        }

        private void Button_Click_Klanten(object sender, RoutedEventArgs e)
        {
            bool? result = openFileDialog.ShowDialog();
            if (result == true)
            {
                var fileNames = openFileDialog.FileNames;
                KlantenFileListBox.ItemsSource = fileNames;
                openFileDialog.FileName = null;
            }
            else KlantenFileListBox.ItemsSource = null;

        }

        private void Button_Click_Producten(object sender, RoutedEventArgs e)
        {
            bool? result = openFileDialog.ShowDialog();
            if (result == true)
            {
                var fileNames = openFileDialog.FileNames;
                ProductenFileListBox.ItemsSource = fileNames;
                openFileDialog.FileName = null;
            }
            else ProductenFileListBox.ItemsSource = null;

        }

        private void Button_Click_UploadProducten(object sender, RoutedEventArgs e)
        {
            foreach (string fileName in ProductenFileListBox.ItemsSource)
            {
                TCManager.UploadProducten(fileName);
            }
            MessageBox.Show("Upload klaar", "Producten");

        }
        // Heeft werk nodig
        private void Button_Click_Offerte(object sender, RoutedEventArgs e)
        {

            bool? result = openFileDialog.ShowDialog();
            if (result == true)
            {
                var fileNames = openFileDialog.FileNames;
                OffertesFileListBox.ItemsSource = fileNames;
                openFileDialog.FileName = null;
            }
            else OffertesFileListBox.ItemsSource = null;
        }

        private void Button_Click_UploadOffertes(object sender, RoutedEventArgs e)
        {
            foreach (string fileName in OffertesFileListBox.ItemsSource)
            {
                TCManager.UploadOffertes(fileName);
            }
            MessageBox.Show("Upload klaar", "Offertes");

        }
    }
}