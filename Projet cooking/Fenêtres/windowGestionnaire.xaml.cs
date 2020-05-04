using Projet_cooking.Classes;
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

namespace Projet_cooking.Fenêtres
{
    /// <summary>
    /// Logique d'interaction pour windowGestionnaire.xaml
    /// </summary>
    public partial class windowGestionnaire : Window
    {
        public windowGestionnaire()
        {
            InitializeComponent();
            RessourceSQL.allRecettes.Sort();
            int i = 0;
            while (i < 5 && i < RessourceSQL.allRecettes.Count)
            {
                listTopRecette.Items.Add(RessourceSQL.allRecettes[i]);
                i++;
            }
        }

        private void buttonGererStock_Click(object sender, RoutedEventArgs e)
        {
            windowGererStocks w = new windowGererStocks();
            w.Show();
            this.Hide();
        }

        private void buttonGererCdR_Click(object sender, RoutedEventArgs e)
        {
            windowGererCdR w = new windowGererCdR();
            w.Show();
            this.Hide();
        }
    }
}
