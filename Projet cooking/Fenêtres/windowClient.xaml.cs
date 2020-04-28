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
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace Projet_cooking.Fenêtres
{
    /// <summary>
    /// Logique d'interaction pour windowClient.xaml
    /// </summary>
    public partial class windowClient : Window
    {
        public windowClient()
        {
            InitializeComponent();
            messageConnection.Text += "Mr Cornichon";
            nbCook.Text += "0";
            boxNbRecette.Text = "5";
            RessourceSQL.toutesRecettes();
            foreach(Recette recette in RessourceSQL.allRecettes)
            {
                boxListeRecettes.Items.Add(recette.Nom);
            }
            boxListeRecettes.Items.Refresh();
        }

        private void buttonCdR_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageDevenirCdR = MessageBox.Show("Voulez-vous devenir CdR ?", "Devenir CdR", MessageBoxButton.YesNo);
            if(messageDevenirCdR == MessageBoxResult.Yes)
            {
                windowCdR w = new windowCdR();
                w.Show();
                this.Close();
                MessageBoxResult message = MessageBox.Show("Félicitations ! Vous êtes maintenant un créateur de recettes et participez à l'évolution de la plateforme.");
            }
        }

        private void buttonAjouterRecette_Click(object sender, RoutedEventArgs e)
        {
            if (boxListeRecettes.SelectedItem != null)
            {
                boxNbRecette.Text = Convert.ToString(Convert.ToInt32(boxNbRecette.Text) + 1);
            }
        }

        private void buttonRetirerRecette_Click(object sender, RoutedEventArgs e)
        {
            if (boxListeRecettes.SelectedItem != null)
            {
                if (Convert.ToInt32(boxNbRecette.Text) > 0)
                {
                    boxNbRecette.Text = Convert.ToString(Convert.ToInt32(boxNbRecette.Text) - 1);
                }
            }
        }

        private void buttonAjouterPanier_Click(object sender, RoutedEventArgs e)
        {
            //string recetteSelected;
            //recetteSelected = boxListeRecettes.SelectedItem.ToString();

            //Recette recette = RessourceSQL.rechercheRecette(recetteSelected);
            Recette recette = new Recette();
            if (!listPanier.Items.Contains(recette))
            {
                recette.Quantite += Convert.ToInt32(boxNbRecette.Text);
                recette.PrixTotal += recette.PrixVente * Convert.ToInt32(boxNbRecette.Text);
                listPanier.Items.Add(recette);
            }
            else
            {
                recette.Quantite += Convert.ToInt32(boxNbRecette.Text); ;
                recette.PrixTotal += recette.PrixVente * Convert.ToInt32(boxNbRecette.Text);
                listPanier.Items.Refresh();
            }
            boxNbRecette.Text = "0";
        }

        private void buttonPaiement_Click(object sender, RoutedEventArgs e)
        {
            foreach(Recette r in listPanier.Items)
            {
                //Verifier stock
                r.NbCommande += r.Quantite;
                r.Quantite = 0;
                r.PrixTotal = 0;
            }
        }
    }
}
