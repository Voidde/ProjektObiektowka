using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Data;
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


        public MainWindow()
        {
            InitializeComponent();
            
        }
        SqlConnection cn;
        SqlCommand cmd;
        SqlDataReader dr;
        // adding data to credit card and amount of the account to main window
        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = @"Data Source=DESKTOP-F09P8H4\SQLEXPRESS;Initial Catalog=LocalDB;Integrated Security=True";
            using (DatabaseContext db = new DatabaseContext(connectionString))
            {
                
                var UIDq = db.Karty.Where(x => x.NrKarty.Equals(CardNum_TextBox.Text)).Select(x => x.UserID).FirstOrDefault();
                var Saldoq = db.Users.Where(x => x.UserID.Equals(UIDq)).Select(x => x.Saldo).FirstOrDefault();

                if (AccountNumber_Button.Text.Length == 0 || Amount_button.Text.Length == 0 || dpDate.Text.Length == 0 || Amount_button.Text.Any(char.IsLetter) || !Amount_button.Text.All(char.IsDigit) )
                {
                    MessageBox.Show("Wprowadz poprawdne dane do przelewu.");
                }
                else if (decimal.Parse(Amount_button.Text) <= 0 )
                {
                    MessageBox.Show("Kwota przelewu powinna być większa od 0");
                }
                else if (decimal.Parse(Amount_button.Text) > Saldoq)
                {
                    MessageBox.Show("Zbyt niskie saldo");
                }
                else
                {
                    db.Add(new Przelew { DataPrzelewu = dpDate.DisplayDate, Kwota = long.Parse(Amount_button.Text), UserID = UIDq, NaKonto = AccountNumber_Button.Text });
                    db.SaveChanges();

                    MessageBox.Show("Pomyślnie wykonano przelew");

                    User user1 = db.Users.Where(x => x.UserID.Equals(UIDq)).First();

                    var przelew = decimal.Parse(Amount_button.Text);

                    user1.Saldo = user1.Saldo - przelew;
                    db.SaveChanges();
                    this.UpdateLayout();
                    

                    try
                    {
                        User user2 = db.Users.Where(x => x.UserID.Equals(int.Parse(AccountNumber_Button.Text))).FirstOrDefault();
                        if (!user2.Equals(null))
                        {
                            user2.Saldo = user2.Saldo + przelew;
                            db.SaveChanges();
                        }
                    }
                    catch (System.NullReferenceException)
                    {
                    }

                }
            }
        }
        // Binding data to list box
        private void BindComboBox_Load(object sender, EventArgs e)
        {
            cn = new SqlConnection(@"Data Source=DESKTOP-F09P8H4\SQLEXPRESS;Initial Catalog=LocalDB;Integrated Security=True");
            cn.Open();

            BindData();
        }
        public void BindData()
        {
            cmd = new SqlCommand("select  Kwota  from Przelewy", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Transaction_ListBox.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        // logout button
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow lw = new LoginWindow();
            lw.Show();
            this.Close();
          
        }
        // refreshing button for listbox

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            Transaction_ListBox.Items.Clear();
            BindComboBox_Load( sender,e);

        }
    }


 }


   
    

