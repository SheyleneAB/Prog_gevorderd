﻿using System;
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

namespace TC_KlantenOverzicht
{
    /// <summary>
    /// Interaction logic for KlantopzoekenUI.xaml
    /// </summary>
    public partial class KlantopzoekenUI : Window
    {
        public KlantopzoekenUI(List<Offerte> klantengeg)
        {
            InitializeComponent();
            this.DataContext = klantengeg;
            StatistiekenDataGrid2.ItemsSource = klantengeg;
            aantaloffertes.Content = klantengeg.Count();

            double totaalprijs = 0;
            foreach (var offerte in klantengeg)
            {
                totaalprijs += offerte.prijsberekenen();
            }
            Totaalprijs.Content = totaalprijs;
        }
    }
}
