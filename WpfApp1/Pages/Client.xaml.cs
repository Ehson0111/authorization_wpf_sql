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

            try
            {
                var authorization = db.Авторизация;
                var client = db.Клиент;

                var quer = from a in authorization
                           join c in client on a.Id_Авторизация equals c.id_Авторизация
                           where a.Id_Авторизация == user.Id_Авторизация
                           select new { c.Имя, c.Фамилия };
                time(quer.First());
            }
            catch (Exception)
            {
                Text1.Content = "";

            }

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
        }

        private void time(dynamic query)
        {

            DateTime currentTime = DateTime.Now;
            string text = " ";

            string s = "";
            if (currentTime.Hour >= 10 && currentTime.Hour <= 12)
            {
                s = "утро";
                text = $"Доброе {s} !, {query.Имя} {query.Фамилия} ";
            }
            else if (currentTime.Hour >= 12 && currentTime.Hour <= 17)
            {
                s = "день";
                text = $"Добрый {s} !, {query.Имя} {query.Фамилия} ";
            }
            else if (currentTime.Hour >= 17 && currentTime.Hour <= 19)
            {
                s = "вечер ";
                text = $"Добрый {s} !, {query.Имя} {query.Фамилия} ";
            }

            Text1.Content = text;
        }

        private void cmbSorting_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            db = new Пр4_Агентсво_недвижимостиEntities();

            var items = db.Недвижимость.ToList();
 
            if (LViewProduct==null)
            {
                return;
            }
            switch (cmbSorting.SelectedIndex)
            {
                case 0:
                    LViewProduct.ItemsSource = items;
                    break;
                case 1:
                    LViewProduct.ItemsSource = items.OrderBy(x => x.Стоимость).ToList();
                    break;
                case 2:
                    LViewProduct.ItemsSource = items.OrderByDescending(x => x.Стоимость).ToList();
                    break;
               
            }
            //   LViewProduct.Items.Refresh();

            // Пример с учетом поиска
        
        }

        private void LViewProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}