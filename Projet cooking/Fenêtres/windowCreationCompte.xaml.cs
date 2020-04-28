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
using System.Data;

namespace Projet_cooking.Fenêtres
{
    /// <summary>
    /// Logique d'interaction pour windowCreationCompte.xaml
    /// </summary>
    public partial class windowCreationCompte : Window
    {

        public windowCreationCompte(MainWindow main)
        {
            InitializeComponent();
        }


        public bool Verification_MDP()
        {
            bool identique = false;

            string mdp = txtMDP.Text;
            string verif = txtVerification.Text;

            if (mdp == verif)
            {
                identique = true;
            }

            return identique;
        }


        public List<string> Liste_Mails()
        {
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=loueur;UID=root;PASSWORD=Nico72Newbie05;Convert Zero Datetime=True";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT mailClient, mailCdr, mailGestionnare FROM client, cdr, gestionnaire";

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

            List<string> liste = null;
            for(int i = 0; i < tab.Length; i++)
            {
                liste.Add(tab[i]);
            }

            return liste;
        }



        private void btnValider_Click(object sender, RoutedEventArgs e, MainWindow main)
        {
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=cooking;UID=root;PASSWORD=Nico72Newbie05;Convert Zero Datetime=True";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            bool verif = Verification_MDP();
            string mail = txtMail.Text;
            List<string> liste = Liste_Mails();

            if (verif && !(liste.Contains(mail)))
            {
                try
                {
                    string query = "insert into client values(@mailClient,@nom,@prenom,@mdpClient)";
                    MySqlCommand sqlCmd = new MySqlCommand(query, connection);
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.AddWithValue("@mailClient", txtMail.Text);
                    sqlCmd.Parameters.AddWithValue("@mdpClient", txtMDP.Text);
                    sqlCmd.Parameters.AddWithValue("@nom", txtNom.Text);
                    sqlCmd.Parameters.AddWithValue("@prenom", txtPrenom.Text);

                    MessageBox.Show("Vous êtes désormais un client de Cooking. Félicitations !");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            else
            {
                MessageBox.Show("Le mot de passe entré et la vérification ne sont pas les mêmes, veuillez réessayer.");
            }

            connection.Close();
            main.Show();

        }
    }
}
