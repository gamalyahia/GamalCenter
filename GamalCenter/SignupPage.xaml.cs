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
    /// Interaction logic for SignupPage.xaml
    /// </summary>
    public partial class SignupPage : Page
    {
        GamalCenterEntities db = new GamalCenterEntities(); 
        public SignupPage()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
        private void signup_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(usertxt.Text))
            {
                MessageBox.Show("please Enter Username");
            }
            else if (string.IsNullOrWhiteSpace(passtxt.Text))
            {
                MessageBox.Show("please Enter password");
            }
            else if (string.IsNullOrWhiteSpace(Cpasstxt.Text))
            {
                MessageBox.Show("please Enter password again !!");
            }
            else if (Cpasstxt.Text != passtxt.Text)
            {
                MessageBox.Show("please Enter password Correctly");
            }
            else if (passtxt.Text.Length < 8)
            {
                MessageBox.Show("please Enter stronger password");
            }
            else
            {
                logindet l = new logindet
                {
                    username = usertxt.Text,
                    Passwordx = passtxt.Text,
                };

                if (db.logindets.Any(x => x.username == l.username))
                {
                    MessageBox.Show("please Enter unique username");
                    return;
                }
                else
                {
                    db.logindets.Add(l);
                    db.SaveChanges();
                    MessageBox.Show("back to login page to login");
                    this.NavigationService.Navigate(new LogInPage());
                }
            }
        }
    }
}
