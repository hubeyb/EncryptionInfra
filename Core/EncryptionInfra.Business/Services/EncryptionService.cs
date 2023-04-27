using EncryptionInfra.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EncryptionInfra.Business.Services
{
    public class EncryptionService
    {
        private const string securityKey = "o92eosdfn3414719mksolw02!i@#Ulka";

        private static byte[] secretKey
        {
            get
            {
                return Encoding.UTF8.GetBytes(securityKey);
            }
        }

        private static byte[] IV
        {
            get
            {
                byte[] IV = { 57, 167, 78, 223, 28, 34, 19, 76, 78, 26, 28, 67, 78, 18, 28, 57 };
                return IV;
            }
        }

        public static string Encrypt(string plainText)
        {
            using (Aes aesAlgorithm = Aes.Create())
            {
                aesAlgorithm.KeySize = 256;
                aesAlgorithm.Key = secretKey;
                aesAlgorithm.IV = IV;

                ICryptoTransform encryptor = aesAlgorithm.CreateEncryptor();

                byte[] encryptedData;

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(plainText);
                        }
                        encryptedData = ms.ToArray();
                    }
                }

                return Convert.ToBase64String(encryptedData);
            }
        }

        public static string Decrypt(string encryptedText)
        {
            using (Aes aesAlgorithm = Aes.Create())
            {
                aesAlgorithm.KeySize = 256;
                aesAlgorithm.Key = secretKey;
                aesAlgorithm.IV = IV;

                ICryptoTransform decryptor = aesAlgorithm.CreateDecryptor();

                byte[] cipherText = Convert.FromBase64String(encryptedText);

                using (MemoryStream ms = new MemoryStream(cipherText))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(cs))
                        {
                            return sr.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}

