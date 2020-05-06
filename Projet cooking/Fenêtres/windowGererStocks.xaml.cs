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
        public windowGererStocks()
        {
            InitializeComponent();
            foreach (Produit p in RessourceSQL.allProduits)
            {
                boxIngredients.Items.Add(p);
                listProduit.Items.Add(p);
            }
            foreach(string f in RessourceSQL.listeFournisseur())
            {
                boxFournisseur.Items.Add(f);
            }
        }

        private void boxIngredients_DropDownClosed(object sender, EventArgs e)
        {
            if (boxIngredients.SelectedItem != null)
            {
                boxQteCommande.Text = "0";
                Produit produitSelectionne = (Produit)boxIngredients.SelectedItem;
                boxQteMax.Text = produitSelectionne.StockMax.ToString() + " " + produitSelectionne.Unite;
                boxQteMin.Text = produitSelectionne.StockMin.ToString() + " " + produitSelectionne.Unite;
                boxQteActuelle.Text = produitSelectionne.StockActuel.ToString() + " " + produitSelectionne.Unite;
                boxQteCommande.Text = (produitSelectionne.StockMax - produitSelectionne.StockActuel).ToString() + " " + produitSelectionne.Unite;
            }
        }

        private void buttonCommander_Click(object sender, RoutedEventArgs e)
        {
            if (boxIngredients.SelectedItem != null)
            {
                RessourceSQL.commandeProduit(((Produit)boxIngredients.SelectedItem).NomProduit, Convert.ToDouble(boxQteCommande), Convert.ToDouble(boxQteActuelle));
                listProduit.Items.Clear();
                boxIngredients.Items.Clear();
                foreach (Produit p in RessourceSQL.allProduits)
                {
                    boxIngredients.Items.Add(p);
                    listProduit.Items.Add(p);
                }
            }
        }
    }
}
