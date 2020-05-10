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
    /// Logique d'interaction pour windowAjouterRecette.xaml
    /// </summary>
    public partial class windowAjouterRecette : Window
    {
        windowRecettes recettesCdR;
        Dictionary<Produit, double> ingredientsRecette = new Dictionary<Produit, double>();
        string mailCdR;
        public windowAjouterRecette(windowRecettes mesRecettes, string mail)
        {
            InitializeComponent();
            recettesCdR = mesRecettes;
            foreach (Produit p in RessourceSQL.allProduits)
            {
                boxListeIngredients.Items.Add(p);
            }
            mailCdR = mail;
        }

        private void validerRecette_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //On vérifie que les informations entrées sont correctes 
                if (Convert.ToDouble(boxPrix.Text) < 10 || Convert.ToDouble(boxPrix.Text) > 40)
                {
                    MessageBoxResult messagePrix = MessageBox.Show("Veuillez entrer un prix entre 10 et 40 Cook svp");
                    return;
                }
                else
                {
                    //On crée la nouvelle recette et on l'ajoute à la BDD
                    Recette nouvelleRecette = new Recette(boxNomRecette.Text, boxTypeRecette.Text, ingredientsRecette, BoxDescriptifRecette.Text, Convert.ToDouble(boxPrix.Text));
                    nouvelleRecette.RemunerationCdRCook = 2;
                    nouvelleRecette.MailCdR = mailCdR;
                    RessourceSQL.ajouterRecette(nouvelleRecette);
                    MessageBoxResult message = MessageBox.Show("Recette créée !");
                    recettesCdR.listRecettes.Items.Add(nouvelleRecette);
                    recettesCdR.Show();
                    this.Close();
                }

            }
            catch
            {
                MessageBoxResult message = MessageBox.Show("Informations incorrectes, veuillez vérifier les informations saisies.");
            }
        }

        private void buttonAjouterARecette_Click(object sender, RoutedEventArgs e)
        {
            //On sélectionne les ingrédients à ajouter à la recette
            if (boxListeIngredients.SelectedItem != null)
            {
                Produit p = (Produit)boxListeIngredients.SelectedItem;
                boxSupprIngredients.Items.Add(p);
                try
                {
                    ingredientsRecette.Add(p, Convert.ToDouble(boxQuantite.Text));
                }
                catch
                {
                    MessageBoxResult message = MessageBox.Show("Veuillez vérifier la quantité entrée, veillez à mettre une virgule et non un point s'il s'agit d'une quantité non entière");
                }
                boxListeIngredients.Items.Remove(p);
                boxListeIngredients.Items.Refresh();
                labelUnite.Content = "";
                boxQuantite.Text = "0";
            }
        }

        private void boxListeIngredients_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                if (boxListeIngredients.SelectedItem != null)
                {
                    labelUnite.Content = ((Produit)boxListeIngredients.SelectedItem).Unite;
                }
            }
            catch { }
        }

        private void buttonSupprIngredient_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                boxListeIngredients.Items.Add((Produit)boxSupprIngredients.SelectedItem);
                ingredientsRecette.Remove((Produit)boxSupprIngredients.SelectedItem);
                boxSupprIngredients.Items.Remove((Produit)boxSupprIngredients.SelectedItem);
                boxSupprIngredients.Items.Refresh();
            }
            catch { }
        }

        private void buttonRetour_Click(object sender, RoutedEventArgs e)
        {
            //on possède le mail du cdr
            //on récupère le nom, prenom et le nombre de cook 

            string [] infoCdR = RessourceSQL.nom_prenom_CdR(mailCdR);
            string nom = infoCdR[1];
            string prenom = infoCdR[2] ;
            int nbCook = Convert.ToInt32(infoCdR[3]);

            windowCdR w = new windowCdR(mailCdR, nom, prenom, nbCook);
            w.Show();
            this.Close();
        }
    }
}
