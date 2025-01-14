//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Windows.Controls;
//using WpfApp1.Models;
//using static System.Net.Mime.MediaTypeNames;

//namespace WpfApp1.Pages
//{
//    public partial class Client : Page
//    {
//        private static Пр4_Агентсво_недвижимостиEntities db;
//        private ListBox listBox;
//        public Client(Авторизация user, string role)
//        {
//            InitializeComponent();
//            db = new Пр4_Агентсво_недвижимостиEntities();

//            var authorization = db.Авторизация.ToList();
//            //var client = db.Клиент.ToList();
//            //var user1 = user.Клиент.First();
//            //var quer = from a in authorization
//            //           join c in client on a.Id_Авторизация equals c.id_Авторизация
//            //           where a.Id_Авторизация == user.Id_Авторизация
//            //           select new { c.Имя, c.Фамилия };

//            ////var result = quer.First(); // Используем FirstOrDefault()
//            //}


//            var items = db.Недвижимость.ToList();

//            LViewProduct.ItemsSource = items; // Заполняем данные


//            string searchText = txtSearch.Text.ToLower();
//            var filteredItems = items.Where(x => x.Адресс.ToLower().Contains(searchText)).ToList();

//            LViewProduct.ItemsSource = filteredItems;

//            // Количество отображаемых элементов
//            int displayedCount = filteredItems.Count;

//            // Общее количество элементов
//            int totalCount = items.Count;

//            // Обновляем текст Label
//            lblCount.Content = $"{displayedCount} из {totalCount}";
//            time(user.Клиент.First());
//        }


//        private void time(Клиент user)
//        {

//            DateTime currentTime = DateTime.Now;
//            string text = "";

//            string s = "";
//            if (currentTime.Hour >= 10 && currentTime.Hour <= 12)
//            {
//                s = "утро";
//                text = $"Доброе {s} !, {user.Имя} {user.Фамилия} ";
//            }
//            else if (currentTime.Hour >= 12 && currentTime.Hour <= 17)
//            {
//                s = "день";
//                text = $"Добрый {s} !, {user.Имя} {user.Фамилия} ";
//            }
//            else if (currentTime.Hour >= 17 && currentTime.Hour <= 19)
//            {
//                s = "вечер ";
//                text = $"Добрый {s} !, {user.Имя} {user.Фамилия} ";
//            }

//            text1.Content = text;
//        }

//        private void cmbSorting_SelectionChanged(object sender, SelectionChangedEventArgs e)
//        {
//            db = new Пр4_Агентсво_недвижимостиEntities();

//            var items = db.Недвижимость.ToList();
//            var display = db.Недвижимость.ToList();

//            if (LViewProduct == null)
//            {
//                return;
//            }
//            switch (cmbSorting.SelectedIndex)
//            {
//                case 0:
//                    display = items;
//                    break;
//                case 1:
//                    display = items.OrderBy(x => x.Стоимость).ToList();
//                    break;
//                case 2:
//                    display = items.OrderByDescending(x => x.Стоимость).ToList();
//                    break;

//            }
//            LViewProduct.ItemsSource = display;


//            lblCount.Content = $"{display.Count()} из {items.Count()}";
//            //   LViewProduct.Items.Refresh();

//            // Пример с учетом поиска

//        }

//        private void LViewProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
//        {

//        }

//        private void cmbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
//        {
//            db = new Пр4_Агентсво_недвижимостиEntities();

//            var items = db.Недвижимость.ToList();
//            var display = db.Недвижимость.ToList();

//            if (LViewProduct == null)
//            {
//                return;
//            }


//            switch (cmbFilter.SelectedIndex)
//            {
//                case 0:
//                    display = items;
//                    break;
//                case 1:
//                    display = items.Where(x => double.TryParse(x.Общая_площадь, out double s) && s < 50).ToList();

//                    break;
//                case 2:
//                    display = items.Where(x => double.TryParse(x.Общая_площадь, out double s) && s > 50).ToList();
//                    break;

//            }
//            LViewProduct.ItemsSource = display;


//            lblCount.Content = $"{display.Count()} из {items.Count()}";

//        }

//        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
//        {
//            db = new Пр4_Агентсво_недвижимостиEntities();

//            var items = db.Недвижимость.ToList();

//            string searchText = txtSearch.Text.ToLower();
//            var filteredItems = items.Where(x => x.Адресс.ToLower().Contains(searchText)).ToList();

//            LViewProduct.ItemsSource = filteredItems;

//            lblCount.Content = $"{filteredItems.Count()} из {items.Count()}";
//        }
//    }
//}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using WpfApp1.Models;

namespace WpfApp1.Pages
{
    public partial class Client : Page
    {
        private static Пр4_Агентсво_недвижимостиEntities db;

        public Client(Авторизация user, string role)
        {
            InitializeComponent();
            db = new Пр4_Агентсво_недвижимостиEntities();

            LoadData();

            time(user.Клиент.First());
        }

        private void LoadData()
        {
            db = new Пр4_Агентсво_недвижимостиEntities();
            // Загружаем все данные
            var items = db.Недвижимость.ToList();
            ApplyFiltersAndSorting(items);
        }
         
        private void ApplyFiltersAndSorting(List<Недвижимость> items)
        {
            // Поиск
            string searchText = txtSearch.Text.ToLower();
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                  items = items
               .Where(x =>
                    x.Адресс.ToLower().Contains(searchText.ToLower()) || // Filter by address
                    x.tip_zhilya.tip_zhilya1.ToString().ToLower().Contains(searchText.ToLower())  // Filter by housing type
                    )  
                   .ToList();
            }

            // Сортировка
            //switch (cmbSorting.SelectedIndex)
            //{
            //    case 1:
            //        items = items.OrderBy(x => x.Стоимость).ToList();
            //        break;
            //    case 2:
            //        items = items.OrderByDescending(x => x.Стоимость).ToList();
            //        break;
            //}

            if (LViewProduct == null)
            {
                return;
            }
            switch (cmbSorting.SelectedIndex)
            {
                case 0:
                    items = items;
                    break;
                case 1:
                    items = items.OrderBy(x => x.Стоимость).ToList();
                    break;
                case 2:
                    items = items.OrderByDescending(x => x.Стоимость).ToList();
                    break;

            }
            // Фильтрация
            switch (cmbFilter.SelectedIndex)
            {
                case 1:
                    items = items.Where(x => double.TryParse(x.Общая_площадь, out double s) && s < 50).ToList();
                    break;
                case 2:
                    items = items.Where(x => double.TryParse(x.Общая_площадь, out double s) && s >= 50).ToList();
                    break;
            }

            // Обновляем список и счетчики
            LViewProduct.ItemsSource = items;
            lblCount.Content = $"{items.Count} из {db.Недвижимость.Count()}";
        }

        private void time(Клиент user)
        {
            DateTime currentTime = DateTime.Now;
            string greeting;

            if (currentTime.Hour >= 10 && currentTime.Hour < 12)
            {
                greeting = "утро";
            }
            else if (currentTime.Hour >= 12 && currentTime.Hour < 17)
            {
                greeting = "день";
            }
            else if (currentTime.Hour >= 17 && currentTime.Hour < 19)
            {
                greeting = "вечер";
            }
            else
            {
                greeting = "время";
            }

            text1.Content = $"Доброе {greeting}!, {user.Имя} {user.Фамилия}";
        }

        private void cmbSorting_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadData();
        }

        private void cmbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadData();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadData();
        }

        private void LViewProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Обработка выбора элемента
        }
    }
}