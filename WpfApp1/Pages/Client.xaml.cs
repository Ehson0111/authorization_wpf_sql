using System;
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

        public Client(Авторизация user,string role)
        {
            InitializeComponent();
            db = new Пр4_Агентсво_недвижимостиEntities();



            var authorization = db.Авторизация;
            var client = db.Клиент;

            var quer = from a in authorization
                       join c in client on a.Id_Авторизация equals c.id_Авторизация
                       where a.Id_Авторизация == user.Id_Авторизация
                       select new {c.Имя,c.Фамилия };

            time(quer.First());
        }

        private void time(dynamic query) {

            //Table<Authorization> авторизации = db.GetTable<Authorization>();
            //Table<Client> клиенты = db.GetTable<Client>();

            //var query = from a in авторизации
            //            join c in клиенты on a.Id equals c.IdAuthorization
            //            where a.Id == 18
            //
            //      select new { a.Имя, a.Фамилия, a.Id };
             


            DateTime currentTime = DateTime.Now;

            string s="";
            if (currentTime.Hour>=10 && currentTime.Hour<=12   )
            {
                s = "Утро";
            }
            else if(currentTime.Hour >= 12 && currentTime.Hour <= 17)
            {
                s = "День";

            }
            else if(currentTime.Hour >= 17 && currentTime.Hour <= 19)
            {
                s = "Вечер ";

            }

            string text=$"Доброе {s} !, {query.Имя} {query.Фамилия} ";
            Text1.Content=text;

        }

    }
}
