using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_cooking.Classes
{
    class Recette
    {
        string nom;
        string type;
        string descriptif;
        double prixVente;
        int remunerationCdRcook;
        string mailCdR;
        int quantite=0;
        int nbCommande = 0;
		Dictionary<string, double> ingredients = new Dictionary<string, double>();

        public Recette(string n, string t, Dictionary<string, double> i, string d, double p, int r, string m)
        {
            this.nom = n;
            this.type = t;
            this.ingredients = i;
            this.descriptif = d;
            this.prixVente = p;
            this.remunerationCdRcook = r;
            this.mailCdR = m;
        }
        public string Nom
        {
            get { return this.nom; }
            set { this.nom = value; }
        }
        public double PrixVente
        {
            get { return this.prixVente; }
            set { this.prixVente = value; }
        }
        public int Quantite
        {
            get { return this.quantite; }
            set { this.quantite = value; }
        }
        public string Type
        {
            get { return this.type; }
            set { this.type = value; }
        }
        public int PrixCook
        {
            get { return this.remunerationCdRcook; }
            set { this.remunerationCdRcook = value; }
        }
        public int NbCommande
        {
            get { return this.nbCommande; }
            set { this.nbCommande = value; }
        }
    }
}
