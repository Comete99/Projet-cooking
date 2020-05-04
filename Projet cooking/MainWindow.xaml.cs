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
                    command.CommandText = "SELECT nom,prenom FROM Client where mailClient='kevin.vaut@gmail.com';";

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
            
        }


        private bool EstClient(string mail, string mdp)
        {
            bool estClient = false;

            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=cooking;UID=root;PASSWORD=SQL.ESILV.Comete.99;Convert Zero Datetime=True";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT count(1) FROM Client where mailClient=='" + mail + " and mdpClient='" + mdp + "';";

            MySqlDataReader reader;
            reader = command.ExecuteReader();

            int count = 0;
            string c = "";

            while (reader.Read())
            {
                string currentRowAsString = "";
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string valueAsString = reader.GetValue(i).ToString();
                    currentRowAsString += valueAsString;
                }
                c = currentRowAsString;
                count = Convert.ToInt32(c);
            }

            if (count == 1)
            {
                estClient = true;

            }

            connection.Close();

            return estClient;
        }


        private bool EstCdr(string mail, string mdp)
        {
            bool estCdr = false;

            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=cooking;UID=root;PASSWORD=SQL.ESILV.Comete.99;Convert Zero Datetime=True";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT count(1) FROM Cdr where mailCdr=='" + mail + " and mdpCdr='" + mdp + "';";

            MySqlDataReader reader;
            reader = command.ExecuteReader();

            int count = 0;
            string c = "";

            while (reader.Read())
            {
                string currentRowAsString = "";
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string valueAsString = reader.GetValue(i).ToString();
                    currentRowAsString += valueAsString;
                }
                c = currentRowAsString;
                count = Convert.ToInt32(c);
            }

            if (count == 1)
            {
                estCdr = true;

            }

            connection.Close();

            return estCdr;
        }

        private bool EstGestionnaire(string mail, string mdp)
        {
            bool estGest = false;

            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=cooking;UID=root;PASSWORD=SQL.ESILV.Comete.99;Convert Zero Datetime=True";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT count(1) FROM Gestionnaire where mailGestionnaire=='" + mail + " and mdpGestionnaire='" + mdp + "';";

            MySqlDataReader reader;
            reader = command.ExecuteReader();

            int count = 0;
            string c = "";

            while (reader.Read())
            {
                string currentRowAsString = "";
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string valueAsString = reader.GetValue(i).ToString();
                    currentRowAsString += valueAsString;
                }
                c = currentRowAsString;
                count = Convert.ToInt32(c);
            }

            if (count == 1)
            {
                estGest = true;

            }

            connection.Close();

            return estGest;
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            windowCreationCompte w = new windowCreationCompte();
            this.Visibility = Visibility.Hidden;
            w.Show();
        }
    }
}
