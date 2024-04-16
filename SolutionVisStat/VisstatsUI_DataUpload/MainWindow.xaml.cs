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
using Visstat_SQL;
using Visstat_uploaddata;
using VisStatsBL.interfaces;
using VisStatsBL.Manager;

namespace VisstatsUI_DataUpload
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        IFileProcessor fileProcessor;
        IVisStatRepository visStatRepository;
        VisStatManager visStatsManager;
        string connectionString = @"Data Source=RADION\SQLEXPRESS;Initial Catalog=Vissen;Integrated Security=True;Trust Server Certificate=True";

        public MainWindow()
        {
            InitializeComponent();
            openFileDialog.DefaultExt = ".txt";
            openFileDialog.Filter = "Text documents (.txt)|*.txt";
            openFileDialog.InitialDirectory = @"C:\Users\elyne\OneDrive\Documents\School-Documenten\Programmeren gevorderd\Vis";
            openFileDialog.Multiselect = true;
            fileProcessor = new FileProcessor();
            visStatRepository = new VisStatRepository(connectionString);
            visStatsManager = new VisStatManager(fileProcessor, visStatRepository);

        }

        private void Button_Click_Vissoorten(object sender, RoutedEventArgs e)
        {
            bool? result = openFileDialog.ShowDialog();
            if (result == true)
            {
                var fileNames = openFileDialog.FileNames;
                VissoortenFileListBox.ItemsSource = fileNames;
                openFileDialog.FileName = null;
            }
            else VissoortenFileListBox.ItemsSource = null;
        }
        private void Button_Click_VisHavens(object sender, RoutedEventArgs e)
        {
            bool? result = openFileDialog.ShowDialog();
            if (result == true)
            {
                var fileNames = openFileDialog.FileNames;
                VisHavensFileListBox.ItemsSource = fileNames;
                openFileDialog.FileName = null;
            }
            else VissoortenFileListBox.ItemsSource = null;
        }
        private void Button_Click_UploadVissoorten(object sender, RoutedEventArgs e)
        {
            foreach (string fileName in VissoortenFileListBox.ItemsSource)
            {
                visStatsManager.UploadVissoorten(fileName);
            }
            MessageBox.Show("Upload klaar", "VisStats");
        }


        private void Button_Click_UploadHavens(object sender, RoutedEventArgs e)
        {
            foreach (string fileName in VisHavensFileListBox.ItemsSource)
            {
                visStatsManager.UploadVisHavens(fileName);
            }
            MessageBox.Show("Upload klaar", "VisStats");
        }

        private void Button_Click_UploadStatistieken(object sender, RoutedEventArgs e)
        {
            foreach (string fileName in StatistiekenFileListBox.ItemsSource)
            {
                visStatsManager.UploadStatistieken(fileName);
            }
            MessageBox.Show("Upload klaar", "VisStats");
        }
        private void Button_Click_Statistieken(object sender, RoutedEventArgs e)
        {
            bool? result = openFileDialog.ShowDialog();
            if (result == true)
            {
                var fileNames = openFileDialog.FileNames;
                StatistiekenFileListBox.ItemsSource = fileNames;
                openFileDialog.FileName = null;
            }
            else StatistiekenFileListBox.ItemsSource = null;
        }
    }
}