using Projet_cooking.Classes;
using Renci.SshNet.Messages.Transport;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Logique d'interaction pour windowRecettes.xaml
    /// </summary>
    public partial class windowRecettes : Window
    {
        private windowCdR currentCdR;
        string mailCdR = "";
        public windowRecettes(windowCdR cdR, string mail)
        {
            InitializeComponent();
            currentCdR = cdR;
            foreach(Recette r in RessourceSQL.mesRecettes(mail))
            {
                listRecettes.Items.Add(r);
            }
            mailCdR = mail;
        }

        private void buttonRetourAuMenu_Click(object sender, RoutedEventArgs e)
        {
            currentCdR.boxListeRecettes.Items.Clear();
            foreach(Recette r in RessourceSQL.allRecettes)
            {
                currentCdR.boxListeRecettes.Items.Add(r);
            }
            currentCdR.Show();
            this.Close();
        }

        private void buttonAjouterRecette_Click(object sender, RoutedEventArgs e)
        {
            windowAjouterRecette w = new windowAjouterRecette(this, mailCdR);
            this.Hide();
            w.Show();
        }

        //Fonctions liées aux recettes présentes dans la list view
        private void buttonInfoRecette_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Recette r = button.DataContext as Recette;
            MessageBoxResult descriptionRecette = MessageBox.Show(r.Descriptif);
        }

        private void buttonSupprRecette_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Recette r = button.DataContext as Recette;
            MessageBoxResult supprimerRecette = MessageBox.Show("Êtes-vous sûr de vouloir supprimer cette recette ?", "Supprimer recette", MessageBoxButton.YesNo);
            if (supprimerRecette == MessageBoxResult.Yes)
            {
                currentCdR.boxListeRecettes.Items.Remove(r);
                if (currentCdR.listPanier.Items.Contains(r))
                {
                    currentCdR.listPanier.Items.Remove(r);
                }
                listRecettes.Items.Remove(r);
                RessourceSQL.supprRecette(r.Nom, r.MailCdR);
            }
        }
    }
}
