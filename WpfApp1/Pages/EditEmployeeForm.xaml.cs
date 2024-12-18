using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Models;

namespace WpfApp1.Pages
{
    public partial class EditEmployeeForm : Page
    {
        private Пр4_Агентсво_недвижимостиEntities db;
        private int _employeeId;

        public EditEmployeeForm(int employeeId)
        {
            InitializeComponent();
            _employeeId = employeeId;
            db = new Пр4_Агентсво_недвижимостиEntities();

            // Загрузка данных сотрудника
            var employee = db.Сотрудник.Find(employeeId);
            if (employee == null)
            {
                MessageBox.Show("Сотрудник не найден!");
                return;
            }

            txt(employee);

            cbDolzhnost.ItemsSource = db.dolzhnost.ToList();
        }

        private void txt(Сотрудник employee)
        {
            txtFirstName.Text = employee.Имя;
            txtLastName.Text = employee.Фамилия;
            txtMiddleName.Text = employee.Отчество;
            txtContactDetails.Text = employee.Контактные_данные;
            cbDolzhnost.SelectedValue = employee.id_dolzhnost;

            // Загрузка данных авторизации
            var auth = db.Авторизация.Where(a => a.Id_Авторизация == employee.id_Авторизация).FirstOrDefault();
            if (auth != null)
            {
                txtlogin.Text = auth.login;
                pbPassword.Password = auth.password;
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            var employee = db.Сотрудник.Find(_employeeId);
            if (employee == null)
            {
                MessageBox.Show("Сотрудник не найден!");
                return;
            }

            // Обновление данных сотрудника
            employee.Имя = txtFirstName.Text;
            employee.Фамилия = txtLastName.Text;
            employee.Отчество = txtMiddleName.Text;
            employee.Контактные_данные = txtContactDetails.Text;
            employee.id_dolzhnost = (int?)cbDolzhnost.SelectedValue;
            //employee.Авторизация.password=Tb
            var auth = db.Авторизация.Where(a => a.Id_Авторизация == employee.id_Авторизация).FirstOrDefault();


            auth.login = txtlogin.Text;
            HashPassword hash = new HashPassword();

            //  password = hash.HashPassword1(password);


            auth.password = hash.HashPassword1(pbPassword.Password);

            // Обновление данных авторизации
            // Сохранение изменений
            try
            {
                db.SaveChanges();
                MessageBox.Show("Данные успешно сохранены!");
                NavigationService.Navigate(new Sotrudnik(auth, null));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}");
            }
        }

        private void cbDolzhnost_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CLEAR_Click(object sender, RoutedEventArgs e)
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtMiddleName.Text = "";
            txtContactDetails.Text = "";
            txtlogin.Text = "";
            pbPassword.Password = "";
        }
    }
}