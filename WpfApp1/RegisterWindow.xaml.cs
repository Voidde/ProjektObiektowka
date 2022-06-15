using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Logika interakcji dla klasy RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = @"Data Source=LAPTOP-VHLI3BSD\SQLEXPRESS;Initial Catalog=LocalDB;Integrated Security=True";
            using (DatabaseContext db = new DatabaseContext(connectionString))
            {
                db.Add(new Users { Imie = txtImie.Text, Nazwisko = txtNazwisko.Text, Haslo = txtHaslo.Password, Pesel = txtPesel.Text, NrTel = txtNrT.Text, Saldo = 0 });
                db.SaveChanges();
               
                db.Add(new Adresy {UserID = db.Users.Where(x => x.Pesel == txtPesel.Text).FirstOrDefault().UserID, Adres = txtAdres.Text, KodPocztowy = txtKod.Text, Miasto = txtMiasto.Text });
                db.SaveChanges();
            }
        }
    }
}
