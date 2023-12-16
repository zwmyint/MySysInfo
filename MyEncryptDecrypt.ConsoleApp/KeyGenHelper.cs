using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MyEncryptDecrypt.ConsoleApp
{
    public class KeyGenHelper
    {
        public static string GenerateKey(int keySize)
        {
            if (keySize != 128 && keySize != 192 && keySize != 256)
            {
                throw new ArgumentException("The key size must be 128, 192, or 256 bits.");
            }

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.KeySize = keySize;
                aesAlg.GenerateKey();
                return Convert.ToBase64String(aesAlg.Key);
            }
        }
    }
}
