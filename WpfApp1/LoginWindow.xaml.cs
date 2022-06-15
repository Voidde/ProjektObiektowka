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
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection s = new SqlConnection(@"Data Source=localhost;Initial Catalog=LocalDB;Integrated Security=True");

            try
            {
                if (s.State == ConnectionState.Closed)
                {
                    s.Open();
                }
                string query = "SELECT COUNT(1) FROM Users WHERE Pesel ='" + txtPesel.Text + "' AND Haslo ='" + txtPassword.Password + "'" + ";
                
                SqlCommand sc = new SqlCommand(query, s);
                sc.CommandType = CommandType.Text;
                int count = Convert.ToInt32(sc.ExecuteScalar());
                if (count == 1)
                {
                    MainWindow mw = new MainWindow();
                    mw.Show();
                    this.Close();
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

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow rw = new RegisterWindow();
            rw.Show();
            this.Close();
        }
    }
}
