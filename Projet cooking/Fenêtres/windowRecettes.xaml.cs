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
            foreach(Recette rCdR in RessourceSQL.mesRecettes("kevin.vaut@gmail.com"))
            {
                listRecettes.Items.Add(rCdR);
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
    }
}
