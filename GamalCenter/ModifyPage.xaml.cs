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
    /// Interaction logic for ModifyPage.xaml
    /// </summary>
    public partial class ModifyPage : Page
    {
        GamalCenterEntities db = new GamalCenterEntities();
        public ModifyPage()
        {
            InitializeComponent();
            SetData();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(IDtxt.Text))
            {
                MessageBox.Show("Please enter the Student ID.");
                return;
            }

            if (string.IsNullOrWhiteSpace(phonetxt.Text))
            {
                MessageBox.Show("Please enter the phone number.");
                return;
            }

            int id = int.Parse(IDtxt.Text);
           

            var studentToUpdate = db.students.FirstOrDefault(x => x.Student_ID == id);
            if (studentToUpdate == null)
            {
                MessageBox.Show("Student not found.");
                return;
            }

            int courseId = db.Courses
                .Where(x => x.Courses_name == combo.SelectedItem.ToString())
                .Select(x => x.Courses_ID)
                .FirstOrDefault();

            int courseGradeId = db.Courses
                .Where(x => x.Courses_Grade == combo1.SelectedItem.ToString())
                .Select(x => x.Courses_ID)
                .FirstOrDefault();

            studentToUpdate.Student_phone = phonetxt.Text;
            studentToUpdate.Courses_name = courseId;
            studentToUpdate.Courses_Grade = courseGradeId;
            db.students.AddOrUpdate(studentToUpdate);
            db.SaveChanges();
            MessageBox.Show("Data updated successfully.");

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

            combo.ItemsSource = courseNames;

            var courseGrade = db.Courses
               .Select(x => x.Courses_Grade)
               .Distinct().ToList();


            combo1.ItemsSource = courseGrade;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(IDtxt.Text))
            {
                MessageBox.Show("Please enter the Student ID.");
                return;
            }

            int deleteId = int.Parse(IDtxt.Text);

            var studentToDelete = db.students.FirstOrDefault(x => x.Student_ID == deleteId);
            if (studentToDelete == null)
            {
                MessageBox.Show("Student ID not found.");
                return;
            }

            db.students.Remove(studentToDelete);
            
            db.SaveChanges();
            MessageBox.Show("Record deleted successfully.");

            SetData();
        }

        private void CodifyCourse_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ModifyCourse());
        }
    }
}
