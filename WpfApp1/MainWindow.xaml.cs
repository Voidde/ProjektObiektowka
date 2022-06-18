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

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = @"Data Source=localhost;Initial Catalog=LocalDB;Integrated Security=True";
            using (DatabaseContext db = new DatabaseContext(connectionString))
            {
                
                var UIDq = db.Karty.Where(x => x.NrKarty.Equals(CardNum_TextBox.Text)).Select(x => x.UserID).FirstOrDefault();
                var Saldoq = db.Users.Where(x => x.UserID.Equals(UIDq)).Select(x => x.Saldo).FirstOrDefault();    
               
                if (AccountNumber_Button.Text.Length == 0 || Amount_button.Text.Length == 0 || dpDate.Text.Length == 0 )
                {
                    MessageBox.Show("Wprowadz dane do przelewu.");
                }
                else if (decimal.Parse(Amount_button.Text) <= 0)
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


            }


    }
    

