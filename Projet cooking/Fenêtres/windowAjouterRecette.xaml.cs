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
        Dictionary<string, double> ingredients = new Dictionary<string, double>();
        List<string> ingredientsDisponibles = new List<string> { };
        public windowAjouterRecette(windowRecettes mesRecettes)
        {
            InitializeComponent();
            recettesCdR = mesRecettes;
            labelUnite.Content = "ml";
        }

        private void validerRecette_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                Recette nouvelleRecette = new Recette(boxNomRecette.Text, boxTypeRecette.Text, ingredients, BoxDescriptifRecette.Text, Convert.ToDouble(boxPrix.Text));
                MessageBoxResult message = MessageBox.Show("Recette ajoutée !");
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
            ingredients.Add(boxListeIngredients.SelectedItem.ToString(), Convert.ToDouble(boxQuantite));
        }

    }
}
