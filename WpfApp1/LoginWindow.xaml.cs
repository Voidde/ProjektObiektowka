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
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data;
using Microsoft.EntityFrameworkCore;
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
       
        public void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection s = new SqlConnection(@"Data Source=LAPTOP-VHLI3BSD\SQLEXPRESS;Initial Catalog=LocalDB;Integrated Security=True");

            try
            {
                if (s.State == ConnectionState.Closed)
                {
                    s.Open();
                }
                string query = "SELECT COUNT(1) FROM Users WHERE Pesel ='" + txtPesel.Text + "' AND Haslo ='" + txtPassword.Password + "'";
                SqlCommand sc = new SqlCommand(query, s);
                sc.CommandType = CommandType.Text;
                int count = Convert.ToInt32(sc.ExecuteScalar());
                

                if (count == 1)
                {
                    MW();
                }
                else
                {
                    MessageBox.Show("Błędne login lub hasło.");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                s.Close();
            }
        }
        public void MW()
        {
            string label = "";
            long balance = 0;
            int userID = 0;
            using (DatabaseContext db = new DatabaseContext(@"Data Source=LAPTOP-VHLI3BSD\SQLEXPRESS;Initial Catalog=LocalDB;Integrated Security=True"))
            {
                var query2 = from x in db.Users
                             where x.Pesel == txtPesel.Text
                             select x.Imie;

                foreach (var i in query2)
                {
                    label = i.ToString();
                }
                var query3 = from x in db.Users
                             where x.Pesel == txtPesel.Text
                             select x.UserID;
                foreach (var userid in query3)
                {
                    userID = userid; 
                }

            }
            MainWindow mw = new MainWindow();
            mw.IN.Text = "Witaj! " + label;
            mw.AccountBalance_TextBox.Text = balance.ToString() + " PLN";
            mw.Show();
            this.Close();
            mw.user = userID;
        }
        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow rw = new RegisterWindow();
            rw.Show();
            this.Close();
        }
    }
}
