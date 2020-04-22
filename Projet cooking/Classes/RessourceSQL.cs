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
        //string connectionString = "SERVER=localhost;PORT=3306;DATABASE=cooking;UID=root;PASSWORD=SQL.ESILV.Comete.99;Convert Zero Datetime=True";
        //MySqlConnection connection = new MySqlConnection(connectionString);
        //connection.Open();
        public static List<Recette> recettes = new List<Recette> { };

        public static Recette rechercheRecette(string nom)
        {
            foreach(Recette r in recettes)
            {
                if (r.Nom == nom)
                {
                    return r;
                }
            }
            return null;
        }
    }
}
