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
using Visstat_SQL;
using Visstat_uploaddata;
using VisStatsBL.interfaces;
using VisStatsBL.Manager;
using VisStatsBL.MODEL;


namespace VisStatsUI_Statistieken
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
        string connectionString = @"Data Source=Elyne\SQLEXPRESS;Initial Catalog=PQValue_B;Integrated Security=True;Trust Server Certificate=True";
        ObservableCollection<Vissoort> AlleVissoorten;
        ObservableCollection<Vissoort> GeselecteerdeVissoorten;
        public MainWindow()
        {
            fileProcessor = new FileProcessor();
            visStatRepository = new VisStatRepository(connectionString);
            visStatsManager = new VisStatManager(fileProcessor, visStatRepository);
            InitializeComponent();
            HavensComboBox.ItemsSource = visStatsManager.GeefHaven();
            HavensComboBox.SelectedIndex = 0;
            JaarComboBox.ItemsSource = visStatsManager.GeefJaartallen();
            JaarComboBox.SelectedIndex = 0;
            AlleVissoorten = new ObservableCollection<Vissoort>(visStatsManager.GeefVissoorten());
            AlleSoortenListBox.ItemsSource = AlleVissoorten;
            GeselecteerdeVissoorten = new ObservableCollection<Vissoort>();
            GeselecteerdeSoortenListBox.ItemsSource = GeselecteerdeVissoorten;
        }

        private void VoegAlleSoortenToe(object sender, RoutedEventArgs e)
        {
            foreach(Vissoort v in AlleVissoorten)
            {
                GeselecteerdeVissoorten.Add(v);
            }
            AlleVissoorten.Clear();
        }

        private void VoegAlleSoortenToeButton_Click(object sender, RoutedEventArgs e)
        {
            List<Vissoort> soorten = new();
            foreach (Vissoort v  in AlleSoortenListBox.SelectedItems) soorten.Add(v);
            foreach(Vissoort v in AlleSoortenListBox.SelectedItems)
            {
                GeselecteerdeVissoorten.Add(v);
                AlleVissoorten.Remove(v);
            }
        }

        private void VoegSoortenToeButton_Click(object sender, RoutedEventArgs e)
        {
            List<Vissoort> soorten = new();
            foreach (Vissoort v in AlleSoortenListBox.SelectedItems) soorten.Add(v);
            foreach (Vissoort v in soorten)
            {
                GeselecteerdeVissoorten.Add(v);
                AlleVissoorten.Remove(v);
            }


        }

        private void VerwijderSoortenToeButton_Click(object sender, RoutedEventArgs e)
        {

            List<Vissoort> soorten = new();
            foreach (Vissoort v in GeselecteerdeSoortenListBox.SelectedItems) soorten.Add(v);
            foreach (Vissoort v in soorten)
            {
                GeselecteerdeVissoorten.Remove(v);
                AlleVissoorten.Add(v);
            }

        }

        private void VerwijderAlleSoortenToeButton_Click(object sender, RoutedEventArgs e)
        {
            foreach(Vissoort v in GeselecteerdeVissoorten)
            {
                AlleVissoorten.Add(v);
            }
            GeselecteerdeVissoorten.Clear();

        }

        private void ToonStatistieken_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}