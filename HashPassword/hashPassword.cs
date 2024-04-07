using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HashPassword
{
    public class hashPassword
    {
        public string HashingPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] sourceBytePassw = Encoding.UTF8.GetBytes(password);
                byte[] hashSourceBytePassw = sha256Hash.ComputeHash(sourceBytePassw);
                string hashpassw = BitConverter.ToString(hashSourceBytePassw).Replace("-", String.Empty);
                return hashpassw;
            }
        }
    }
}
