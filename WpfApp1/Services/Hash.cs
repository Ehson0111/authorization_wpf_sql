using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Services
{
    internal class Hash
    {
         
            /// <summary>
            /// Хэширует заданный пароль с использованием алгоритма SHA256.
            /// </summary>
            /// <param name="password">Пароль, который необходимо хэшировать.</param>
            /// <returns>Строковое представление хэша пароля в шестнадцатеричном формате.</returns>
            public static string HashPassword(string password)
            {
             
                using (SHA256 shs256Hash = SHA256.Create())
                {
                    byte[] sourceBytePassword = Encoding.UTF8.GetBytes(password);//password принимается методом в виде аргумента
                    byte[] hash = shs256Hash.ComputeHash(sourceBytePassword);
                    return BitConverter.ToString(hash).Replace("-", String.Empty); //Возвращаем методом строковое значение
                }
            }
        


    }
}
