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
            string connectionString = @"Data Source=localhost;Initial Catalog=LocalDB;Integrated Security=True";
            using (DatabaseContext db = new DatabaseContext(connectionString))
            {
                bool isCorrect = false;
                bool isCorrectPassword = false;


                if (txtPesel.Text.Length != 11 || txtNrT.Text.Length != 9 || txtKod.Text.Length !=6 || txtImie.Text.Length == 0 || txtNazwisko.Text.Length == 0 || txtMiasto.Text.Length == 0 || txtAdres.Text.Length == 0 )
                    {
                        MessageBox.Show("Sprawdz wpisane przez Ciebie dane.");
                    }
                    else
                    {
                        isCorrect = true;
                    }
                if (txtHaslo.Password != txtHaslo2.Password)
                {
                    MessageBox.Show("Hasła różnią się od siebie.");
                }
                else
                {
                    isCorrectPassword = true;
                }
                    

                LoginWindow lw = new LoginWindow();
            

                if (isCorrect == true && isCorrectPassword == true)
                {
                    db.Add(new Users { Imie = txtImie.Text, Nazwisko = txtNazwisko.Text, Haslo = txtHaslo.Password, Pesel = txtPesel.Text, NrTel = txtNrT.Text, Saldo = 0 });
                    db.SaveChanges();

                    db.Add(new Adresy { UserID = db.Users.Where(x => x.Pesel == txtPesel.Text).FirstOrDefault().UserID, Adres = txtAdres.Text, KodPocztowy = txtKod.Text, Miasto = txtMiasto.Text });
                    db.SaveChanges();

                    MessageBox.Show("Pomyslnie zarejestrowano, proszę się zalogować.");


                    lw.Show();
                    this.Close();


                    
                }

            }
        }
    }
}
