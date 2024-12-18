﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using WpfApp1.Models;

namespace WpfApp1.Pages
{
    /// <summary>
    /// Логика взаимодействия для Client.xaml
    /// </summary>
    public partial class Client : Page
    {
        private static Пр4_Агентсво_недвижимостиEntities db;

        public Client(Авторизация user, string role)
        {
            InitializeComponent();
            db = new Пр4_Агентсво_недвижимостиEntities();
            var authorization = db.Авторизация;
            var client = db.Клиент;

            var quer = from a in authorization
                       join c in client on a.Id_Авторизация equals c.id_Авторизация
                       where a.Id_Авторизация == user.Id_Авторизация
                       select new { c.Имя, c.Фамилия };

            time(quer.First());
            var client1=db.Клиент.ToList();
            LViewProduct.ItemsSource = client1; // Заполняем данные

        }
        public class YourDataModel
        {
            public string Фамилия { get; set; }
            public string Имя { get; set; }
            public string Отчество { get; set; }
        }
     

            private List<YourDataModel> GetData()
            {
                // Возвращаем коллекцию данных
                return new List<YourDataModel>
        {
            new YourDataModel { Фамилия = "Иванов", Имя = "Иван", Отчество = "Иванович" },
            new YourDataModel { Фамилия = "Петров", Имя = "Петр", Отчество = "Петрович" }
        };
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

            // Text1.Content = text;
        }
    }
}