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
        public windowRecettes(windowCdR cdR)
        {
            InitializeComponent();
            currentCdR = cdR;
            foreach(Recette r in RessourceSQL.mesRecettes("kevin.vaut@gmail.com"))
            {
                listRecettes.Items.Add(r);
            }
        }

        private void buttonRetourAuMenu_Click(object sender, RoutedEventArgs e)
        {
            currentCdR.Show();
            this.Close();
        }

        private void buttonAjouterRecette_Click(object sender, RoutedEventArgs e)
        {
            string mail = "kevin.vaut@gmail.com";
            windowAjouterRecette w = new windowAjouterRecette(this, mail);
            this.Hide();
            w.Show();
        }

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
                listRecettes.Items.Remove(r);
                RessourceSQL.supprRecette(r.Nom, r.MailCdR);
            }
        }

        //private void boxProduits_DropDownClosed(object sender, EventArgs e)
        //{
        //    ComboBox comboBox = sender as ComboBox;
        //    Produit p = comboBox.SelectedItem as Produit;
        //    Recette r = comboBox.DataContext as Recette;
        //    TextBox textBox = sender as TextBox;
        //    textBox.Text = r.Ingredients[p].ToString();
        //}
    }
}
