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
                string[] tab = RessourceSQL.nom_prenom_Client(mail);
                windowClient w = new windowClient(tab[0], tab[1]);
                this.Visibility = Visibility.Hidden;
                w.Show();
            }

            if (RessourceSQL.est_CdR(mail, mdp))
            {
                string[] tab = RessourceSQL.nom_prenom_CdR(mail);
                windowCdR w = new windowCdR(tab[0], tab[1],tab[2],Convert.ToInt32(tab[3]));
                this.Visibility = Visibility.Hidden;
                w.Show();
            }

            if (RessourceSQL.est_gestionnaire(mail, mdp))
            {
                string[] tab = RessourceSQL.nom_prenom_Gestionnaire(mail);
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
            

            Flash_Unlock w = new Flash_Unlock();
            this.Visibility = Visibility.Hidden;
            w.Show();

        }
    }
}
