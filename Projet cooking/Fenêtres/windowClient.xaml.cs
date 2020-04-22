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
        public static Dictionary<string, double> ingredientsRecette1 = new Dictionary<string, double>();
        Recette recette1 = new Recette("Galettes de quinoa", "Plat", ingredientsRecette1, "Plat parfait pour les végatariens", 5, 2, "jean.dupont@gmail.com");
        public windowClient()
        {
            InitializeComponent();
            messageConnection.Text += "Mr Cornichon";
            nbCook.Text += "0";
            boxNbRecette.Text = "5";
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
            boxNbRecette.Text = Convert.ToString(Convert.ToInt32(boxNbRecette.Text) + 1);
        }

        private void buttonRetirerRecette_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt32(boxNbRecette.Text) > 0)
            {
                boxNbRecette.Text = Convert.ToString(Convert.ToInt32(boxNbRecette.Text) - 1);
            }
        }

        private void buttonAjouterPanier_Click(object sender, RoutedEventArgs e)
        {
            if (!listPanier.Items.Contains(recette1))
            {
                listPanier.Items.Add(recette1);
                recette1.Quantite += 1;
            }
            else
            {
                recette1.Quantite += 1;
                listPanier.Items.Refresh();
            }
        }
    }
}
