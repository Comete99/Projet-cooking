using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MySql.Data.MySqlClient;
using System.Xml;

namespace Projet_cooking.Classes
{
    public static class RessourceSQL
    {
        //public static MySqlConnection connection;
        public static List<Recette> allRecettes = new List<Recette> { };
        public static List<Produit> allProduits = new List<Produit> { };
        public static string mdp_utilisateur = "SQL.ESILV.Comete.99";

        public static Recette rechercheRecette(string nom)
        {
            foreach(Recette r in allRecettes)
            {
                if (r.Nom == nom)
                {
                    return r;
                }
            }
            return null;
        }
        public static bool est_client(string mail, string mdp)
        {
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=cooking;UID=root;PASSWORD=" + mdp_utilisateur + ";Convert Zero Datetime=True";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command = connection.CreateCommand();
            string requete = "SELECT nom FROM client WHERE mailClient=" +"'"+ mail + "'" + " AND mdpClient=" + "'" + mdp + "'" + ";";
            command.CommandText = requete;

            MySqlDataReader reader;
            reader = command.ExecuteReader();
            string client = "";
            while (reader.Read())
            {
                client = reader.GetValue(0).ToString();
            }
            if (client == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static string[] nom_prenom_Client(string mail)
        {
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=cooking;UID=root;PASSWORD=" + RessourceSQL.mdp_utilisateur + ";Convert Zero Datetime=True";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT nom,prenom FROM Client where mailClient='" + mail + "';";

            MySqlDataReader reader;
            reader = command.ExecuteReader();
            string[] tab = null;

            while (reader.Read())
            {
                string currentRowAsString = "";
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string valueAsString = reader.GetValue(i).ToString();
                    currentRowAsString += valueAsString + ",";
                }
                tab = currentRowAsString.Split(',');
            }
            return tab;
        }
        
        public static bool est_CdR(string mail, string mdp)
        {
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=cooking;UID=root;PASSWORD=" + mdp_utilisateur + ";Convert Zero Datetime=True";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command = connection.CreateCommand();
            string requete = "SELECT nom FROM Cdr WHERE mailCdr=" + "'" + mail + "'" + " AND mdpCdr=" + "'" + mdp + "'" + ";";
            command.CommandText = requete;

            MySqlDataReader reader;
            reader = command.ExecuteReader();
            string CdR = "";
            while (reader.Read())
            {
                CdR = reader.GetValue(0).ToString();
            }
            if (CdR == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static string[] nom_prenom_CdR(string mail)
        {
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=cooking;UID=root;PASSWORD=" + RessourceSQL.mdp_utilisateur + ";Convert Zero Datetime=True";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT mailCdr,nom,prenom,nbCook FROM cdr where mailCdr='" + mail + "';";

            MySqlDataReader reader;
            reader = command.ExecuteReader();
            string[] tab = null;

            while (reader.Read())
            {
                string currentRowAsString = "";
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string valueAsString = reader.GetValue(i).ToString();
                    currentRowAsString += valueAsString + ",";
                }
                tab = currentRowAsString.Split(',');
            }
            return tab;
        }
        public static bool est_gestionnaire(string mail, string mdp)
        {
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=cooking;UID=root;PASSWORD=" + mdp_utilisateur + ";Convert Zero Datetime=True";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command = connection.CreateCommand();
            string requete = "SELECT nom FROM gestionnaire WHERE mailGestionnaire=" + "'" + mail + "'" + " AND mdpGestionnaire=" + "'" + mdp + "'" + ";";
            command.CommandText = requete;

            MySqlDataReader reader;
            reader = command.ExecuteReader();
            string gestionnaire = "";
            while (reader.Read())
            {
                gestionnaire = reader.GetValue(0).ToString();
            }
            if (gestionnaire == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static string[] nom_prenom_Gestionnaire(string mail)
        {
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=cooking;UID=root;PASSWORD=" + RessourceSQL.mdp_utilisateur + ";Convert Zero Datetime=True";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT nom,prenom FROM gestionnaire where mailGestionnaire='" + mail + "';";

            MySqlDataReader reader;
            reader = command.ExecuteReader();
            string[] tab = null;

            while (reader.Read())
            {
                string currentRowAsString = "";
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string valueAsString = reader.GetValue(i).ToString();
                    currentRowAsString += valueAsString + ",";
                }
                tab = currentRowAsString.Split(',');
            }
            return tab;
        }


        public static List<string> allMails()
        {
            List<string> clients = Mails_Client();
            List<string> cdrs = Mails_Cdr();
            List<string> gerants = Mails_Gestionnaire();

            List<string> liste = new List<string>();

            for (int i = 0; i < clients.Count; i++)
            {
                liste.Add(clients[i]);
            }

            for (int i = 0; i < cdrs.Count; i++)
            {
                liste.Add(cdrs[i]);
            }

            for (int i = 0; i < gerants.Count; i++)
            {
                liste.Add(gerants[i]);
            }

            for (int i = 0; i < liste.Count; i++)
            {
                Console.WriteLine(liste[i]);
            }

            return liste;
        }

        public static List<string> Mails_Client()
        {
            List<string> liste = new List<string>();

            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=cooking;UID=root;PASSWORD=Nico72Newbie05;Convert Zero Datetime=True";
            MySqlConnection connection = new MySqlConnection(connectionString);

            ///On liste les mails des clients
            connection.Open();

            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT mailClient from client";

            MySqlDataReader reader;
            reader = command.ExecuteReader();
            string resultat = "";
            string[] tab = null;
            while (reader.Read())
            {
                string currentRowAsString = "";
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string valueAsString = reader.GetValue(i).ToString();
                    currentRowAsString += valueAsString + ",";
                    resultat += currentRowAsString;
                }
                tab = resultat.Split(',');
            }

            if (tab != null)
            {
                for (int i = 0; i < tab.Length; i++)
                {
                    liste.Add(tab[i]);
                }
            }
            connection.Close();


            return liste;
        }

        public static List<string> Mails_Cdr()
        {
            List<string> liste = new List<string>();

            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=cooking;UID=root;PASSWORD=Nico72Newbie05;Convert Zero Datetime=True";
            MySqlConnection connection = new MySqlConnection(connectionString);

            connection.Open();

            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT mailCdr from cdr";

            MySqlDataReader reader;
            reader = command.ExecuteReader();
            string resultat = "";
            string[] tab = null;
            while (reader.Read())
            {
                string currentRowAsString = "";
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string valueAsString = reader.GetValue(i).ToString();
                    currentRowAsString += valueAsString + ",";
                }
                resultat += currentRowAsString;
                ///Console.WriteLine(currentRowAsString);
                tab = resultat.Split(',');
            }

            if (tab != null)
            {
                for (int i = 0; i < tab.Length; i++)
                {
                    liste.Add(tab[i]);
                }
            }

            connection.Close();

            return liste;
        }

        public static List<string> Mails_Gestionnaire()
        {
            List<string> liste = new List<string>();

            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=cooking;UID=root;PASSWORD=Nico72Newbie05;Convert Zero Datetime=True";
            MySqlConnection connection = new MySqlConnection(connectionString);

            connection.Open();

            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT mailGestionnaire from gestionnaire";

            MySqlDataReader reader;
            reader = command.ExecuteReader();
            string resultat = "";
            string[] tab = null;
            while (reader.Read())
            {
                string currentRowAsString = "";
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string valueAsString = reader.GetValue(i).ToString();
                    currentRowAsString += valueAsString + ",";
                }
                resultat += currentRowAsString;
                ///Console.WriteLine(currentRowAsString);
                tab = resultat.Split(',');
            }

            if (tab != null)
            {
                for (int i = 0; i < tab.Length; i++)
                {
                    liste.Add(tab[i]);
                }
            }

            connection.Close();

            return liste;

        }



        public static List<string> recetteCdR(string mail)
        {
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=cooking;UID=root;PASSWORD=" + mdp_utilisateur + ";Convert Zero Datetime=True";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command = connection.CreateCommand();
            string requete = "SELECT nomRecette FROM recette WHERE mailCdR=" + "'" + mail + "'" + ";";
            command.CommandText = requete;

            MySqlDataReader reader;
            reader = command.ExecuteReader();
            List<string> recettes = new List<string> { };
            while (reader.Read())
            {
                //string currentRowAsString = "";
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string valueAsString = reader.GetValue(i).ToString();
                    recettes.Add(valueAsString);
                    //currentRowAsString += valueAsString + ",";
                }
            }
            return recettes;
        }
        public static List<Recette> mesRecettes(string mail)
        {
            List<Recette> recettes = new List<Recette> { };
            foreach(Recette r in allRecettes)
            {
                if (r.MailCdR.Contains(mail))
                {
                    recettes.Add(r);
                }
            }
            return recettes;
        }
        public static void toutesRecettes()
        {
            allRecettes.Clear();
            tousProduits();
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=cooking;UID=root;PASSWORD=" + mdp_utilisateur + ";Convert Zero Datetime=True";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command = connection.CreateCommand();
            string requete = "SELECT nomRecette, type, ingredients, descriptif, prixVente, remuneration, mailCdr, nbCommande FROM recette;";
            command.CommandText = requete;

            MySqlDataReader reader;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Recette recetteTable = new Recette();
                //string currentRowAsString = "";
                recetteTable.Nom = reader.GetValue(0).ToString();
                recetteTable.Type = reader.GetValue(1).ToString();
                Dictionary<Produit, double> ingredients = new Dictionary<Produit, double>();
                string[] Ingredients = reader.GetValue(2).ToString().Split(';');
                foreach(string i in Ingredients)
                {
                    string[] ingredient_quantite = i.Split(' ');
                    foreach(Produit p in allProduits)
                    {
                        if(p.NomProduit == ingredient_quantite[0])
                        {
                            ingredients.Add(p, Convert.ToDouble(ingredient_quantite[1]));
                        }
                    }
                }
                recetteTable.Ingredients = ingredients;
                recetteTable.Descriptif = reader.GetValue(3).ToString();
                recetteTable.PrixVente= Convert.ToDouble(reader.GetValue(4));
                recetteTable.RemunerationCdRCook = reader.GetInt32(5);
                recetteTable.MailCdR = reader.GetValue(6).ToString();
                recetteTable.NbCommande = reader.GetInt32(7);
                allRecettes.Add(recetteTable);
            }
        }
        public static void tousProduits()
        {
            allProduits.Clear();
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=cooking;UID=root;PASSWORD=" + mdp_utilisateur + ";Convert Zero Datetime=True";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command = connection.CreateCommand();
            string requete = "SELECT nomProduit, categorie, unite, stockActuel, stockMin, stockMax, nomFournisseur FROM produit;";
            command.CommandText = requete;

            MySqlDataReader reader;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Produit produitTable = new Produit();
                //string currentRowAsString = "";
                produitTable.NomProduit = reader.GetValue(0).ToString();
                produitTable.Categorie = reader.GetValue(1).ToString();
                produitTable.Unite = reader.GetValue(2).ToString();
                produitTable.StockActuel = reader.GetDouble(3);
                produitTable.StockMin = reader.GetDouble(4);
                produitTable.StockMax = reader.GetDouble(5);
                produitTable.NomFournisseur = reader.GetValue(6).ToString();
                allProduits.Add(produitTable);
            }
            allProduits.Sort();
        }
        public static void CdRPaiementCook(Recette recette, bool commande10, bool commande50)
        {
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=cooking;UID=root;PASSWORD=" + mdp_utilisateur + ";Convert Zero Datetime=True";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            //On met à jour le prix de la recette et sa rémunération en cook si besoin
            if (commande10 || commande50)
            {
                if (commande10 && commande50)
                {
                    recette.PrixVente += 7;
                    recette.RemunerationCdRCook = 4;
                }
                else if (commande10)
                {
                    recette.PrixVente += 2;
                    recette.RemunerationCdRCook = 2;
                }
                else if (commande50)
                {
                    recette.PrixVente += 5;
                    recette.RemunerationCdRCook = 4;
                }

                MySqlCommand commandRecette = connection.CreateCommand();
                string requeteRecette = "UPDATE recette SET prixVente=" + "'" + recette.PrixVente + "'" + ", remuneration=" + recette.RemunerationCdRCook + " AND nbCommande=" + recette.NbCommande + " WHERE mailCdr=" + "'" + recette.MailCdR + "'" + " AND nomRecette=" + "'" + recette.Nom + "'" + ";";
                commandRecette.CommandText = requeteRecette;
                MySqlDataReader readerRecette;
                readerRecette = commandRecette.ExecuteReader();

                //On met à jour le stocks des produits
                foreach (KeyValuePair<Produit, double> produit in recette.Ingredients)
                {
                    MySqlCommand commandProduit = connection.CreateCommand();
                    string requeteProduit = "UPDATE produit SET stockActuel=" + "'" + produit.Key.StockActuel + "'" + "WHERE nomProduit=" + "'" + produit.Key.NomProduit + "' ;";
                    commandProduit.CommandText = requeteProduit;
                    MySqlDataReader readerProduit;
                    readerProduit = commandProduit.ExecuteReader();
                }

            }
            
            //On paie le CdR avec le nb de Cook correspondant
            MySqlCommand commandCdR = connection.CreateCommand();
            string requeteCdR = "UPDATE cdr SET nbCook="+"'"+recette.RemunerationCdRCook.ToString()+"'"+ "WHERE mailCdr=" + "'" + recette.MailCdR + "'" + ";";
            commandCdR.CommandText = requeteCdR;
            MySqlDataReader readerCdR;
            readerCdR = commandCdR.ExecuteReader();

        }
        public static void ajouterRecette(Recette recette)
        {
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=cooking;UID=root;PASSWORD=" + mdp_utilisateur + ";Convert Zero Datetime=True";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command = connection.CreateCommand();
            string ingredients = "";
            foreach (KeyValuePair<Produit, double> produit in recette.Ingredients)
            {
                ingredients+=produit.Key.NomProduit + " " + produit.Value+";";
            }
            ingredients = ingredients.Remove(ingredients.Length - 1);
            string requete = "INSERT INTO recette (nomRecette, type, ingredients, descriptif, prixVente, remuneration, mailCdR) VALUES (" + "'" + recette.Nom + "'," + "'" + recette.Type + "'," + "'" + ingredients + "'," + "'" + recette.Descriptif + "'," + "'" + recette.PrixVente.ToString() + "'," + "'" + recette.RemunerationCdRCook.ToString() + "'," + "'" + recette.MailCdR + "') ;";
            command.CommandText = requete;

            MySqlDataReader readerRecette;
            readerRecette = command.ExecuteReader();
            RessourceSQL.toutesRecettes();
        }
        public static string rechercheMailCdR(string nomCdR, string prenomCdR)
        {
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=cooking;UID=root;PASSWORD=" + mdp_utilisateur + ";Convert Zero Datetime=True";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command = connection.CreateCommand();
            string requete = "SELECT mailCdr FROM cdr WHERE nom =" + "'" + nomCdR + "'" + "AND prenom =" + "'" + prenomCdR + "'" + ";";
            command.CommandText = requete;

            MySqlDataReader reader;
            reader = command.ExecuteReader();
            string mailCdR="";
            while (reader.Read())
            {
                mailCdR = reader.GetValue(0).ToString();
            }  
            return mailCdR;
        }
        public static List<string> listeCdR()
        {
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=cooking;UID=root;PASSWORD=" + mdp_utilisateur + ";Convert Zero Datetime=True";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command = connection.CreateCommand();
            string requete = "SELECT nom, prenom FROM cdr ;";
            command.CommandText = requete;

            MySqlDataReader reader;
            reader = command.ExecuteReader();

            List<string> CdR = new List<string> { };
            while (reader.Read()) 
            {
                string currentRowAsString = "";
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string valueAsString = reader.GetValue(i).ToString();
                    if (i == 0)
                    {
                        currentRowAsString += valueAsString + " ";
                    }
                    else
                    {
                        currentRowAsString += valueAsString;
                    }
                }
                CdR.Add(currentRowAsString);
            }
            return CdR;
        }
        public static void supprCdR(string nomCdR, string prenomCdR)
        {
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=cooking;UID=root;PASSWORD=" + mdp_utilisateur + ";Convert Zero Datetime=True";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand command = connection.CreateCommand();

            string mailCdR = rechercheMailCdR(nomCdR, prenomCdR);

            //On supprime les recettes du CdR de la table recette
            string requete = "DELETE FROM recette WHERE mailCdR =" + "'" + mailCdR + "'" + ";";
            command.CommandText = requete;
            MySqlDataReader reader;
            reader = command.ExecuteReader();

            //On supprime le CdR de la table
            requete = "DELETE FROM cdr WHERE mailCdR =" + "'" + mailCdR + "'" + ";";
            command.CommandText = requete;
            reader = command.ExecuteReader();
            toutesRecettes();
        }
        public static void supprRecette(string nomRecette, string mailCdR)
        {
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=cooking;UID=root;PASSWORD=" + mdp_utilisateur + ";Convert Zero Datetime=True";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand command = connection.CreateCommand();

            //On supprime la recette de la table recette
            string requete = "DELETE FROM recette WHERE mailCdR =" + "'" + mailCdR + "'" + " AND nomRecette ="+"'"+nomRecette+"'"+";";
            command.CommandText = requete;
            MySqlDataReader reader;
            reader = command.ExecuteReader();
            toutesRecettes();
        }
        public static void devenirCdR(string nom, string prenom)
        {
            //On récupère toutes les informations pour transférer le client dans la table CdR
            //Le mail
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=cooking;UID=root;PASSWORD=" + mdp_utilisateur + ";Convert Zero Datetime=True";
            MySqlConnection connectionMail = new MySqlConnection(connectionString);
            connectionMail.Open();

            MySqlCommand commandMail = connectionMail.CreateCommand();
            string requeteMail = "SELECT mailClient FROM client WHERE nom =" + "'" + nom + "'" + "AND prenom =" + "'" + prenom + "'" + ";";
            commandMail.CommandText = requeteMail;

            MySqlDataReader readerMail;
            readerMail = commandMail.ExecuteReader();
            string mailCdR = "";
            while (readerMail.Read())
            {
                mailCdR = readerMail.GetValue(0).ToString();
            }

            connectionMail.Close();

            //Le mot de passe
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command = connection.CreateCommand();
            string requete = "SELECT mdpClient FROM client WHERE nom =" + "'" + nom + "'" + "AND prenom =" + "'" + prenom + "'" + ";";
            command.CommandText = requete;

            MySqlDataReader reader;
            reader = command.ExecuteReader();
            string mdpClient = "";
            while (reader.Read())
            {
                mdpClient = reader.GetValue(0).ToString();
            }
            connection.Close();

            //On l'insère dans la table CdR
            MySqlConnection connectionInsertion = new MySqlConnection(connectionString);
            connectionInsertion.Open();
            MySqlCommand commandInsertion = connectionInsertion.CreateCommand();
            string requeteInsertion = "INSERT INTO cdr (mailCdr, nom, prenom, nbCook, mdpCdr) VALUES (" + "'" + mailCdR + "'," + "'" + nom + "'," + "'" + prenom + "'," + 0 + "," + "'" + mdpClient + "') ;";
            commandInsertion.CommandText = requeteInsertion;

            MySqlDataReader readerInsertion;
            readerInsertion = commandInsertion.ExecuteReader();

            connectionInsertion.Close();

            //On le supprime de la table client
            MySqlConnection connectionSuppression = new MySqlConnection(connectionString);
            connectionSuppression.Open();
            MySqlCommand commandSuppression = connectionSuppression.CreateCommand();
            string requeteSuppression = "DELETE FROM client WHERE mailClient =" + "'" + mailCdR + "'"+ " ;";
            commandSuppression.CommandText = requeteSuppression;

            MySqlDataReader readerSuppression;
            readerSuppression = commandSuppression.ExecuteReader();
            connectionSuppression.Close();
        }


        public static int NbClients()
        {
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=cooking;UID=root;PASSWORD=" + mdp_utilisateur + ";Convert Zero Datetime=True";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            
            MySqlCommand command = connection.CreateCommand();
            string requete = "Select count(*) from client";
            command.CommandText = requete;

            MySqlDataReader reader;
            reader = command.ExecuteReader();
            string res = "";
            int count;
            while (reader.Read())
            {
                res = reader.GetValue(0).ToString();
            }
            count = Convert.ToInt32(res);

            connection.Close();

            return count;
        }


        public static int NbRecettes()
        {
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=cooking;UID=root;PASSWORD=" + mdp_utilisateur + ";Convert Zero Datetime=True";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command = connection.CreateCommand();
            string requete = "Select count(*) from recette";
            command.CommandText = requete;

            MySqlDataReader reader;
            reader = command.ExecuteReader();
            string res = "";
            int count;
            while (reader.Read())
            {
                res = reader.GetValue(0).ToString();
            }
            count = Convert.ToInt32(res);

            connection.Close();

            return count;
        }
        public static List<string> listeFournisseur()
        {
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=cooking;UID=root;PASSWORD=" + mdp_utilisateur + ";Convert Zero Datetime=True";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command = connection.CreateCommand();
            string requete = "SELECT nomFournisseur FROM fournisseur ;";
            command.CommandText = requete;

            MySqlDataReader reader;
            reader = command.ExecuteReader();

            List<string> fournisseurs = new List<string> { };
            while (reader.Read())
            {
                string currentRowAsString = "";
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string valueAsString = reader.GetValue(i).ToString();
                    currentRowAsString += valueAsString;

                }
                fournisseurs.Add(currentRowAsString);
            }
            return fournisseurs;
        }
        public static void commandeProduit(string nomProduit, double quantiteCommandee, double stockActuel)
        {
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=cooking;UID=root;PASSWORD=" + mdp_utilisateur + ";Convert Zero Datetime=True";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            //On met à jour les stocks
            MySqlCommand commandCdR = connection.CreateCommand();
            double stock = quantiteCommandee + stockActuel;
            string requeteCdR = "UPDATE produit SET stockActuel=" + "'" + stock + "'" + "WHERE nomProduit=" + "'" + nomProduit + "'" + ";";
            commandCdR.CommandText = requeteCdR;
            MySqlDataReader readerCdR;
            readerCdR = commandCdR.ExecuteReader();
            RessourceSQL.tousProduits();
        }
        public static void commandesProduitsXml()
        {
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=cooking;UID=root;PASSWORD=" + mdp_utilisateur + ";Convert Zero Datetime=True";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT nomFournisseur, nomProduit, (stockMax-stockActuel), unite FROM produit ORDER BY nomFournisseur, nomProduit;"; //WHERE stockActuel<stockMin
            MySqlDataReader reader;
            reader = command.ExecuteReader();


            XmlDocument docXml = new XmlDocument();

            // création de l'élément racine ... qu'on ajoute au document
            XmlElement racine = docXml.CreateElement("Réapprovisionnement_Hebdomadaire");
            docXml.AppendChild(racine);

            // création et insertion de l'en-tête XML (no <=> pas de DTD associée)
            XmlDeclaration xmldecl = docXml.CreateXmlDeclaration("1.0", "UTF-8", "no");
            docXml.InsertBefore(xmldecl, racine);

            XmlNode fournisseur = docXml.CreateElement("Fournisseur");
            string nomFournisseur = "";

            while (reader.Read())
            {
                if (reader.GetValue(0).ToString()!=nomFournisseur)
                {
                    XmlNode newFournisseur = docXml.CreateElement("Fournisseur");
                    nomFournisseur = reader.GetValue(0).ToString();
                    newFournisseur.InnerText = nomFournisseur;
                    racine.AppendChild(newFournisseur);
                    fournisseur = newFournisseur;
                }
                string nomProduit = reader.GetValue(1).ToString();
                double quantiteCommandee = reader.GetDouble(2);
                string unite = reader.GetValue(3).ToString();

                XmlElement produit = docXml.CreateElement("Produit");
                produit.InnerText = nomProduit;
                fournisseur.AppendChild(produit);

                XmlElement quantite = docXml.CreateElement("Quantité");
                quantite.InnerText = quantiteCommandee.ToString()+" "+unite;
                produit.AppendChild(quantite);
            }
            // enregistrement du document XML ==> à retrouver dans le dossier bin\ debug de Visual Studio
            docXml.Save("Reapprovisionnement_Hebdo.xml");
        }




    }
}
