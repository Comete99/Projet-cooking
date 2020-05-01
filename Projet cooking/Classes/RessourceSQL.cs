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
            if (reader.FieldCount != 0)
            {
                return true;
            }
            else
            {
                return false;
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
            if (reader.FieldCount != 0)
            {
                return true;
            }
            else
            {
                return false;
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
            if (reader.FieldCount != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static List<Recette> recetteCdR(string mailCdR)
        {
            //string connectionString = "SERVER=localhost;PORT=3306;DATABASE=cooking;UID=root;PASSWORD=SQL.ESILV.Comete.99;Convert Zero Datetime=True";
            //MySqlConnection connection = new MySqlConnection(connectionString);
            //connection.Open();

            //MySqlCommand command = connection.CreateCommand();
            //string requete = "SELECT nomRecette FROM recette WHERE mailCdR=" + "'" + mail + "'" + ";";
            //command.CommandText = requete;

            //MySqlDataReader reader;
            //reader = command.ExecuteReader();
            //List<string> nomRecettes = new List<string> { };
            //while (reader.Read())
            //{
            //    for (int i = 0; i < reader.FieldCount; i++)
            //    {
            //        string valueAsString = reader.GetValue(i).ToString();
            //        nomRecettes.Add(valueAsString);
            //    }
            //}
            List<Recette> recettesCdR = new List<Recette> { };
            foreach(Recette r in allRecettes)
            {
                if (r.MailCdR == mailCdR)
                {
                    recettesCdR.Add(r);
                }
            }
            return recettesCdR;
        }
        public static void toutesRecettes()
        {
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=cooking;UID=root;PASSWORD=SQL.ESILV.Comete.99;Convert Zero Datetime=True";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command = connection.CreateCommand();
            string requete = "SELECT nomRecette, type, ingredients, descriptif, prixVente, remuneration, mailCdR FROM recette;";
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
                recetteTable.RemunerationCdRCook = Convert.ToInt32(reader.GetValue(5));
                recetteTable.MailCdR = reader.GetValue(6).ToString();
                allRecettes.Add(recetteTable);
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
            string requete = "INSERT INTO recette (nomRecette, type, ingredients, descritif, prixVente, remuneration, mailCdR) VALUES (" + "'" + recette.Nom + "'," + "'" + recette.Type + "'," + "'" + recette.Ingredients.ToString() + "'," + "'" + recette.Descriptif + "'," + "'" + recette.PrixVente.ToString() + "'," + "'" + recette.RemunerationCdRCook.ToString() + "'," + "'" + recette.MailCdR + "') ;";
            command.CommandText = requete;
        }
    }
}
