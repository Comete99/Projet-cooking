using Projet_cooking.Fenêtres;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using MySql.Data.MySqlClient;
using System.Data;
using Projet_cooking.Classes;

namespace Projet_cooking
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            RessourceSQL.toutesRecettes();
            RessourceSQL.tousProduits();
            RessourceSQL.commandesProduitsXml();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            string mail = txtMail.Text;
            string mdp = txtMDP.Text;

            if (RessourceSQL.est_client(mail, mdp))
            {
                string connectionString = "SERVER=localhost;PORT=3306;DATABASE=cooking;UID=root;PASSWORD=SQL.ESILV.Comete.99;Convert Zero Datetime=True";
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT nom,prenom FROM Client where mailClient='"+mail+"';";

                MySqlDataReader reader;
                reader = command.ExecuteReader();
                string[] tab = null;

                while (reader.Read())
                {
                    string currentRowAsString = "";
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        string valueAsString = reader.GetValue(i).ToString();
                        currentRowAsString += valueAsString + ",";
                    }
                    tab = currentRowAsString.Split(',');
                }

                windowClient w = new windowClient(tab[0], tab[1]);
                this.Visibility = Visibility.Hidden;
                w.Show();
            }

            if (RessourceSQL.est_CdR(mail, mdp))
            {
                string connectionString = "SERVER=localhost;PORT=3306;DATABASE=cooking;UID=root;PASSWORD=SQL.ESILV.Comete.99;Convert Zero Datetime=True";
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT mailCdr,nom,prenom,nbCook FROM cdr where mailCdr='" + mail + "';";

                MySqlDataReader reader;
                reader = command.ExecuteReader();
                string[] tab = null;

                while (reader.Read())
                {
                    string currentRowAsString = "";
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        string valueAsString = reader.GetValue(i).ToString();
                        currentRowAsString += valueAsString + ",";
                    }
                    tab = currentRowAsString.Split(',');
                }

                windowCdR w = new windowCdR(tab[0], tab[1],tab[2],Convert.ToInt32(tab[3]));
                this.Visibility = Visibility.Hidden;
                w.Show();
            }

            if (RessourceSQL.est_gestionnaire(mail, mdp))
            {
                string connectionString = "SERVER=localhost;PORT=3306;DATABASE=cooking;UID=root;PASSWORD=SQL.ESILV.Comete.99;Convert Zero Datetime=True";
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT nom,prenom FROM gestionnaire where mailGestionnaire='" + mail + "';";

                MySqlDataReader reader;
                reader = command.ExecuteReader();
                string[] tab = null;

                while (reader.Read())
                {
                    string currentRowAsString = "";
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        string valueAsString = reader.GetValue(i).ToString();
                        currentRowAsString += valueAsString + ",";
                    }
                    tab = currentRowAsString.Split(',');
                }

                windowGestionnaire w = new windowGestionnaire(tab[0], tab[1]);
                this.Visibility = Visibility.Hidden;
                w.Show();
            }

        }


        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            windowCreationCompte w = new windowCreationCompte();
            this.Visibility = Visibility.Hidden;
            w.Show();
        }

        private void btnDemo_Click(object sender, RoutedEventArgs e)
        {
            windowDemo w = new windowDemo();
            this.Visibility = Visibility.Hidden;
            w.Show();
        }
    }
}
