using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_cooking.Classes 
{
    public class Recette : IComparable<Recette>
    {
        string nom;
        string type;
        string descriptif;
        double prixVente;
        double prixTotal=0;
        int remunerationCdRcook;
        string mailCdR;
        int quantite=0;
        int nbCommande = 0;
		Dictionary<string, double> ingredients = new Dictionary<string, double>();
        List<DateTime> commandes = new List<DateTime> { };
        public Recette() { }
        public Recette(string n, string t, Dictionary<string, double> i, string d, double p)
        {
            this.nom = n;
            this.type = t;
            this.ingredients = i;
            this.descriptif = d;
            this.prixVente = p;
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

        public string Descriptif
        {
            get { return this.descriptif; }
            set { this.descriptif = value; }
        }
        public int RemunerationCdRCook
        {
            get { return this.remunerationCdRcook; }
            set { this.remunerationCdRcook = value; }
        }
        public double PrixTotal
        {   get { return this.prixTotal; }
            set { this.prixTotal = value; }
        }
        public int NbCommande
        {
            get { return this.nbCommande; }
            set { this.nbCommande = value; }
        }
        public string MailCdR
        {
            get { return this.mailCdR; }
            set { this.mailCdR = value; }
        }
        public Dictionary<string, double> Ingredients
        {
            get { return this.ingredients; }
            set { this.ingredients = value; }
        }
        public int CompareTo(Recette r)
        {
            return this.nbCommande.CompareTo(r.nbCommande);
        }
    }
}
