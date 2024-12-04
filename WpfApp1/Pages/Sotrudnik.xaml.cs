using System;
using System.Collections.Generic;
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
using WpfApp1.Models;

namespace WpfApp1.Pages
{
    /// <summary>
    /// Логика взаимодействия для Sotrudnik.xaml
    /// </summary>
    public partial class Sotrudnik : Page
    {
        private static Пр4_Агентсво_недвижимостиEntities db;

        public Sotrudnik(Авторизация user, string role)
        {
            InitializeComponent();
            db = new Пр4_Агентсво_недвижимостиEntities();
            var authorization = db.Авторизация;
            var sotrudnik = db.Сотрудник;

            var quer = from a in authorization
                       join c in sotrudnik on a.Id_Авторизация equals c.id_Авторизация
                       where a.Id_Авторизация == user.Id_Авторизация
                       select new { c.Имя, c.Фамилия };

            time(quer.First());
        }
        private void time(dynamic query)
        {

            //Table<Authorization> авторизации = db.GetTable<Authorization>();
            //Table<Client> клиенты = db.GetTable<Client>();

            //var query = from a in авторизации
            //            join c in клиенты on a.Id equals c.IdAuthorization
            //            where a.Id == 18
            //
            //      select new { a.Имя, a.Фамилия, a.Id };



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
    }
}
