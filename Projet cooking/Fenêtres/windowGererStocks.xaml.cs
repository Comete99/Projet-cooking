using Projet_cooking.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    /// Logique d'interaction pour windowGererStocks.xaml
    /// </summary>
    public partial class windowGererStocks : Window
    {
        windowGestionnaire windowGestionnaire;
        public windowGererStocks(windowGestionnaire w)
        {
            InitializeComponent();
            foreach (Produit p in RessourceSQL.allProduits)
            {
                boxIngredients.Items.Add(p);
                listProduit.Items.Add(p);
            }
            windowGestionnaire = w;
        }

        private void boxIngredients_DropDownClosed(object sender, EventArgs e)
        {
            if (boxIngredients.SelectedItem != null)
            {
                boxFournisseur.Items.Clear();
                boxQteCommande.Text = "0";
                Produit produitSelectionne = (Produit)boxIngredients.SelectedItem;
                boxQteMax.Text = produitSelectionne.StockMax.ToString() + " " + produitSelectionne.Unite;
                boxQteMin.Text = produitSelectionne.StockMin.ToString() + " " + produitSelectionne.Unite;
                boxQteActuelle.Text = produitSelectionne.StockActuel.ToString() + " " + produitSelectionne.Unite;
                boxFournisseur.Items.Add(produitSelectionne.NomFournisseur);
                boxQteCommande.Text = (produitSelectionne.StockMax - produitSelectionne.StockActuel).ToString() + " " + produitSelectionne.Unite;
            }
        }

        private void buttonCommander_Click(object sender, RoutedEventArgs e)
        {
            if (boxIngredients.SelectedItem != null)
            {
                string[] quantiteCommandee = boxQteCommande.Text.Split(' ');
                string[] quantiteActuelle = boxQteActuelle.Text.Split(' ');
                double stock = Convert.ToDouble(quantiteCommandee[0]) + Convert.ToDouble(quantiteActuelle[0]);
                RessourceSQL.commandeProduit(((Produit)boxIngredients.SelectedItem).NomProduit, stock.ToString());
                listProduit.Items.Clear();
                boxIngredients.Items.Clear();
                foreach (Produit p in RessourceSQL.allProduits)
                {
                    boxIngredients.Items.Add(p);
                    listProduit.Items.Add(p);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            windowGestionnaire.Show();
        }
    }
}
