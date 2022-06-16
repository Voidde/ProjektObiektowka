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
    /// Logika interakcji dla klasy DebitCardWindow.xaml
    /// </summary>
    public partial class DebitCardWindow : Window
    {
        public DebitCardWindow()
        {
            InitializeComponent();
            var CardNumber = RandomDigits(11);
            var cvv = RandomDigits(3);
            var ExpDate = DateTime.Now.AddYears(5);
            string connectionString = @"Data Source=LAPTOP-VHLI3BSD\SQLEXPRESS;Initial Catalog=LocalDB;Integrated Security=True";
            LoginWindow loginWindow = new LoginWindow();
            
            using (DatabaseContext db = new DatabaseContext(connectionString))
            {
                db.Add(new Karty { UserID = user, NrKarty = CardNumber, CVV = cvv, DataWaznosci = ExpDate });
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