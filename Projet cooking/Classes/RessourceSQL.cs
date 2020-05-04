using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MySql.Data.MySqlClient;

namespace Projet_cooking.Classes
{
    public static class RessourceSQL
    {
        //public static MySqlConnection connection;
        public static List<Recette> allRecettes = new List<Recette> { };
        public static List<Produit> allProduits = new List<Produit> { };

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

        //public static void connexionBDD()
        //{
        //    string connectionString = "SERVER=localhost;PORT=3306;DATABASE=cooking;UID=root;PASSWORD=SQL.ESILV.Comete.99;Convert Zero Datetime=True";
        //    connection = new MySqlConnection(connectionString);
        //    connection.Open();
        //}

        public static bool est_client(string mail, string mdp)
        {
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=cooking;UID=root;PASSWORD=SQL.ESILV.Comete.99;Convert Zero Datetime=True";
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
        public static bool est_CdR(string mail, string mdp)
        {
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=cooking;UID=root;PASSWORD=SQL.ESILV.Comete.99;Convert Zero Datetime=True";
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
        public static bool est_gestionnaire(string mail, string mdp)
        {
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=cooking;UID=root;PASSWORD=SQL.ESILV.Comete.99;Convert Zero Datetime=True";
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

        public static List<string> recetteCdR(string mail)
        {
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=cooking;UID=root;PASSWORD=SQL.ESILV.Comete.99;Convert Zero Datetime=True";
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
        public static void toutesRecettes()
        {
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=cooking;UID=root;PASSWORD=SQL.ESILV.Comete.99;Convert Zero Datetime=True";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command = connection.CreateCommand();
            string requete = "SELECT nomRecette, type, ingredients, descriptif, prixVente FROM recette;";
            command.CommandText = requete;

            MySqlDataReader reader;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Recette recetteTable = new Recette();
                //string currentRowAsString = "";
                recetteTable.Nom = reader.GetValue(0).ToString();
                recetteTable.Type = reader.GetValue(1).ToString();
                Dictionary<string, double> ingredients = new Dictionary<string, double>();
                string[] nomIngredients = reader.GetValue(2).ToString().Split(';');
                foreach(string i in nomIngredients)
                {
                    ingredients.Add(i, 1);
                }
                recetteTable.Descriptif = reader.GetValue(3).ToString();
                recetteTable.PrixVente= Convert.ToDouble(reader.GetValue(4));
                allRecettes.Add(recetteTable);
            }
        }
        public static void tousProduits()
        {
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=cooking;UID=root;PASSWORD=SQL.ESILV.Comete.99;Convert Zero Datetime=True";
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
        }
        public static void CdRPaiementCook(Recette recette, bool commande10, bool commande50)
        {
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=cooking;UID=root;PASSWORD=SQL.ESILV.Comete.99;Convert Zero Datetime=True";
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
                string requeteRecette = "UPDATE recette SET prixVente=" + "'"+ recette.PrixVente.ToString() + "'"+ "AND remuneration=" + "'" + recette.RemunerationCdRCook.ToString() +"'"+ "WHERE mailCdr=" + "'" + recette.MailCdR + "'" + " AND nomRecette=" + "'" + recette.Nom + "'" + ";";
                commandRecette.CommandText = requeteRecette;
                MySqlDataReader readerRecette;
                readerRecette = commandRecette.ExecuteReader();

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
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=cooking;UID=root;PASSWORD=SQL.ESILV.Comete.99;Convert Zero Datetime=True";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command = connection.CreateCommand();
            string ingredients = "";
            foreach (KeyValuePair<Produit, double> produit in recette.Ingredients)
            {
                ingredients+=produit.Key.NomProduit + " " + produit.Value+";";
            }
            ingredients = ingredients.Remove(ingredients.Length - 1);
            string requete = "INSERT INTO recette (nomRecette, type, ingredients, descritif, prixVente, remuneration, mailCdR) VALUES (" + "'" + recette.Nom + "'," + "'" + recette.Type + "'," + "'" + ingredients + "'," + "'" + recette.Descriptif + "'," + "'" + recette.PrixVente.ToString() + "'," + "'" + recette.RemunerationCdRCook.ToString() + "'," + "'" + recette.MailCdR + "') ;";
            command.CommandText = requete;

            MySqlDataReader readerRecette;
            readerRecette = command.ExecuteReader();
        }
        public static string rechercheMailCdR(string nomCdR, string prenomCdR)
        {
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=cooking;UID=root;PASSWORD=SQL.ESILV.Comete.99;Convert Zero Datetime=True";
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
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=cooking;UID=root;PASSWORD=SQL.ESILV.Comete.99;Convert Zero Datetime=True";
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
                }
                CdR.Add(currentRowAsString);
            }
            return CdR;
        }
        public static void supprCdR(string nomCdR, string prenomCdR)
        {
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=cooking;UID=root;PASSWORD=SQL.ESILV.Comete.99;Convert Zero Datetime=True";
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
        }
        public static void supprRecette(string nomRecette, string mailCdR)
        {
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=cooking;UID=root;PASSWORD=SQL.ESILV.Comete.99;Convert Zero Datetime=True";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand command = connection.CreateCommand();

            //On supprime la recette de la table recette
            string requete = "DELETE FROM recette WHERE mailCdR =" + "'" + mailCdR + "'" + " AND nomRecette ="+"'"+nomRecette+"'"+";";
            command.CommandText = requete;
            MySqlDataReader reader;
            reader = command.ExecuteReader();
        }
        public static void devenirCdR(string nom, string prenom)
        {
            //On récupère toutes les informations pour transférer le client dans la table CdR
            //Le mail
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=cooking;UID=root;PASSWORD=SQL.ESILV.Comete.99;Convert Zero Datetime=True";
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
    }
}
