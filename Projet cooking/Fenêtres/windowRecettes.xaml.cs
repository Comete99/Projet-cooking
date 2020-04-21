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
    /// Logique d'interaction pour windowRecettes.xaml
    /// </summary>
    public partial class windowRecettes : Window
    {
        private windowCdR currentCdR;
        static Dictionary<string, double> ingredients = new Dictionary<string, double>();
        Recette recette1 = new Recette("Galettes de quinoa", "Test", ingredients, "c'est bon", 6, 2, "jean.dupont@cook.com");
        public windowRecettes(windowCdR cdR)
        {
            InitializeComponent();
            currentCdR = cdR;
            listRecettes.Items.Add(recette1);
            listRecettes.Items.Refresh();
        }

        private void buttonRetourAuMenu_Click(object sender, RoutedEventArgs e)
        {
            currentCdR.Show();
            this.Close();
        }

        private void buttonAjouterRecette_Click(object sender, RoutedEventArgs e)
        {
            windowAjouterRecette w = new windowAjouterRecette(this);
            this.Hide();
            w.Show();
        }
    }
}
