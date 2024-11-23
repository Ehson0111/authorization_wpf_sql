using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    internal class HashPassword
    {
        public string HashPassword1(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] SourceSwitch = Encoding.UTF8.GetBytes(password);
                byte[] hashSourceBytePassw = sha256Hash.ComputeHash(SourceSwitch);
                string hashPassw = BitConverter.ToString(hashSourceBytePassw).Replace("-", string.Empty);
                return hashPassw;
            }
        }
    }
}
