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
    /// Logique d'interaction pour windowGestionnaire.xaml
    /// </summary>
    public partial class windowGestionnaire : Window
    {
        public windowGestionnaire(string nom, string prenom)
        {
            InitializeComponent();
            messageConnection.Text += " " + nom + " " + prenom;
            RessourceSQL.allRecettes.Sort();

            //Recherche du CdR d'Or
            string nomPrenomCdROr = "";
            int maxRecettesCommandees = 0;
            int nbRecettesCommandees = 0;
            foreach (string cdr in RessourceSQL.listeCdR())
            {
                string[] infoCdR = cdr.Split(' ');
                foreach (Recette r in RessourceSQL.mesRecettes(RessourceSQL.rechercheMailCdR(infoCdR[0], infoCdR[1])))
                {
                    nbRecettesCommandees += r.NbCommande;
                }
                if (nbRecettesCommandees > maxRecettesCommandees)
                {
                    nomPrenomCdROr = cdr;
                    maxRecettesCommandees = nbRecettesCommandees;
                }
                nbRecettesCommandees = 0;
            }
            //On modifie alors l'affichage
            labelCdROr.Content += nomPrenomCdROr;
            string[] CdR_Or = nomPrenomCdROr.Split(' ');
            List<Recette> recettesCdR_Or = RessourceSQL.mesRecettes(RessourceSQL.rechercheMailCdR(CdR_Or[0], CdR_Or[1]));
            recettesCdR_Or.Sort();
            int j = recettesCdR_Or.Count()-1;
            int compteur = 5;
            while (j >0 && compteur>0)
            {
                listRecetteOr.Items.Add(recettesCdR_Or[j]);
                j--;
                compteur--;
            }

            //Recherche du CdR de la semaine
            string nomPrenomCdRSemaine = "";
            int maxRecettesCommandeesSemaine = -1;
            int nbRecettesCommandeesSemaine = 0;
            foreach (string cdr in RessourceSQL.listeCdR())
            {
                string[] infoCdR = cdr.Split(' ');
                foreach (Recette r in RessourceSQL.mesRecettes(RessourceSQL.rechercheMailCdR(infoCdR[0], infoCdR[1])))
                {
                    foreach(DateTime d in r.Commandes)
                    {
                        
                        nbRecettesCommandeesSemaine++;
                        
                    }
                }
                if (nbRecettesCommandeesSemaine > maxRecettesCommandeesSemaine)
                {
                    nomPrenomCdRSemaine = cdr;
                    maxRecettesCommandeesSemaine = nbRecettesCommandeesSemaine;
                }
                nbRecettesCommandeesSemaine = 0;
            }
            //On modifie l'affichage CdR Semaine
            labelCdRSemaine.Content += nomPrenomCdRSemaine;

            //Top 5 des recettes
            int count = 5;
            int k = RessourceSQL.allRecettes.Count()-1;
            while (count > 0 && k > 0)
            {
                listTopRecette.Items.Add(RessourceSQL.allRecettes[k]);
                k--;
                count--;
            }
        }

        private void buttonGererStock_Click(object sender, RoutedEventArgs e)
        {
            windowGererStocks w = new windowGererStocks(this);
            w.Show();
            this.Hide();
        }

        private void buttonGererCdR_Click(object sender, RoutedEventArgs e)
        {
            windowGererCdR w = new windowGererCdR(this);
            w.Show();
            this.Hide();
        }

        private void buttonDeconnexion_Click(object sender, RoutedEventArgs e)
        {
            MainWindow w = new MainWindow();
            w.Show();
            this.Close();
        }
    }
}
