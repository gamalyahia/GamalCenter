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
    /// Interaction logic for adminPage.xaml
    /// </summary>
    public partial class adminPage : Page
    {
        GamalCenterEntities db = new GamalCenterEntities();
        public adminPage()
        {
            InitializeComponent();
            SetData();
        }

        private void SetData()
        {

            datagrid.ItemsSource = db.students.Select(x => new { 
                x.Student_ID,
                x.Student_name, 
                x.Student_adress, 
                x.Student_age, 
                x.Student_phone, 
                x.Cours.Courses_name, 
                x.Cours1.Courses_Grade,
                x.Cours.Courses_price
            }).ToList();

            var courseNames = db.Courses
                .Select(x => x.Courses_name)
                .Distinct()
                .ToList();

            courseNames.Add("All");

            combo.ItemsSource = courseNames;

             var courseGrade = db.Courses
                .Select(x => x.Courses_Grade)
                .Distinct().ToList();

            courseGrade.Add("All");

            combo1.ItemsSource = courseGrade;
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }


        private void Search_Click(object sender, RoutedEventArgs e)
        {
            var query = db.students.AsQueryable();

            if (combo.SelectedItem != null && combo.SelectedItem.ToString() != "All")
            {
                query = query.Where(x => x.Cours.Courses_name == combo.SelectedItem.ToString());
            }

            if (combo1.SelectedItem != null && combo1.SelectedItem.ToString() != "All")
            {
                query = query.Where(x => x.Cours1.Courses_Grade == combo1.SelectedItem.ToString());
            }

            datagrid.ItemsSource = query.
                Select(x => new {
                x.Student_ID,
                x.Student_name,
                x.Student_adress,
                x.Student_age,
                x.Student_phone,
                x.Cours.Courses_name,
                x.Cours1.Courses_Grade,
                x.Cours.Courses_price
            }).ToList();
        }

        private void Modify_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ModifyPage());
        }
    }
}
