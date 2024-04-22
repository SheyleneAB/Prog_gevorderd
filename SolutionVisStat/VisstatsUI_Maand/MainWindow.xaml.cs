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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace VisstatsUI_Maand
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
        ObservableCollection<int> AlleJaren;
        ObservableCollection<int> GeselecteerdeJaren;
        ObservableCollection<Haven> AlleHavens;
        ObservableCollection <Haven> GeselecteerdeHavens;
        public MainWindow()
        {
            fileProcessor = new FileProcessor();
            visStatRepository = new VisStatRepository(connectionString);
            visStatsManager = new VisStatManager(fileProcessor, visStatRepository);
            InitializeComponent();
            VissoortComboBox.ItemsSource = visStatsManager.GeefVissoorten();
            VissoortComboBox.SelectedIndex = 0;

            AlleJaren = new ObservableCollection<int>(visStatsManager.GeefJaartallen());
            AlleJarenListBox.ItemsSource = AlleJaren;
            GeselecteerdeJaren = new ObservableCollection<int>();
            GeselecteerdeJarenListBox.ItemsSource = GeselecteerdeJaren;

            AlleHavens = new ObservableCollection<Haven>(visStatsManager.GeefHaven());
            AlleHavensListBox.ItemsSource= AlleHavens;
            GeselecteerdeHavens = new ObservableCollection<Haven>();
            GeselecteerdeHavensListBox.ItemsSource = GeselecteerdeHavens;
        }


        private void voegAlleJarenToeButton_Click(object sender, RoutedEventArgs e)
        {

            foreach (int jaar in AlleJaren)
            {
                GeselecteerdeJaren.Add(jaar);
            }
            AlleJaren.Clear();
        }


        private void voegJarenToeButton_Click(object sender, RoutedEventArgs e)
        {

            List<int> jaren = new();
            foreach (int jaar in AlleJarenListBox.SelectedItems) jaren.Add(jaar);
            foreach (int jaar in jaren)
            {
                GeselecteerdeJaren.Add(jaar);
                AlleJaren.Remove(jaar);
            }
        }

        private void verwijderJarenButton_Click(object sender, RoutedEventArgs e)
        {
            List<int> jaren = new();
            foreach (int jaar in GeselecteerdeJarenListBox.SelectedItems) jaren.Add(jaar);
            foreach (int jaar in jaren)
            {
                GeselecteerdeJaren.Remove(jaar);
                AlleJaren.Add(jaar);
            }

        }

        private void verwijderAlleJarenToeButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (int jaar in GeselecteerdeJaren)
            {
                AlleJaren.Add(jaar);
            }
            GeselecteerdeJaren.Clear();
        }

        private void voegAlleHavensToeButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (Haven haven in AlleHavens)
            {
                GeselecteerdeHavens.Add(haven);
            }
            AlleHavens.Clear();
        }

        private void voegHavensToeButton_Click(object sender, RoutedEventArgs e)
        {
            List<Haven> havens = new();
            foreach (Haven haven in AlleHavensListBox.SelectedItems) havens.Add(haven);
            foreach (Haven haven in havens)
            {
                GeselecteerdeHavens.Add(haven);
                AlleHavens.Remove(haven);
            }
        }

        private void verwijderHavensButton_Click(object sender, RoutedEventArgs e)
        {
            List<Haven> havens = new();
            foreach (Haven haven in GeselecteerdeHavensListBox.SelectedItems) havens.Add(haven);
            foreach (Haven haven in havens)
            {
                AlleHavens.Add(haven);
                GeselecteerdeHavens.Remove(haven);
            }
        }

        private void verwijderAlleHavensButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (Haven haven in GeselecteerdeHavens)
            {
                AlleHavens.Add(haven);
            }
            GeselecteerdeHavens.Clear();
        }

        private void ToonStatistieken_Click(object sender, RoutedEventArgs e)
        {
            Eenheid eenheid;
            if ((bool)KgRadioButton.IsChecked) { eenheid = Eenheid.kg; } else eenheid = Eenheid.euro;
            List<Maandvangst> vangst = visStatsManager.GeefMaandVangst(GeselecteerdeJaren.ToList(), 
                GeselecteerdeHavens.ToList(), (Vissoort)VissoortComboBox.SelectedItem, eenheid);

            StatistiekenWindow w = new StatistiekenWindow(vangst, eenheid);
            w.ShowDialog();
        }
    }
}