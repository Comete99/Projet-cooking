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
using System.Data;

namespace Projet_cooking.Fenêtres
{
    /// <summary>
    /// Logique d'interaction pour windowCreationCompte.xaml
    /// </summary>
    public partial class windowCreationCompte : Window
    {

        public windowCreationCompte()
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
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=cooking;UID=root;PASSWORD=Nico72Newbie05;Convert Zero Datetime=True";
            MySqlConnection connection = new MySqlConnection(connectionString);

            ///On liste les mails des clients
            connection.Open();

            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT mailClient from client";

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
            if (tab != null)
            {
                for (int i = 0; i < tab.Length; i++)
                {
                    liste.Add(tab[i]);
                }
            }
            connection.Close();

            ///On ajoute les mails des cdr
            connection.Open();

            MySqlCommand command2 = connection.CreateCommand();
            command2.CommandText = "SELECT mailCdr from cdr";

            MySqlDataReader reader2;
            reader2 = command2.ExecuteReader();
            string resultat2 = "";
            string[] tab2 = null;
            while (reader2.Read())
            {
                string currentRowAsString = "";
                for (int i = 0; i < reader2.FieldCount; i++)
                {
                    string valueAsString = reader.GetValue(i).ToString();
                    currentRowAsString += valueAsString + ",";
                }
                resultat2 = currentRowAsString;
                ///Console.WriteLine(currentRowAsString);
                tab2 = currentRowAsString.Split(',');
            }

            
            for (int i = 0; i < tab2.Length; i++)
            {
                liste.Add(tab[i]);
            }
            connection.Close();


            ///On ajoute la liste des mails des gestionnaires
            connection.Open();

            MySqlCommand command3 = connection.CreateCommand();
            command3.CommandText = "SELECT mailGestionnaire from gestionnaire";

            MySqlDataReader reader3;
            reader3 = command3.ExecuteReader();
            string resultat3 = "";
            string[] tab3 = null;
            while (reader3.Read())
            {
                string currentRowAsString = "";
                for (int i = 0; i < reader3.FieldCount; i++)
                {
                    string valueAsString = reader.GetValue(i).ToString();
                    currentRowAsString += valueAsString + ",";
                }
                resultat3 = currentRowAsString;
                ///Console.WriteLine(currentRowAsString);
                tab3 = currentRowAsString.Split(',');
            }


            for (int i = 0; i < tab3.Length; i++)
            {
                liste.Add(tab[i]);
            }
            connection.Close();

            return liste;
        }



        private void btnValider_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=cooking;UID=root;PASSWORD=Nico72Newbie05;Convert Zero Datetime=True";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            bool verif = Verification_MDP();
            string mail = txtMail.Text;
            

            if (verif==true)
            {
                List<string> liste = Liste_Mails();
                if (!(liste.Contains(mail)))
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
            }

            else
            {
                MessageBox.Show("Le mot de passe entré et la vérification ne sont pas les mêmes, veuillez réessayer.");
            }

            connection.Close();

            MainWindow w = new MainWindow();
            w.Show();
            this.Close();

        }

        private void btnValider_Click2(object sender, RoutedEventArgs e)
        {
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=cooking;UID=root;PASSWORD=Nico72Newbie05;Convert Zero Datetime=True";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            bool verif = Verification_MDP();
            string mail = txtMail.Text;
            

            if (verif)
            {
                List<string> liste = Liste_Mails();
                if (!(liste.Contains(mail)))
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

                        MainWindow w = new MainWindow();
                        w.Show();
                        this.Close();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }

            else
            {
                MessageBox.Show("Le mot de passe entré et la vérification ne sont pas les mêmes, veuillez réessayer.");
            }

            connection.Close();

            
        }
    }
}
