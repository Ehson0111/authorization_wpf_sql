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
        HashPassword hash = new HashPassword();
        Helpel helpel = new Helpel();


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

            txtZarplata.Text = employee.Зарплата.ToString();

            dpBirthday.Text = employee.Дата_рождение;

            cbpol.SelectedValue = employee.id_pol;



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
            employee.Зарплата = Convert.ToDecimal(txtZarplata.Text);
            employee.id_dolzhnost = (int?)cbDolzhnost.SelectedValue;
            employee.id_pol = (int?)cbpol.SelectedValue;
            //employee.Авторизация.password=Tb
            var auth = db.Авторизация.Where(a => a.Id_Авторизация == employee.id_Авторизация).FirstOrDefault();
  
            auth.login = txtlogin.Text;

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //"   var employee = db.Сотрудник.Find(_employeeId);

            //   // Обновление данных сотрудника
            //   employee.Имя = txtFirstName.Text;
            //   employee.Фамилия = txtLastName.Text;
            //   employee.Отчество = txtMiddleName.Text;
            //   employee.Контактные_данные = txtContactDetails.Text;
            //   employee.id_dolzhnost = (int?)cbDolzhnost.SelectedValue;
            //   //employee.Авторизация.password=Tb
            //   var auth = db.Авторизация.Where(a => a.Id_Авторизация == employee.id_Авторизация).FirstOrDefault();

            //   var Cotrudnik = db.Сотрудник;

            //   auth.login = txtlogin.Text;
            //   HashPassword hash = new HashPassword();

            //   //  password = hash.HashPassword1(password);


            //   auth.password = hash.HashPassword1(pbPassword.Password);

            //   // Обновление данных авторизации
            //   // Сохранение изменений
            //   try
            //   {
            //       db.SaveChanges();
            //       MessageBox.Show("Данные успешно сохранены!");
            //       NavigationService.Navigate(new Sotrudnik(auth, null));
            //   }
            //   catch (Exception ex)
            //   {
            //       MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}");

            //   }
            ////   auth.password = hash.HashPassword1(pbPassword.Password);
            ///
            ///
            //employee.Имя = txtFirstName.Text;
            //employee.Фамилия = txtLastName.Text;
            //employee.Отчество = txtMiddleName.Text;
            //employee.Контактные_данные = txtContactDetails.Text;
            //employee.id_dolzhnost = (int?)cbDolzhnost.SelectedValue;
            ////employee.Авторизация.password=Tb
            //var auth = db.Авторизация.Where(a => a.Id_Авторизация == employee.id_Авторизация).FirstOrDefault();


            //auth.login = txtlogin.Text;

            ////  password = hash.HashPassword1(password);


            try
            {

                string parol = hash.HashPassword1(pbPassword.Password);
                string login1 = txtlogin.Text;
                Сотрудник newSotrudnik = new Сотрудник
                {
                    Фамилия = txtLastName.Text,
                    Имя = txtFirstName.Text,
                    Отчество = txtMiddleName.Text,
                    Контактные_данные = txtContactDetails.Text,
                    id_dolzhnost=(int?)cbDolzhnost.SelectedIndex,
                    Дата_рождение=dpBirthday.Text,
                   Зарплата= Convert.ToDecimal(txtZarplata.Text),
                   id_pol=(int)cbpol.SelectedIndex,



                    //Зарплата=,
                    // Дата_рождение=,
                    //Контактные_данные= txtContactDetails.Text,
                    //паспортные_данные,            employee.Зарплата = Convert.ToInt64(txtZarplata);

                    //id_pol

                };


                Авторизация auth = new Авторизация
                {
                    login = login1,
                    password = parol,
                    id_role = 2 ,
                  
                };
                helpel.CreateAuthorization(auth);

                var authId = helpel.GetLastAuthorizationId();
                newSotrudnik.id_Авторизация = authId;
                helpel.CreateSotrudnik(newSotrudnik);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}");

            }
        }
    }
}