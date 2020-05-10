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
            //On vérifie si le mail et la vérification sont identiques
            bool identique = false;

            string mdp = txtMDP.Text;
            string verif = txtVerification.Text;

            if (mdp == verif)
            {
                identique = true;
            }

            return identique;
        }


        public bool IsNumeric(string Nombre)
        {
            //permet de savoir si le string en entrée peut être converti en int ou non
            try
            {
                int.Parse(Nombre);
                return true;
            }
            catch
            {
                return false;
            }
        }




        private void btnValider_Click2(object sender, RoutedEventArgs e)
        {
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=cooking;UID=root;PASSWORD=" + RessourceSQL.mdp_utilisateur + ";Convert Zero Datetime=True";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            bool verif = Verification_MDP();
            string mail = txtMail.Text;
            

            if (verif==true) //on vérifie que la vérification du mail est correcte
            {
                List<string> liste = RessourceSQL.allMails();
                if (!(liste.Contains(mail))) //on vérifie que le mail n'existe pas déjà
                {
                    if (!(IsNumeric(mail))&&(!IsNumeric(txtPrenom.Text))&&(!IsNumeric(txtNom.Text))) //on vérifie si le nom, le prenom ou le mail en entrée n'est pas une suite numérique
                    {
                        try
                        {
                            //on ajoute le nouveau client à la base de données

                            string nom = txtNom.Text;
                            string prenom = txtPrenom.Text;
                            string mdp = txtMDP.Text;

                            MySqlCommand commandInsertion = connection.CreateCommand();
                            string requeteInsertion = "INSERT INTO client (mailClient, nom, prenom,  mdpClient) VALUES (" + "'" + mail + "'," + "'" + nom + "'," + "'" + prenom + "'," + "'" + mdp + "') ;";
                            commandInsertion.CommandText = requeteInsertion;

                            MySqlDataReader readerInsertion;
                            readerInsertion = commandInsertion.ExecuteReader();

                            connection.Close();

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
                    else
                    {
                        MessageBox.Show("Attention, le nom, le prénom ou le mail ne peut pas être une suite de chiffres.");
                    }
                }
            }

            else
            {
                MessageBox.Show("Le mot de passe entré et la vérification ne sont pas les mêmes, veuillez réessayer.");
            }

            connection.Close();

            
        }

        private void buttonRetour_Click(object sender, RoutedEventArgs e)
        {
            MainWindow w = new MainWindow();
            w.Show();
            this.Close();
        }
    }
}
