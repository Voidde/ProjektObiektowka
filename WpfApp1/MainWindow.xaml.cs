using Microsoft.VisualBasic.ApplicationServices;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            
                // Hint: change `DESKTOP-123ABC\SQLEXPRESS` to your server name
                //       alternatively use `localhost` or `localhost\SQLEXPRESS`

                string connectionString = @"Data Source=localhost;Initial Catalog=LocalDB;Integrated Security=True";

                using (DatabaseContext db = new DatabaseContext(connectionString))
                {

                }
            
        

    }
        // Transfer button which open TransactionWindow
        private void Transfer_Button_Click(object sender, RoutedEventArgs e)
        {
            TransactionWindows transactionWindows = new TransactionWindows();
            transactionWindows.Show();
        }

        private void DebitCard_Button_Click(object sender, RoutedEventArgs e)
        {
            DebitCardWindow debitCardWindow = new DebitCardWindow();
            debitCardWindow.Show();
        }

        private void IN_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
