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

namespace VisstatsUI_Maand
{
    /// <summary>
    /// Interaction logic for StatistiekenWindow.xaml
    /// </summary>
    public partial class StatistiekenWindow : Window
    {
        public StatistiekenWindow(List<Maandvangst> vangst , Eenheid eenheid)
        {
            InitializeComponent();
            EenheidTextBox.Text = eenheid.ToString();
            StatistiekenmaandDataGrid.ItemsSource = vangst;
        }
    }
    
}
