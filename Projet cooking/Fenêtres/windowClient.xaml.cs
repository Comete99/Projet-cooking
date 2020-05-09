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
using System.Transactions;

namespace Projet_cooking.Fenêtres
{
    /// <summary>
    /// Logique d'interaction pour windowClient.xaml
    /// </summary>
    public partial class windowClient : Window
    {
        string nomClient;
        string prenomClient;
        public windowClient(string nom, string prenom)
        {
            InitializeComponent();
            nomClient = nom;
            prenomClient = prenom;
            messageConnection.Text += nom + " " + prenom;
            nbCook.Text += "0";
            boxNbRecette.Text = "5";
            foreach (Recette recette in RessourceSQL.allRecettes)
            {
                boxListeRecettes.Items.Add(recette);
            }
            boxListeRecettes.Items.Refresh();
        }

        private void buttonCdR_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageDevenirCdR = MessageBox.Show("Voulez-vous devenir CdR ?", "Devenir CdR", MessageBoxButton.YesNo);
            if (messageDevenirCdR == MessageBoxResult.Yes)
            {
                RessourceSQL.devenirCdR(nomClient, prenomClient);
                windowCdR w = new windowCdR(RessourceSQL.rechercheMailCdR(nomClient,prenomClient), nomClient, prenomClient, 0);
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
            Recette recette = (Recette)boxListeRecettes.SelectedItem;
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
            foreach (Recette r in listPanier.Items)
            {
                //Verifier stock
                foreach (KeyValuePair<Produit, double> produit in r.Ingredients)
                {
                    if (produit.Value * r.Quantite > produit.Key.StockActuel)
                    {
                        r.Quantite = 0;
                        listPanier.Items.Remove(r);
                        MessageBoxResult message = MessageBox.Show("Veuillez-nous excuser mais la recette : " + r.Nom + " ne peut pas être commandée par manque de " + produit.Key.NomProduit + "cette recette a été retirée de votre panier, veuillez repasser la commande si vous le souhaitez");
                        return;
                    }
                }
            }
            double prixCook = 0;
            foreach (Recette r in listPanier.Items)
            {
                //On ajuste les stocks
                foreach (KeyValuePair<Produit, double> produit in r.Ingredients)
                {
                    produit.Key.StockActuel -= produit.Value * r.Quantite;
                }
                //rémunérer CdR
                if (r.NbCommande < 10 && r.NbCommande + r.Quantite >= 10)
                {
                    if (r.NbCommande + r.Quantite >= 50)
                    {
                        r.NbCommande += r.Quantite;
                        RessourceSQL.CdRPaiementCook(r, true, true);
                    }
                    else
                    {
                        r.NbCommande += r.Quantite;
                        RessourceSQL.CdRPaiementCook(r, true, false);
                    }
                }
                else if (r.NbCommande < 50 && r.NbCommande + r.Quantite >= 50)
                {
                    r.NbCommande += r.Quantite;
                    RessourceSQL.CdRPaiementCook(r, false, true);
                }
                else
                {
                    r.NbCommande += r.Quantite;
                    RessourceSQL.CdRPaiementCook(r, false, false);
                }
                for (int i = 0; i < r.Quantite; i++)
                {
                    r.Commandes.Add(DateTime.Now);
                }
                prixCook += r.PrixTotal;
                r.Quantite = 0;
                r.PrixTotal = 0;
            }
            MessageBoxResult messageCommande = MessageBox.Show("Prix de la commande : " + prixCook + " Cook");
            listPanier.Items.Clear();
        }

        private void buttonSupprRecette_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Recette r = button.DataContext as Recette;
            listPanier.Items.Remove(r);
        }

        private void buttonDeconnexion_Click(object sender, RoutedEventArgs e)
        {
            MainWindow w = new MainWindow();
            w.Show();
            this.Close();
        }
    }
}
