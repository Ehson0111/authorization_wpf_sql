using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;

namespace WpfApp1
{
    internal class Helpel
    {
        private static Пр4_Агентсво_недвижимостиEntities _context; // Статическая приватная переменная для хранения контекста модели данных

        /// <summary>
        /// Метод для получения контекста данных, необходимого для подключения к базе данных.
        /// </summary>
        /// <returns>Контекст данных типа demonEntities.</returns>
        public static Пр4_Агентсво_недвижимостиEntities GetContext()
        {
            // Проверка, установлено ли подключение; если нет, создается новое подключение
            if (_context == null)
            {
                _context = new Пр4_Агентсво_недвижимостиEntities(); // Создание нового подключения к БД
            }
            return _context; // Возвращение текущего подключения
        }

        /// <summary>
        /// Метод для добавления новой записи о пользователе в таблицу Users базы данных.
        /// </summary>
        /// <param name="user">Объект типа Users, содержащий информацию о новом пользователе.</param>
        public void CreateUser(Клиент user)
        {
            // Убедитесь, что контекст инициализирован
            if (_context == null)
            {
                _context = GetContext();
            }

            _context.Клиент.Add(user); // Добавление записи нового пользователя в таблицу Users
            _context.SaveChanges(); // Сохранение изменений в БД
        }

        /// <summary>
        /// Метод для обновления записи о пользователе в таблице Users базы данных.
        /// </summary>
        /// <param name="user">Объект типа Users, представляющий измененную сущность пользователя.</param>
        public void UpdateUser(Клиент user)
        {
            // Убедитесь, что контекст инициализирован
            if (_context == null)
            {
                _context = GetContext();
            }

            // Состояние сущности помечается как Измененная
            _context.Entry(user).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges(); // Сохранение измененной сущности в БД
        }

        public void UpdateSotrudnik(Сотрудник сотрудник)
        {
            // Убедитесь, что контекст инициализирован
            if (_context == null)
            {
                _context = GetContext();
            }

            // Состояние сущности помечается как Измененная
            _context.Entry(сотрудник).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges(); // Сохранение измененной сущности в БД
        }

        /// <summary>
        /// Метод для удаления записи о пользователе из таблицы Users базы данных.
        /// </summary>
        /// <param name="idUser">Целое число, представляющее идентификатор 
        /// пользователя, который необходимо удалить.</param>
        public void RemoveUser(int idUser)
        {
            // Убедитесь, что контекст инициализирован
            if (_context == null)
            {
                _context = GetContext();
            }

            var users = _context.Клиент.Find(idUser); // Поиск записи пользователя по его id
            _context.Клиент.Remove(users); // Удаление записи найденного пользователя
            _context.SaveChanges(); // Сохранение изменений в БД
        }

        /// <summary>
        /// Метод для фильтрации записей пользователей по имени.
        /// </summary>
        /// <returns>Список пользователей, чьи имена начинаются с буквы 'M' или 'A'.</returns>
        public List<Клиент> FiltrUsers()
        {
            // Убедитесь, что контекст инициализирован
            if (_context == null)
            {
                _context = GetContext();
            }

            return _context.Клиент.Where(x => x.Имя.StartsWith("M") || x.Имя.StartsWith("A")).ToList();
        }

        /// <summary>
        /// Метод для сортировки записей пользователей по имени.
        /// </summary>
        /// <returns>Список пользователей, отсортированных по именам в порядке возрастания.</returns>
        public List<Клиент> SortUsers()
        {
            // Убедитесь, что контекст инициализирован
            if (_context == null)
            {
                _context = GetContext();
            }

            return _context.Клиент.OrderBy(x => x.Имя).ToList();
        }

        /// <summary>
        /// Получает тип пользователя из связанной таблицы Types.
        /// </summary>
        /// <param name="user">Объект пользователя, содержащий информацию о типе.</param>
        /// <returns>Название типа пользователя.</returns>
    }
}
