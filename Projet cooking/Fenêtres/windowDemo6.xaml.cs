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
    /// Logique d'interaction pour windowDemo6.xaml
    /// </summary>
    public partial class windowDemo6 : Window
    {
        public windowDemo6()
        {
            InitializeComponent();

            foreach (Produit p in RessourceSQL.allProduits)
            {
                boxListeProduits.Items.Add(p);
            }
            boxListeProduits.Items.Refresh();

            
            
            
        }

        private void boxListeProduits_DropDownClosed(object sender, EventArgs e)
        {

            if (boxListeProduits.SelectedItem != null)
            {
                listProduits.Items.Clear();
                Produit produit = (Produit)boxListeProduits.SelectedItem;

                foreach (Recette r in RessourceSQL.allRecettes)
                {

                    foreach (KeyValuePair<Produit, double> prod in r.Ingredients)
                    {

                        if (produit == prod.Key)
                        {
                            listProduits.Items.Add(new MyItem { Nom = r.Nom, Quantite = prod.Value, Unite=prod.Key.Unite });
                        }
                    }
                }
            }

        }

        private void btnDemo_Click(object sender, RoutedEventArgs e)
        {
            windowDemo7 w = new windowDemo7();
            w.Show();
            this.Hide();
        }
    }
}
