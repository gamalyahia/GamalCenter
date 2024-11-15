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

namespace GamalCenter
{
    /// <summary>
    /// Interaction logic for LogInPage.xaml
    /// </summary>
    public partial class LogInPage : Page
    {
        GamalCenterEntities db = new GamalCenterEntities();
        public LogInPage()
        {
            InitializeComponent();
        }

        private void signup_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new SignupPage());
        }
        private void login_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(usertxt.Text))
            {
                MessageBox.Show("Please Enter Username");
            }
            else if (string.IsNullOrWhiteSpace(passtxt.Text))
            {
                MessageBox.Show("Please Enter Password");
            }
            else if(usertxt.Text == "admin" && passtxt.Text == "12345")
            {
                MessageBox.Show("admin !! \n welcome back");
                this.NavigationService.Navigate(new adminPage());
            }
            else
            {
                var log = db.logindets.Where(x => x.username == usertxt.Text)
                                           .Select(x => new { x.username, x.Passwordx })
                                           .FirstOrDefault();

                if (log != null && log.username == usertxt.Text && log.Passwordx == passtxt.Text)
                {
                    MessageBox.Show($"welcome {log.username}");
                    this.NavigationService.Navigate(new enrollPage(log.username));
                }
                else
                {
                    MessageBox.Show("Invalid username or password", "Error");
                }
            }
        }
    }
}
