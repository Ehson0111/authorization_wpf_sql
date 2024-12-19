using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp1.Models;

namespace WpfApp1.Pages
{
    public partial class Sotrudnik : Page
    {
        private static Пр4_Агентсво_недвижимостиEntities db;

        public Sotrudnik(Авторизация user, string role)
        {
            InitializeComponent();
            db = new Пр4_Агентсво_недвижимостиEntities();

            // Приветствие
            var authorization = db.Авторизация;
            var sotrudnik = db.Сотрудник;

            var userQuery = from a in authorization
                            join c in sotrudnik on a.Id_Авторизация equals c.id_Авторизация
                            where a.Id_Авторизация == user.Id_Авторизация
                            select new { c.Имя, c.Фамилия, c.Отчество, c.Контактные_данные };
   //         var userInfo = userQuery.First();
     //       Text1.Content = GenerateGreeting(userInfo.Имя, userInfo.Фамилия);
            //if (userQuery == null)
            //{
            //    if (userQuery.Any())
            //    {
            //        var userInfo = userQuery.First();
            //        Text1.Content = GenerateGreeting(userInfo.Имя, userInfo.Фамилия);
            //    }

            //}

            // Получение сотрудников для DataGrid
            var employees = db.Сотрудник.Select(c => new
            {
                c.Id_Сотрудник, // Добавляем ID для идентификации
                c.Имя,
                c.Фамилия,
                c.Отчество,
                c.Контактные_данные,
                nazvanie = c.dolzhnost.nazvanie // Добавляем должность

            }).ToList();
            employeesDataGrid.ItemsSource = employees;

        }

        private string GenerateGreeting(string firstName, string lastName)
        {
            DateTime currentTime = DateTime.Now;
            string timeOfDay;

            if (currentTime.Hour >= 10 && currentTime.Hour < 12)
                timeOfDay = "утро";
            else if (currentTime.Hour >= 12 && currentTime.Hour < 17)
                timeOfDay = "день";
            else if (currentTime.Hour >= 17 && currentTime.Hour < 19)
                timeOfDay = "вечер";
            else
                timeOfDay = "ночь";

            return $"Доброе {timeOfDay}!, {firstName} {lastName}";
        }

        private void OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (employeesDataGrid.SelectedItem != null)
            {
                var selectedEmployee = employeesDataGrid.SelectedItem as dynamic;

                if (selectedEmployee != null)
                {
                    // Передаем только идентификатор сотрудника
                    int employeeId = selectedEmployee.Id_Сотрудник;

                    // Переходим на страницу редактирования
                    NavigationService.Navigate(new EditEmployeeForm(employeeId));
                }
            }

        }

        private void UpdateEmployeesDataGrid()
        {
            var updatedEmployees = db.Сотрудник.Select(c => new
            {
                c.Id_Сотрудник,
                c.Имя,
                c.Фамилия,
                c.Отчество,
                c.Контактные_данные,
                nazvanie = c.dolzhnost.nazvanie // Добавляем должность
            }).ToList();
            employeesDataGrid.ItemsSource = updatedEmployees;
        }
    }
}