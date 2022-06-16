﻿using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string RandomDigits(int length)
        {
            var random = new Random();
            string s = string.Empty;
            for (int i = 0; i < length; i++)
                s = String.Concat(s, random.Next(10).ToString());
            return s;
        }

        public MainWindow()
        {
            InitializeComponent();
          

        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = @"Data Source=localhost;Initial Catalog=LocalDB;Integrated Security=True";
            using (DatabaseContext db = new DatabaseContext(connectionString))
            {
                if (AccountNumber_Button.Text.Length == 0 || Amount_button.Text.Length == 0 || dpDate.Text.Length == 0)
                {
                    MessageBox.Show("Wprowadz dane do przelewu.");
                }
                else if (Amount_button.Text == "0")
                {
                    MessageBox.Show("Kwota przelewu powinna być większa od 0");
                }
                //else if (long.Parse(Amount_button.Text) > /*Tutaj ma byc Saldo,jak zrobimy */)
                //{
                //    MessageBox.Show("Zbyt niskie saldo");
                //}
                else
                {
                    db.Add(new Przelewy { DataPrzelewu = dpDate.DisplayDate, Kwota = long.Parse(Amount_button.Text), UserID = db.Karty.Where(x => x.NrKarty.Equals(CardNum_TextBox.Text)).Select(x => x.UserID).FirstOrDefault(), NaKonto = AccountNumber_Button.Text });
                    db.SaveChanges();
                    MessageBox.Show("Pomyślnie wykonano przelew");
                }
                }
                }


            }


    }
    

