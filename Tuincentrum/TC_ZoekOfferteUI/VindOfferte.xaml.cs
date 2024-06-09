using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TC_BL.Model;

namespace TC_ZoekOfferteUI
{
    /// <summary>
    /// Interaction logic for VindOfferte.xaml
    /// </summary>
    public partial class VindOfferte : Window
    {
        private List<Offerte> offertes;

        public VindOfferte(List<Offerte> offertes)
        {
            InitializeComponent();
            this.DataContext = offertes;
            StatistiekenDataGridoffertes.ItemsSource = offertes;
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            Offerte SelOf = StatistiekenDataGridoffertes.SelectedItem as Offerte;
            TC_UpdateOfferteUI.MainWindow w = new TC_UpdateOfferteUI.MainWindow(SelOf);
            w.Show();
        }
    }
}
