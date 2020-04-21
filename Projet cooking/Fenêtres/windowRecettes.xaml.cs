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
        public windowRecettes(windowCdR cdR)
        {
            InitializeComponent();
            currentCdR = cdR;
        }

        private void buttonRetourAuMenu_Click(object sender, RoutedEventArgs e)
        {
            currentCdR.Show();
            this.Close();
        }
    }
}
