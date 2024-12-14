using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Threading;
using WpfApp1.Models;
using WpfApp1.Services;

namespace WpfApp1.Pages
{
    public partial class Autho : Page
    {
        int click;
        int n;
        private DispatcherTimer timer;
        private int timeLeft;

        public Autho()
        {
            InitializeComponent();
            click = 0;
            n = 0;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick; // Каждую секунду будет вызываться метод
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timeLeft--;
            if (timeLeft > 0)
            {
                tbTimeLeft.Text = $"Подождите {timeLeft} секунд";
            }
            else
            {
                timer.Stop();
                tbTimeLeft.Visibility = Visibility.Hidden;
                tbLogin.IsEnabled = true;
                tbPassword.IsEnabled = true;
                btnEnterGuest.IsEnabled = true;
                tbCaptcha.IsEnabled = true;
                btnEnter.IsEnabled = true;
                n = 0;
            }
        }

        private void BtnEnter_Click(object sender, RoutedEventArgs e)
        {
            if (!IsWorkingHours())
            {
                MessageBox.Show("Доступ запрещен. Рабочее время: с 10:00 до 19:00.");
                return;
            }

            NavigationService.Navigate(new Client(null, null));
        }

        private void locked()
        {
            MessageBox.Show("Блокировка: Слишком много неудачных попыток входа.");

            tbLogin.IsEnabled = false;
            tbPassword.IsEnabled = false;
            btnEnter.IsEnabled = false;
            btnEnterGuest.IsEnabled = false;
            tbCaptcha.IsEnabled = false;
            tbTimeLeft.Visibility = Visibility.Visible;
            timeLeft = 10;
            tbTimeLeft.Text = $"Подождите {timeLeft} секунд";
            timer.Start();
        }

        private void GenerateCapctcha()
        {
            tbCaptcha.Visibility = Visibility.Visible;
            tblCaptcha.Visibility = Visibility.Visible;

            string capctchaText = CapthchaGenerator.GenerateCaptchaText(6);
            tblCaptcha.Text = capctchaText;
            tblCaptcha.TextDecorations = TextDecorations.Strikethrough;
        }

        private static Пр4_Агентсво_недвижимостиEntities db;

        private void btnEnterGuests_Click(object sender1, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Client(null, null));
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
           /* if (!IsWorkingHours())
            {
                MessageBox.Show("Доступ запрещен. Рабочее время: с 10:00 до 19:00.");
                return;
            }*/

            click += 1;
            string login = tbLogin.Text.Trim();
            string password = tbPassword.Text.Trim();
            HashPassword hash = new HashPassword();
            Helpel helpel = new Helpel();
            password = hash.HashPassword1(password);
            db = new Пр4_Агентсво_недвижимостиEntities();

            var user = db.Авторизация.Where(x => x.login == login && x.password == password).FirstOrDefault();

            if (click == 1)
            {
                if (user != null)
                {
                    MessageBox.Show("Вы вошли под: " + user.role.role1.ToString());
                    LoadPage(user.role.role1.ToString(), user);
                }
                else
                {
                    MessageBox.Show("Вы ввели логин или пароль неверно!");
                    GenerateCapctcha();
                    tbLogin.Text = " ";
                    tbPassword.Text = " ";
                    tbCaptcha.Text = " ";
                    n++;
                }
            }
            else if (click > 1)
            {
                if (user != null && tbCaptcha.Text.Trim() == tblCaptcha.Text.Trim())
                {
                    MessageBox.Show("Вы вошли под: " + user.role.role1.ToString());
                    LoadPage(user.role.role1.ToString(), user);
                }
                else
                {
                    if (n >= 3)
                    {
                        locked();
                    }
                    else
                    {
                        MessageBox.Show("Введите данные заново!");
                        GenerateCapctcha();
                        n++;
                        tbLogin.Text = " ";
                        tbPassword.Text = " ";
                        tbCaptcha.Text = " ";
                    }
                }
            }
        }

        private void LoadPage(string _role, Авторизация user)
        {
            click = 0;
            switch (_role)
            {
                case "Клиент":
                    NavigationService.Navigate(new Client(user, _role));
                    break;

                case "Сотрудник":
                    NavigationService.Navigate(new Sotrudnik(user, _role));
                    break;
            }
        }

        private void btnEnterGuest_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Вы вошли как гость: ");
            NavigationService.Navigate(new Client(null, null));
        }

        private bool IsWorkingHours()
        {
            DateTime now = DateTime.Now;



            DateTime startTime = new DateTime(now.Year, now.Month, now.Day, 10, 0, 0);
            DateTime endTime = new DateTime(now.Year, now.Month, now.Day, 19, 0, 0);

            return now >= startTime && now <= endTime;
        }
    }
}