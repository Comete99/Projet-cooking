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
    /// Logique d'interaction pour windowDemo5.xaml
    /// </summary>
    public partial class windowDemo5 : Window
    {
        public windowDemo5()
        {
            InitializeComponent();

            //on remplit la listview grâce à la classe MyItem
            foreach(Produit p in RessourceSQL.allProduits)
            {
                listProduits.Items.Add(new MyItem { Nom = p.NomProduit, Quantite = p.StockActuel, Unite=p.Unite });
            }

        }

        private void btnDemo_Click(object sender, RoutedEventArgs e)
        {
            windowDemo6 w = new windowDemo6();
            w.Show();
            this.Hide();
        }
    }
}
