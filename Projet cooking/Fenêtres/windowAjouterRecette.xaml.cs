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
    /// Logique d'interaction pour windowAjouterRecette.xaml
    /// </summary>
    public partial class windowAjouterRecette : Window
    {
        windowRecettes recettesCdR;
        public windowAjouterRecette(windowRecettes mesRecettes)
        {
            InitializeComponent();
            recettesCdR = mesRecettes;
        }
    }
}
