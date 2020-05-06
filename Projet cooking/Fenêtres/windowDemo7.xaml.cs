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
    /// Logique d'interaction pour windowDemo7.xaml
    /// </summary>
    public partial class windowDemo7 : Window
    {
        public windowDemo7()
        {
            InitializeComponent();
        }

        private void btnDemo_Click(object sender, RoutedEventArgs e)
        {
            MainWindow w = new MainWindow();
            w.Show();
            this.Hide();
        }
    }
}
