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
        Dictionary<string, double> ingredientsRecette = new Dictionary<string, double>();
        List<string> ingredientsDisponibles = new List<string> { };
        string mailCdR;
        public windowAjouterRecette(windowRecettes mesRecettes, string mail)
        {
            InitializeComponent();
            recettesCdR = mesRecettes;
            labelUnite.Content = "ml";
            foreach (string i in ingredientsDisponibles)
            {
                boxListeIngredients.Items.Add(i);
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
                recettesCdR.Show();
                recettesCdR.listRecettes.Items.Refresh();
                this.Close();
            }
            catch
            {
                MessageBoxResult message = MessageBox.Show("Informations incorrectes, veuillez vérifier les informations saisies.");
            }

        }

        private void buttonAjouterARecette_Click(object sender, RoutedEventArgs e)
        {
            boxSupprIngredients.Items.Add(boxListeIngredients.SelectedItem);
            boxListeIngredients.Items.Remove(boxListeIngredients.SelectedItem);
            ingredientsRecette.Add(boxListeIngredients.SelectedItem.ToString(), Convert.ToDouble(boxQuantite));
        }

        private void buttonSupprIngredient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                boxListeIngredients.Items.Add(boxSupprIngredients.SelectedItem);
                ingredientsRecette.Remove(boxSupprIngredients.SelectedItem.ToString());
                boxSupprIngredients.Items.Remove(boxSupprIngredients.SelectedItem);
            }
            catch { }
        }
    }
}
