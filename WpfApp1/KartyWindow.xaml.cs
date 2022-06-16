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

namespace WpfApp1
{
    /// <summary>
    /// Logika interakcji dla klasy KartyWindow.xaml
    /// </summary>
    public partial class KartyWindow : Window
    {
        public KartyWindow()
        {
            InitializeComponent();
        }

        private void btnGeneruj_Click(object sender, RoutedEventArgs e)
        {
            txtCVV.Text = RandomDigits(3);
            txtNr.Text = RandomDigits(16);
            txtData.Text = DateTime.Now.AddYears(5).ToString().Substring(0,10);
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = @"Data Source=localhost;Initial Catalog=LocalDB;Integrated Security=True";
            using (DatabaseContext db = new DatabaseContext(connectionString))
            {
                if (txtUser.Text.Length == 0)
                {
                    MessageBox.Show("Wpisz ID klienta do którego ma zostać przypisana karta.");
                }
                else if (txtData.Text.Length != 10 || txtNr.Text.Length != 16 || txtCVV.Text.Length != 3)
                {
                    MessageBox.Show("Wpisane przez Ciebie dane są błędne.");
                }
                else
                {
                    db.Add(new Karty { UserID = Int32.Parse(txtUser.Text), CVV = txtCVV.Text, DataWaznosci = DateTime.Parse(txtData.Text), NrKarty = txtNr.Text });
                    db.SaveChanges();
                    MessageBox.Show("Pomyślnie dodano kartę.");
                }
            }
        }

        public string RandomDigits(int length)
        {
            var random = new Random();
            string s = string.Empty;
            for (int i = 0; i < length; i++)
                s = String.Concat(s, random.Next(10).ToString());
            return s;
        }

    }
}
