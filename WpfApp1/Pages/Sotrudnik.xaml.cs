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
            var authorization = db.Авторизация.ToList();
            var sotrudnik = db.Сотрудник.ToList();

            //var userQuery = from a in authorization
            //                join c in sotrudnik on a.Id_Авторизация equals c.id_Авторизация
            //                where a.Id_Авторизация == user.Id_Авторизация
            //                select new { c.Имя, c.Фамилия, c.Отчество, c.Контактные_данные };
            ////var userInfo = userQuery.First();
            //var user1 = user.Сотрудник.First();

            //time(user1);
            
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

        private void time(Сотрудник user)
        {

            DateTime currentTime = DateTime.Now;
            string text = "";

            string s = "";
            if (currentTime.Hour >= 10 && currentTime.Hour <= 12)
            {
                s = "утро";
                text = $"Доброе {s} !, {user.Имя} {user.Фамилия} ";
            }
            else if (currentTime.Hour >= 12 && currentTime.Hour <= 17)
            {
                s = "день";
                text = $"Добрый {s} !, {user.Имя} {user.Фамилия} ";
            }
            else if (currentTime.Hour >= 17 && currentTime.Hour <= 26)
            {
                s = "вечер ";
                text = $"Добрый {s} !, {user.Имя} {user.Фамилия} ";
            }

          // Text1.Content = text;
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