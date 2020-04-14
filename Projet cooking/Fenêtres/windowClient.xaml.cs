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
    /// Logique d'interaction pour windowClient.xaml
    /// </summary>
    public partial class windowClient : Window
    {
        public windowClient()
        {
            InitializeComponent();
            messageConnection.Text += "Mr Cornichon";
            nbCook.Text += "5";
        }

        private void buttonCdR_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageDevenirCdR = MessageBox.Show("Voulez-vous devenir CdR ?", "Devenir CdR", MessageBoxButton.YesNo);
            if(messageDevenirCdR == MessageBoxResult.Yes)
            {
                windowCdR w = new windowCdR();
                w.Show();
            }
        }
    }
}
