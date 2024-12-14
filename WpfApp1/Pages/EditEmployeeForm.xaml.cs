using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
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
using WpfApp1.Models;

namespace WpfApp1.Pages
{
    /// <summary>
    /// Логика взаимодействия для EditEmployeeForm.xaml
    /// </summary>
    public partial class EditEmployeeForm : Page
    {
           private Пр4_Агентсво_недвижимостиEntities db;
           private  Сотрудник _employee;

        Helpel help=new Helpel();
        private int _employeeId;

        public EditEmployeeForm(int employeeId)
        {
            InitializeComponent();
            ///_employee = employee;
            _employeeId = employeeId;

            db = new Пр4_Агентсво_недвижимостиEntities();



            var employee= db.Сотрудник.Find(employeeId);
            txtFirstName.Text = employee.Имя;
            txtLastName.Text = employee.Фамилия;
            txtMiddleName.Text = employee.Отчество;
            txtContactDetails.Text = employee.Контактные_данные;
            
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            var employee = db.Сотрудник.Find(_employeeId);
            var auth = db.Авторизация;
            //var userQuery = from a in authorization
            //                join c in sotrudnik on a.Id_Авторизация equals c.id_Авторизация
            //                where a.Id_Авторизация == user.Id_Авторизация
            //                select new { c.Имя, c.Фамилия, c.Отчество, c.Контактные_данные };
           
            var quer = from a in auth
                       where a.Id_Авторизация == _employeeId
                       select a;

            var result = quer.FirstOrDefault();
            if (employee == null)
            {
                MessageBox.Show("Сотрудник не найден!");
                return;
            }
            employee.Имя = txtFirstName.Text;
            employee.Фамилия = txtLastName.Text;
            employee.Отчество = txtMiddleName.Text;
            employee.Контактные_данные = txtContactDetails.Text;

            db.SaveChanges();


            NavigationService.Navigate(new Sotrudnik(result, null));
            // Сохранение изменений в базу данных
        }
    }
}
