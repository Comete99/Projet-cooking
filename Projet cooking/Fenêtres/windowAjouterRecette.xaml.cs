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
                Recette nouvelleRecette = new Recette(boxNomRecette.Text, boxTypeRecette.Text, ingredientsRecette, BoxDescriptifRecette.Text, Convert.ToDouble(boxPrix.Text));
                nouvelleRecette.RemunerationCdRCook = 2;
                nouvelleRecette.MailCdR = mailCdR;
                RessourceSQL.ajouterRecette(nouvelleRecette);
                MessageBoxResult message = MessageBox.Show("Recette créée !");
                recettesCdR.listRecettes.Items.Refresh();
                recettesCdR.Show();
                this.Close();
            }
            catch
            {
                MessageBoxResult message = MessageBox.Show("Informations incorrectes, veuillez vérifier les informations saisies.");
            }

        }

        private void buttonAjouterARecette_Click(object sender, RoutedEventArgs e)
        {
            boxSupprIngredients.Items.Add((Produit)boxListeIngredients.SelectedItem);
            ingredientsRecette.Add((Produit)boxListeIngredients.SelectedItem, Convert.ToDouble(boxQuantite.Text));
            boxListeIngredients.Items.Remove((Produit)boxListeIngredients.SelectedItem);
            labelUnite.Content = "";
        }

        private void buttonSupprIngredient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                boxListeIngredients.Items.Add((Produit)boxSupprIngredients.SelectedItem);
                ingredientsRecette.Remove((Produit)boxSupprIngredients.SelectedItem);
                boxSupprIngredients.Items.Remove((Produit)boxSupprIngredients.SelectedItem);
            }
            catch { }
        }

        private void boxListeIngredients_DropDownClosed(object sender, EventArgs e)
        {
            labelUnite.Content = (((Produit)boxListeIngredients.SelectedItem)).Unite;
        }
    }
}
