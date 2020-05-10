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

            //on remplit la combobox avec les produits de la base de donnée
            foreach (Produit p in RessourceSQL.allProduits)
            {
                boxListeProduits.Items.Add(p);
            }
            boxListeProduits.Items.Refresh();

            
            
            
        }

        private void boxListeProduits_DropDownClosed(object sender, EventArgs e)
        {
            //on vérifie que le sélection n'est pas vide
            if (boxListeProduits.SelectedItem != null)
            {
                listProduits.Items.Clear();
                Produit produit = (Produit)boxListeProduits.SelectedItem;

                //on parcours les recettes
                foreach (Recette r in RessourceSQL.allRecettes)
                {
                    //on regarde les produits dans les ingrédients de la recette
                    foreach (KeyValuePair<Produit, double> prod in r.Ingredients)
                    {
                        //si l'ingrédient a la même nom que le produit sélectionné dans la combobox, on affiche la recette
                        if (produit.NomProduit == prod.Key.NomProduit)
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
