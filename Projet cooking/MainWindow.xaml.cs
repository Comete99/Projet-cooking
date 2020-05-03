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
            windowClient w = new windowClient();
            w.Show();
        }
        
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=cooking;UID=root;PASSWORD=Nico72Newbie05;Convert Zero Datetime=True";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            try
            {
                string query = "select count(1) from client where mailClient=@mailClient and mdpClient=@mdpClient;";
                MySqlCommand sqlCmd = new MySqlCommand(query, connection);
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@mailClient", txtMail.Text);
                sqlCmd.Parameters.AddWithValue("@mdpClient", txtMDP.Text);
                int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                if (count == 1)
                {
                    windowClient w = new windowClient();
                    this.Visibility = Visibility.Hidden;
                    w.Show();
                    
                }
                

                string query2 = "select count(1) from cdr where mailCdr=@mailCdr and mdpCdr=@mdpCdr;";
                MySqlCommand sqlCmd2 = new MySqlCommand(query2, connection);
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@mailCdr", txtMail.Text);
                sqlCmd.Parameters.AddWithValue("@mdpCdr", txtMDP.Text);
                int count2 = Convert.ToInt32(sqlCmd.ExecuteScalar());
                if (count2 == 1)
                {
                    windowCdR w = new windowCdR();
                    this.Visibility = Visibility.Hidden;
                    w.Show();
                }
                
                string query3 = "select count(1) from gestionnaire where mailGestionnaire=@mailGestionnaire and mdpGestionnaire=@mdpGestionnaire;";
                MySqlCommand sqlCmd3 = new MySqlCommand(query3, connection);
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@mailGestionnaire", txtMail.Text);
                sqlCmd.Parameters.AddWithValue("@mdpGestionnaire", txtMDP.Text);
                int count3 = Convert.ToInt32(sqlCmd.ExecuteScalar());
                if (count3 == 1)
                {
                    windowGestionnaire w = new windowGestionnaire();
                    this.Visibility = Visibility.Hidden;
                    w.Show();
                }

                else
                {
                    MessageBox.Show("Le mail et le mot de passe ne correspondent pas.");
                }

                    
  
                
            }
            catch(Exception ex)
            {
                MessageBox.Show("Le mail et le mot de passe ne correspondent pas.");
            }
            finally
            {

            }

        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            windowCreationCompte w = new windowCreationCompte();
            this.Visibility = Visibility.Hidden;
            w.Show();
        }
    }
}
