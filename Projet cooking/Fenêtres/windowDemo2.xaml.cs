﻿using Projet_cooking.Classes;
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
    /// Logique d'interaction pour windowDemo2.xaml
    /// </summary>
    public partial class windowDemo2 : Window
    {
        public windowDemo2()
        {
            InitializeComponent();
            int count = RessourceSQL.NbClients();
            nbClients.Text = Convert.ToString(count);
        }

        private void btnDemo_Click(object sender, RoutedEventArgs e)
        {
            windowDemo3 w = new windowDemo3();
            w.Show();
            this.Hide();
        }
    }
}
