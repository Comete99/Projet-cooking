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
            //int i = 0;
            //while (i < 5 && i < RessourceSQL.allRecettes.Count)
            //{
            //    listTopRecette.Items.Add(RessourceSQL.allRecettes[i]);
            //    i++;
            //}
            //Recherche du CdR d'Or
            string nomPrenomCdROr = "";
            int maxRecettesCommandees = 0;
            int nbRecettesCommandees = 0;
            foreach (string cdr in RessourceSQL.listeCdR())
            {
                string[] infoCdR = cdr.Split(' ');
                foreach (Recette r in RessourceSQL.mesRecettes(RessourceSQL.rechercheMailCdR(infoCdR[0], infoCdR[1])))
                {
                    if(r.MailCdR== "kevin.vaut@gmail.com")
                    {
                        r.NbCommande = 1;
                    }
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
            int j = 0;
            while (j < 5 && j < recettesCdR_Or.Count)
            {
                listRecetteOr.Items.Add(recettesCdR_Or[j]);
                j++;
            }
            //Recherche du CdR de la semaine
            string nomPrenomCdRSemaine = "";
            int maxRecettesCommandeesSemaine = 0;
            int nbRecettesCommandeesSemaine = 0;
            foreach (string cdr in RessourceSQL.listeCdR())
            {
                string[] infoCdR = cdr.Split(' ');
                foreach (Recette r in RessourceSQL.mesRecettes(RessourceSQL.rechercheMailCdR(infoCdR[0], infoCdR[1])))
                {
                    foreach(DateTime d in r.Commandes)
                    {
                        DateTime week = DateTime.Now;
                        week.Subtract(new DateTime(0, 0, Convert.ToInt32(DateTime.Now.DayOfWeek)));
                        if (d >= week)
                        {
                            nbRecettesCommandeesSemaine++;
                        }
                    }
                }
                if (nbRecettesCommandeesSemaine > maxRecettesCommandeesSemaine)
                {
                    nomPrenomCdRSemaine = cdr;
                    maxRecettesCommandeesSemaine = nbRecettesCommandeesSemaine;
                }
                nbRecettesCommandeesSemaine = 0;
            }
            //On modifie l'affichage CdR Semaine et top 5 des recettes
            labelCdRSemaine.Content += nomPrenomCdRSemaine;
            int k = 0;
            while (k < 5 && k < RessourceSQL.allRecettes.Count)
            {
                listTopRecette.Items.Add(RessourceSQL.allRecettes[k]);
                k++;
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
