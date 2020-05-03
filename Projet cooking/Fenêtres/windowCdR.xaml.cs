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
using MySql.Data.MySqlClient;

namespace Projet_cooking.Fenêtres
{
    /// <summary>
    /// Logique d'interaction pour windowCdR.xaml
    /// </summary>
    public partial class windowCdR : Window
    {
        static Dictionary<string, double> ingredients = new Dictionary<string, double>();
        Recette recette1 = new Recette("Galettes de quinoa", "Test", ingredients, "c'est bon", 6);
        int prixCook=15;
        int nbCookCdR = 30;
        
        List<string> panier;
        public windowCdR()
        {
            InitializeComponent();
            messageConnection.Text += "Mr Cornichon";
            nbCook.Text += "30";
            boxNbRecette.Text = "0";
            boxListeRecettes.Items.Add(recette1);
            boxListeRecettes.Items.Refresh();
            RessourceSQL.allRecettes.Add(recette1);
        }

        public windowCdR(string mail, string nom, string prenom, int nombCook)
        {
            InitializeComponent();
            messageConnection.Text += nom+" "+prenom;
            nbCook.Text += Convert.ToString(nombCook);

            int nbRec = nbRecettes(mail);

            boxNbRecette.Text = Convert.ToString(nbRec) ;
            boxListeRecettes.Items.Add(recette1);
            boxListeRecettes.Items.Refresh();
            RessourceSQL.allRecettes.Add(recette1);
        }

        public int nbRecettes(string mail)
        {
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=cooking;UID=root;PASSWORD=Nico72Newbie05;Convert Zero Datetime=True";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT nomRecette from Recette where mailCdr='"+mail+"';";

            MySqlDataReader reader;
            reader = command.ExecuteReader();
            string resultat = "";
            string[] tab = null;
            while (reader.Read())
            {
                string currentRowAsString = "";
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string valueAsString = reader.GetValue(i).ToString();
                    currentRowAsString += valueAsString + ",";
                }
                resultat = currentRowAsString;
                ///Console.WriteLine(currentRowAsString);
                tab = currentRowAsString.Split(',');
            }

            int count = tab.Length;

            return count;
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

        private void buttonPaiementCB_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
