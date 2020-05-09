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
    /// Logique d'interaction pour windowGererCdR.xaml
    /// </summary>
    public partial class windowGererCdR : Window
    {
        public windowGererCdR()
        {
            InitializeComponent();
            foreach(string cdr in RessourceSQL.listeCdR())
            {
                boxCdR.Items.Add(cdr);
            }
            foreach (Recette r in RessourceSQL.allRecettes)
            {
                boxRecetteCdR.Items.Add(r);
            }
        }

        private void buttonSupprCdR_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (boxCdR.SelectedItem != null)
                {
                    MessageBoxResult messageSupprCdR = MessageBox.Show("¨Êtes-vous sûr de vouloir supprimer le CdR sélectionné ?", "Supprimer CdR", MessageBoxButton.YesNo);
                    if (messageSupprCdR == MessageBoxResult.Yes)
                    {
                        string[] infoCdR = boxCdR.SelectedItem.ToString().Split(' ');
                        
                        RessourceSQL.supprCdR(infoCdR[0], infoCdR[1]);
                        MessageBoxResult message = MessageBox.Show("Vous avez destitué "+ infoCdR[0]+" "+ infoCdR[1]+" de son statut de CdR, il reste néanmoins client chez Cooking");
                        boxCdR.Items.Remove(boxCdR.SelectedItem);
                        boxCdR.Items.Refresh();
                    }
                }
            }
            catch
            {
                MessageBoxResult message = MessageBox.Show("Veuillez sélectionner un CdR");
            }
        }

        private void boxCdR_DropDownClosed(object sender, EventArgs e)
        {
            boxRecetteCdR.Items.Clear();
            if (boxCdR.SelectedItem != null)
            {
                string[] infoCdR = boxCdR.SelectedItem.ToString().Split(' ');
                foreach (Recette r in RessourceSQL.mesRecettes(RessourceSQL.rechercheMailCdR(infoCdR[0],infoCdR[1])))
                {
                    boxRecetteCdR.Items.Add(r);
                }
            }
            //Sinon on affiche toutes les recettes
            else
            {
                foreach (Recette r in RessourceSQL.allRecettes)
                {
                    boxRecetteCdR.Items.Add(r);
                }
            }
        }

        private void buttonSupprRecette_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RessourceSQL.supprRecette(((Recette)boxRecetteCdR.SelectedItem).Nom, ((Recette)boxRecetteCdR.SelectedItem).MailCdR);
                MessageBoxResult message = MessageBox.Show("Recette supprimée");
                boxRecetteCdR.Items.Remove((Recette)boxRecetteCdR.SelectedItem);
                boxRecetteCdR.Items.Refresh();
            }
            catch
            {
                MessageBoxResult message = MessageBox.Show("Veuillez sélectionner une recette");
            }
        }
    }
}
