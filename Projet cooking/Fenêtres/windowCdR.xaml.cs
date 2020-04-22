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
    /// Logique d'interaction pour windowCdR.xaml
    /// </summary>
    public partial class windowCdR : Window
    {
        static Dictionary<string, double> ingredients = new Dictionary<string, double>();
        Recette recette1 = new Recette("Galettes de quinoa", "Test", ingredients, "c'est bon", 6, 2, "jean.dupont@cook.com");
        int prixCook=15;
        int nbCookCdR = 30;
        
        List<string> panier;
        public windowCdR()
        {
            InitializeComponent();
            messageConnection.Text += "Mr Cornichon";
            nbCook.Text += "30";
            boxNbRecette.Text = "5";
            boxListeRecettes.Items.Add(recette1);
            boxListeRecettes.Items.Refresh();
            RessourceSQL.recettes.Add(recette1);
        }

        private void buttonAjouterRecette_Click(object sender, RoutedEventArgs e)
        {
            boxNbRecette.Text = Convert.ToString(Convert.ToInt32(boxNbRecette.Text) + 1);
        }

        private void buttonRetirerRecette_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt32(boxNbRecette.Text) > 0)
            {
                boxNbRecette.Text = Convert.ToString(Convert.ToInt32(boxNbRecette.Text) - 1);
            }
        }

        private void buttonPaiementCook_Click(object sender, RoutedEventArgs e)
        {
            if ( nbCookCdR >= prixCook)
            {
                MessageBoxResult message = MessageBox.Show("Paiement accepté");
                nbCookCdR -= prixCook;
                nbCook.Text = Convert.ToString(nbCookCdR);
            }
        }

        private void buttonRecettes_Click(object sender, RoutedEventArgs e)
        {
            windowRecettes w = new windowRecettes(this);
            w.Show();
            this.Hide();
        }

        private void buttonAjouterPanier_Click(object sender, RoutedEventArgs e)
        {
            Recette recette = (Recette)boxListeRecettes.SelectedItem;

            //if(RessourceSQL.est_client("kevin.vaut@gmail.com", "gh54p"))
            if (!listPanier.Items.Contains(recette))
            {
                recette.Quantite += Convert.ToInt32(boxNbRecette.Text);
                recette.PrixTotal += recette.PrixVente*Convert.ToInt32(boxNbRecette.Text);
                listPanier.Items.Add(recette);
            }
            else
            {
                recette.Quantite += Convert.ToInt32(boxNbRecette.Text); ;
                recette.PrixTotal += recette1.PrixVente * Convert.ToInt32(boxNbRecette.Text);
                listPanier.Items.Refresh();
            }
        }
    }
}
