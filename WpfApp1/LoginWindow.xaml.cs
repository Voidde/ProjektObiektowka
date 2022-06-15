﻿using System;
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

namespace WpfApp1
{
    /// <summary>
    /// Logika interakcji dla klasy LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
            if(txtPesel.Text =="admin" )
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                mw.Close();
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow rw = new RegisterWindow();
            rw.Show();
            this.Close();
        }
    }
}