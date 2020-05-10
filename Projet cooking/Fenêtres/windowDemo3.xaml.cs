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

namespace Projet_cooking.Fenêtres
{
    /// <summary>
    /// Logique d'interaction pour windowDemo3.xaml
    /// </summary>
    public partial class windowDemo3 : Window
    {
        public windowDemo3()
        {
            InitializeComponent();
            int countCdr = RessourceSQL.listeCdR().Count;
            nbCdr.Text = Convert.ToString(countCdr);

            //on remplit la combobox avec la liste des Cdr
            foreach (string s in RessourceSQL.listeCdR())
            {
                boxListeCdr.Items.Add(s);
            }
            boxListeCdr.Items.Refresh();
        }
       

        private void btnDemo_Click(object sender, RoutedEventArgs e)
        {
            windowDemo4 w = new windowDemo4();
            w.Show();
            this.Hide();
        }

        private void boxListeCdr_DropDownClosed(object sender, EventArgs e)
        {
            int count = 0;

            //on vérifie que le sélection n'est pas null
            if (boxListeCdr.SelectedItem != null)
            {
                //on récupère les infos sur le cdr : son nom et son prenom
                string[] infoCdR = boxListeCdr.SelectedItem.ToString().Split(' ');
                foreach (Recette r in RessourceSQL.allRecettes)
                {
                    //On compte les recettes en liées au CdR sélectionné
                    if (RessourceSQL.rechercheMailCdR(infoCdR[0], infoCdR[1]).Contains(r.MailCdR))
                    {
                        count++;
                    }
                }
            }

            nbRecettes.Text = Convert.ToString(count);
        }
    }
}
