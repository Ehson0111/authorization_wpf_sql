using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using WpfApp1.Models;
using static System.Net.Mime.MediaTypeNames;

namespace WpfApp1.Pages
{
    /// <summary>
    /// Логика взаимодействия для Client.xaml
    /// </summary>
    public partial class Client : Page
    {
        private static Пр4_Агентсво_недвижимостиEntities db;
        private ListBox listBox;
        public Client(Авторизация user, string role)
        {
            InitializeComponent();
            db = new Пр4_Агентсво_недвижимостиEntities();

            var authorization = db.Авторизация.ToList();
            var client = db.Клиент.ToList();
            var user1=user.Клиент.First();
            var quer = from a in authorization
                       join c in client on a.Id_Авторизация equals c.id_Авторизация
                       where a.Id_Авторизация == user.Id_Авторизация
                       select new { c.Имя, c.Фамилия };

            ////var result = quer.First(); // Используем FirstOrDefault()
            //if (result != null)
            //{
            //    time(result); // Передаем результат в метод time, только если он не null
            //}
            //else
            //{
            //    // Обработка случая, когда результат пуст
            
            //}


            var items = db.Недвижимость.ToList();

            LViewProduct.ItemsSource = items; // Заполняем данные


            string searchText = txtSearch.Text.ToLower();
            var filteredItems = items.Where(x => x.Адресс.ToLower().Contains(searchText)).ToList();

            LViewProduct.ItemsSource = filteredItems;

            // Количество отображаемых элементов
            int displayedCount = filteredItems.Count;

            // Общее количество элементов
            int totalCount = items.Count;

            // Обновляем текст Label
            lblCount.Content = $"{displayedCount} из {totalCount}";
            time(user1);
        }


        private void time(Клиент user)
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
            else if (currentTime.Hour >= 17 && currentTime.Hour <= 19)
            {
                s = "вечер ";
                text = $"Добрый {s} !, {user.Имя} {user.Фамилия} ";
            }

            text1.Content = text;
        }

        private void cmbSorting_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            db = new Пр4_Агентсво_недвижимостиEntities();

            var items = db.Недвижимость.ToList();
            var display = db.Недвижимость.ToList();

            if (LViewProduct == null)
            {
                return;
            }
            switch (cmbSorting.SelectedIndex)
            {
                case 0:
                  display = items;
                    break;
                case 1:
                  display= items.OrderBy(x => x.Стоимость).ToList();
                    break;
                case 2:
                    display = items.OrderByDescending(x => x.Стоимость).ToList();
                    break;

            }
            LViewProduct.ItemsSource = display;


            lblCount.Content = $"{display.Count()} из {items.Count()}";
            //   LViewProduct.Items.Refresh();

            // Пример с учетом поиска

        }

        private void LViewProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cmbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            db = new Пр4_Агентсво_недвижимостиEntities();

            var items = db.Недвижимость.ToList();
            var display = db.Недвижимость.ToList();

            if (LViewProduct == null)
            {
                return;
            }


            switch (cmbFilter.SelectedIndex)
            {
                case 0:
                    display = items;
                    break;
                case 1:
                    display = items.Where(x => double.TryParse(x.Общая_площадь, out double s) && s < 50).ToList();

                    break;
                case 2:
                    display = items.Where(x => double.TryParse(x.Общая_площадь, out double s) && s > 50).ToList();
                    break;

            }
            LViewProduct.ItemsSource = display;


            lblCount.Content = $"{display.Count()} из {items.Count()}";

        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            db = new Пр4_Агентсво_недвижимостиEntities();

            var items = db.Недвижимость.ToList();

            string searchText = txtSearch.Text.ToLower();
            var filteredItems = items.Where(x => x.Адресс.ToLower().Contains(searchText)).ToList();

            LViewProduct.ItemsSource = filteredItems;

            lblCount.Content = $"{filteredItems.Count()} из {items.Count()}";
        }
    }
}