// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");


using MyEncryptDecrypt.ConsoleApp;
using System.Security.Cryptography;
using System.Text;

// -- 1
CryptoHelper crypto = new CryptoHelper();

int keySize = 128; // change between 128, 192 and 256
string generatedKey = KeyGenHelper.GenerateKey(keySize);
Console.WriteLine($"Generated Key ({keySize} bits): {generatedKey}");

string originalText1 = "Text to be encrypted here...";
string encryptedText1 = crypto.Encrypt(originalText1, generatedKey);
string decryptedText1 = crypto.Decrypt(encryptedText1, generatedKey);

Console.WriteLine($"Original text: {originalText1}");
Console.WriteLine($"Encrypted text: {encryptedText1}");
Console.WriteLine($"Decrypted text: {decryptedText1}");


// -- 2 
string originalText = "Hello, World!";
string key = generatedKey; //

string encryptedText = Encrypt(originalText, key);
Console.WriteLine($"Encrypted Text: {encryptedText}");

string decryptedText = Decrypt(encryptedText, key);
Console.WriteLine($"Decrypted Text: {decryptedText}");


static string Encrypt(string plainText, string key)
{
    using (Aes aesAlg = Aes.Create())
    {
        aesAlg.Key = Encoding.UTF8.GetBytes(key);
        aesAlg.GenerateIV();

        ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

        using (MemoryStream msEncrypt = new MemoryStream())
        {
            using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            {
                using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                {
                    swEncrypt.Write(plainText);
                }
            }
            return Convert.ToBase64String(aesAlg.IV) + Convert.ToBase64String(msEncrypt.ToArray());
        }
    }
}

static string Decrypt(string cipherText, string key)
{
    using (Aes aesAlg = Aes.Create())
    {
        byte[] iv = Convert.FromBase64String(cipherText.Substring(0, 24));
        byte[] cipherBytes = Convert.FromBase64String(cipherText.Substring(24));
        aesAlg.Key = Encoding.UTF8.GetBytes(key);
        aesAlg.IV = iv;

        ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

        using (MemoryStream msDecrypt = new MemoryStream(cipherBytes))
        {
            using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
            {
                using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                {
                    return srDecrypt.ReadToEnd();
                }
            }
        }
    }
}