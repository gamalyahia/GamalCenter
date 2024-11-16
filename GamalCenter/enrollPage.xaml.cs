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
    /// Interaction logic for enrollPage.xaml
    /// </summary>
    public partial class EnrollPage : Page
    {
        GamalCenterEntities db = new GamalCenterEntities();
        public EnrollPage(string name)
        {
            InitializeComponent();
            nametxt.Text = name;
            
            Setcombo();
        }

        private void Setcombo()
        {
            combo.ItemsSource = db.Courses.Select(x =>  x.Courses_name ).ToList();
            combo1.ItemsSource = db.Courses.Select(x => x.Courses_Grade ).ToList();
        } 

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(nametxt.Text))
            {
                MessageBox.Show($"please Enter Name");
                return;
            }
            else if (string.IsNullOrWhiteSpace(agestxt.Text))
            {
                MessageBox.Show($"please Enter Age");
                return;
            }
            else if (string.IsNullOrWhiteSpace(addresstxt.Text))
            {
                MessageBox.Show($"please Enter Address");
                return;
            }
            else if (string.IsNullOrWhiteSpace(phonetxt.Text))
            {
                MessageBox.Show($"please Enter Phone number");
                return;
            }
            else if (combo.SelectedItem == null)
            {
                MessageBox.Show($"please select Course");
                return;
            }
            else if (combo1.SelectedItem == null)
            {
                MessageBox.Show($"please select Grade");
                return;
            }
            else 
            {

                if (db.students.Any(x => x.Student_name == nametxt.Text))
                {
                    MessageBox.Show("you allrady book a course");
                    return;
                }

                var c = db.Courses.Where(x => x.Courses_name == combo.SelectedItem.ToString()).Select(x => x.Courses_ID).FirstOrDefault();

                var c1 = db.Courses.Where(x => x.Courses_Grade == combo1.SelectedItem.ToString()).Select(x => x.Courses_ID).FirstOrDefault();

                student s = new student
                {
                    Student_name = nametxt.Text,
                    Student_age = int.Parse(agestxt.Text),
                    Student_adress = addresstxt.Text,
                    Student_phone = phonetxt.Text,
                };
                s.Courses_name = c;
                s.Courses_Grade = c1;

                db.students.Add(s);
                db.SaveChanges();

                MessageBox.Show("Booking Done !!");
            }
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
