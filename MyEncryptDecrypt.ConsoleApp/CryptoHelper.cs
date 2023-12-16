using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MyEncryptDecrypt.ConsoleApp
{
    public class CryptoHelper
    {
        public string Encrypt(string plainText, string key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.GenerateIV();

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }

                    byte[] iv = aesAlg.IV;
                    byte[] encryptedData = msEncrypt.ToArray();
                    byte[] combinedData = new byte[iv.Length + encryptedData.Length];
                    Buffer.BlockCopy(iv, 0, combinedData, 0, iv.Length);
                    Buffer.BlockCopy(encryptedData, 0, combinedData, iv.Length, encryptedData.Length);

                    return Convert.ToBase64String(combinedData);
                }

            }
        }

        public string Decrypt(string encryptedText, string key)
        {
            byte[] combinedData = Convert.FromBase64String(encryptedText);

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                byte[] iv = new byte[aesAlg.BlockSize / 8];
                byte[] encryptedData = new byte[combinedData.Length - iv.Length];

                Buffer.BlockCopy(combinedData, 0, iv, 0, iv.Length);
                Buffer.BlockCopy(combinedData, iv.Length, encryptedData, 0, encryptedData.Length);

                aesAlg.IV = iv;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(encryptedData))
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                {
                    return srDecrypt.ReadToEnd();
                }
            }
        }
    }
}
