using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
    /// Interaction logic for ModifyCourse.xaml
    /// </summary>
    public partial class ModifyCourse : Page
    {
        GamalCenterEntities db = new GamalCenterEntities();
        public ModifyCourse()
        {
            InitializeComponent();
            dataset();
        }
        private void dataset()
        {
            datagrid.ItemsSource = db.Courses.Select(x => new
            {
                x.Courses_ID,
                x.Courses_name,
                x.Courses_Grade,
                x.Courses_price
            }).ToList();
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(nametxt.Text) && string.IsNullOrWhiteSpace(gradetxt.Text))
            {
                MessageBox.Show("not data to add");
                return;
            }
            var c = new Cours();
            c.Courses_name = nametxt.Text;
            c.Courses_Grade = gradetxt.Text;
            c.Courses_price = int.Parse(pricetxt.Text);

            db.Courses.Add(c);
            db.SaveChanges();
            MessageBox.Show("added done");
            dataset();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(IDtxt.Text))
            {
                MessageBox.Show("Enter ID first");
                return;
            }
            if (string.IsNullOrWhiteSpace(nametxt.Text) && string.IsNullOrWhiteSpace(gradetxt.Text) && string.IsNullOrWhiteSpace(pricetxt.Text))
            {
                MessageBox.Show("not data to Update");
                return;
            }
            if (string.IsNullOrWhiteSpace(nametxt.Text))
            {
                MessageBox.Show("Course name was empty");
                int id1 = int.Parse(IDtxt.Text);
                var c1 = db.Courses.Where(x => x.Courses_ID == id1).Select(x => x.Courses_name).ToList();
                Cours s = new Cours();
                s.Courses_name = c1.ToString();
            }
            if (string.IsNullOrWhiteSpace(gradetxt.Text))
            {
                MessageBox.Show("Grade was empty");
                int id1 = int.Parse(IDtxt.Text);
                var c1 = db.Courses.Where(x => x.Courses_ID == id1).Select(x => x.Courses_Grade).ToList();
                Cours s = new Cours();
                s.Courses_Grade = c1.ToString();
            }
            if (string.IsNullOrWhiteSpace(pricetxt.Text))
            {
                MessageBox.Show("Price is empty");
                int id1 = int.Parse(IDtxt.Text);
                int? c1 = db.Courses.Where(x => x.Courses_ID == id1).Select(x => x.Courses_price).FirstOrDefault();
                Cours s = new Cours();
                s.Courses_price = c1;
            }
            int id = int.Parse(IDtxt.Text);
            var c = db.Courses.Where(x => x.Courses_ID == id).FirstOrDefault();
            if (c == null)
            {
                MessageBox.Show("Course dosent found");
                return;
            }
            if (!string.IsNullOrWhiteSpace(gradetxt.Text))
            {
                c.Courses_Grade = gradetxt.Text;
            }
            if (!string.IsNullOrWhiteSpace(nametxt.Text))
            {
                c.Courses_name = nametxt.Text;
            }
            if (!string.IsNullOrWhiteSpace(pricetxt.Text))
            { 
            c.Courses_price = int.Parse(pricetxt.Text);
            }

            db.Courses.AddOrUpdate(c);
            db.SaveChanges();
            MessageBox.Show("Course data updated");
            dataset();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(IDtxt.Text))
            {
                MessageBox.Show("Enter ID first");
                return;
            }
            int id = int.Parse(IDtxt.Text);
            Cours c = db.Courses.Where(x => x.Courses_ID == id).FirstOrDefault();

            if (c ==null)
            {
                MessageBox.Show("not record to delete");
                return;
            }

            db.Courses.Remove(c); 
            db.SaveChanges();
            MessageBox.Show("Deleted done");
            dataset();

        }
    }
}
