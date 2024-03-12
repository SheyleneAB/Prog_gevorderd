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
using VisStatsBL.MODEL;

namespace VisStatsUI_Statistieken
{
    /// <summary>
    /// Interaction logic for StatistiekenWindow.xaml
    /// </summary>
    public partial class StatistiekenWindow : Window
    {
        public StatistiekenWindow(int jaar, Haven haven, Eenheid eenheid, List<Jaarvangst> vangst)
        {
            InitializeComponent();
            HavenTextBox.Text = haven.Stad;
            JaarTextBox.Text = jaar.ToString();
            EenheidTextBox.Text = eenheid.ToString();

            StatistiekenDataGrid.ItemsSource = vangst;
        }
    }
}
