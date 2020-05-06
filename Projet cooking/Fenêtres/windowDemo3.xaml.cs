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
            string cdr = (string)boxListeCdr.SelectedItem;
            string[] tab = cdr.Split(' ');
            string mail = RessourceSQL.rechercheMailCdR(tab[0], tab[1]);
            List<string> listeRecettes = RessourceSQL.recetteCdR(mail);

            nbRecettes.Text = Convert.ToString(listeRecettes.Count);

            int count = 0;

            if (boxListeCdr.SelectedItem != null)
            {
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
