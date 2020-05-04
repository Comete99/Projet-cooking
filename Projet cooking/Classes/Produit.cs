using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_cooking.Classes
{
    public class Produit
    {
        string nomProduit;
        string categorie;
        string unite;
        double stockActuel;
        double stockMin;
        double stockMax;
        string nomFournisseur;
        public Produit()
        {

        }
        public string NomProduit
        {
            get { return this.nomProduit; }
            set { this.nomProduit = value; }
        }
        public string Categorie
        {
            get { return this.categorie; }
            set { this.categorie = value; }
        }
        public string Unite
        {
            get { return this.unite; }
            set { this.unite = value; }
        }
        public double StockActuel
        {
            get { return this.stockActuel; }
            set { this.stockActuel = value; }
        }
        public double StockMin
        {
            get { return this.stockMin; }
            set { this.stockMin = value; }
        }
        public double StockMax
        {
            get { return this.stockMax; }
            set { this.stockMax = value; }
        }
        public string NomFournisseur
        {
            get { return this.nomFournisseur; }
            set { this.nomFournisseur = value; }
        }
    }
}
