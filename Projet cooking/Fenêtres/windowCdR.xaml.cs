﻿using Projet_cooking.Classes;
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
    /// Logique d'interaction pour windowCdR.xaml
    /// </summary>
    public partial class windowCdR : Window
    {
        double nbCookCdR = 0;
        string mailCdR = "";
        
        public windowCdR(string mail, string nom, string prenom, int nombCook)
        {
            InitializeComponent();
            messageConnection.Text += nom + " " + prenom;
            nbCook.Text = Convert.ToString(nombCook);
            boxNbRecette.Text = "0"; ///nb de recettes commandées
            foreach (Recette r in RessourceSQL.allRecettes)
            {
                boxListeRecettes.Items.Add(r);
            };
            nbCookCdR = Convert.ToDouble(nombCook);
            mailCdR = mail;
        }

        private void buttonAjouterRecette_Click(object sender, RoutedEventArgs e)
        {
            boxNbRecette.Text = Convert.ToString(Convert.ToInt32(boxNbRecette.Text) + 1);
        }

        private void buttonRetirerRecette_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt32(boxNbRecette.Text) > 0)
            {
                boxNbRecette.Text = Convert.ToString(Convert.ToInt32(boxNbRecette.Text) - 1);
            }
        }

        private void buttonPaiementCook_Click(object sender, RoutedEventArgs e)
        {
            foreach (Recette r in listPanier.Items)
            {
                //Verifier stock
                foreach (KeyValuePair<Produit, double> produit in r.Ingredients)
                {
                    if (produit.Value * r.Quantite > produit.Key.StockActuel)
                    {
                        r.Quantite = 0;
                        listPanier.Items.Remove(r);
                        MessageBoxResult message = MessageBox.Show("Veuillez-nous excuser mais la recette : " + r.Nom + " ne peut pas être commandée par manque de " + produit.Key.NomProduit + "cette recette a été retirée de votre panier, veuillez repasser la commande si vous le souhaitez \nStock Actuel : "+ produit.Key.StockActuel + "\n Quantité pour recette :"+ produit.Value);
                        return;
                    }
                }
            }
            int prixCook = 0;
            foreach (Recette r in listPanier.Items)
            {
                //On ajuste les stocks
                foreach (KeyValuePair<Produit, double> produit in r.Ingredients)
                {
                    produit.Key.StockActuel -= produit.Value * r.Quantite;
                }
                //rémunérer CdR
                
                if (r.NbCommande < 10 && r.NbCommande + r.Quantite >= 10)
                {
                    if (r.NbCommande + r.Quantite >= 50)
                    {
                        r.NbCommande += r.Quantite;
                        RessourceSQL.CdRPaiementCook(r, true, true);
                    }
                    else
                    {
                        r.NbCommande += r.Quantite;
                        RessourceSQL.CdRPaiementCook(r, true, false);
                    }
                }
                else if (r.NbCommande < 50 && r.NbCommande + r.Quantite >= 50)
                {
                    r.NbCommande += r.Quantite;
                    RessourceSQL.CdRPaiementCook(r, false, true);
                }
                else
                {
                    r.NbCommande += r.Quantite;
                    RessourceSQL.CdRPaiementCook(r, false, false);
                }
                for (int i = 0; i < r.Quantite; i++)
                {
                    r.Commandes.Add(DateTime.Now);
                }
                prixCook += Convert.ToInt32(r.PrixTotal);
                r.Quantite = 0;
                r.PrixTotal = 0;
                r.Commandes.Add(DateTime.Now);
            }
            //On met à jour les cook du CdR qui paie
            if (RessourceSQL.nbCookCdR(mailCdR) >= prixCook)
            {
                MessageBoxResult messageCommande = MessageBox.Show("Prix de la commande : " + prixCook + " Cook");
                nbCook.Text = RessourceSQL.miseAjourNbCook(mailCdR, true, prixCook).ToString();
                listPanier.Items.Clear();
            }
            else
            {
                MessageBoxResult messageCommande = MessageBox.Show("Vous n'avez pas assez de Cook. \nPrix de la commande : " + prixCook + " Cook. \nLe montant restant est de : " + (prixCook - RessourceSQL.nbCookCdR(mailCdR)) / 3 + " €, il a été crédité sur votre CB.");
                nbCook.Text = RessourceSQL.miseAjourNbCook(mailCdR, false, prixCook).ToString();
                listPanier.Items.Clear();
            }
        }

        //Le CdR peut accéder à ses recettes
        private void buttonRecettes_Click(object sender, RoutedEventArgs e)
        {
            windowRecettes w = new windowRecettes(this, mailCdR);
            w.Show();
            this.Hide();
        }

        //Ajouter une recette au panier
        private void buttonAjouterPanier_Click(object sender, RoutedEventArgs e)
        {
            Recette recette = (Recette)boxListeRecettes.SelectedItem;
            if (!listPanier.Items.Contains(recette))
            {
                recette.Quantite += Convert.ToInt32(boxNbRecette.Text);
                recette.PrixTotal += recette.PrixVente * Convert.ToInt32(boxNbRecette.Text);
                listPanier.Items.Add(recette);
            }
            else
            {
                recette.Quantite += Convert.ToInt32(boxNbRecette.Text); ;
                recette.PrixTotal += recette.PrixVente * Convert.ToInt32(boxNbRecette.Text);
                listPanier.Items.Refresh();
            }
            boxNbRecette.Text = "0";
        }

        private void buttonPaiementCB_Click(object sender, RoutedEventArgs e)
        {
            foreach (Recette r in listPanier.Items)
            {
                //Verifier stock
                foreach (KeyValuePair<Produit, double> produit in r.Ingredients)
                {
                    if (produit.Value * r.Quantite > produit.Key.StockActuel)
                    {
                        r.Quantite = 0;
                        listPanier.Items.Remove(r);
                        MessageBoxResult message = MessageBox.Show("Veuillez-nous excuser mais la recette : " + r.Nom + " ne peut pas être commandée par manque de " + produit.Key.NomProduit + ". \nCette recette a été retirée de votre panier, veuillez repasser la commande si vous le souhaitez. \nStock Actuel : " + produit.Key.StockActuel + "\nQuantité pour recette : " + produit.Value*r.Quantite);
                        return;
                    }
                }
            }
            double prixCook = 0;
            foreach (Recette r in listPanier.Items)
            {
                //On ajuste les stocks
                foreach (KeyValuePair<Produit, double> produit in r.Ingredients)
                {
                    produit.Key.StockActuel -= produit.Value * r.Quantite;
                }
                //rémunérer CdR et ajustement des stocks dans la BDD
                if (r.NbCommande < 10 && r.NbCommande + r.Quantite >= 10)
                {
                    if (r.NbCommande + r.Quantite >= 50)
                    {
                        r.NbCommande += r.Quantite;
                        RessourceSQL.CdRPaiementCook(r, true, true);
                    }
                    else
                    {
                        r.NbCommande += r.Quantite;
                        RessourceSQL.CdRPaiementCook(r, true, false);
                    }
                }
                else if (r.NbCommande < 50 && r.NbCommande + r.Quantite >= 50)
                {
                    r.NbCommande += r.Quantite;
                    RessourceSQL.CdRPaiementCook(r, false, true);
                }
                else
                {
                    r.NbCommande += r.Quantite;
                    RessourceSQL.CdRPaiementCook(r, false, false);
                }
                for (int i = 0; i < r.Quantite; i++)
                {
                    r.Commandes.Add(DateTime.Now);
                }
                prixCook += r.PrixTotal;
                r.Quantite = 0;
                r.PrixTotal = 0;
                r.Commandes.Add(DateTime.Now);
            }
            MessageBoxResult messageCommande = MessageBox.Show("Prix de la commande : " + prixCook + " Cook");
            listPanier.Items.Clear();
            nbCook.Text = RessourceSQL.nbCookCdR(mailCdR).ToString();
        }

        //Supprimer une recette du panier
        private void buttonSupprRecette_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Recette r = button.DataContext as Recette;
            listPanier.Items.Remove(r);
        }

        private void buttonDeconnexion_Click(object sender, RoutedEventArgs e)
        {
            MainWindow w = new MainWindow();
            w.Show();
            this.Close();
        }
    }
    
}
