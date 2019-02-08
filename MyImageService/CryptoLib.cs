using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MIData.Models
{
    class CryptoLib
    {
        private static Rijndael RijndaelAlg = Rijndael.Create();
        private static byte[] Key;
        private static byte[] IV;

        private static void GetKey()
        {
            Key = RijndaelAlg.Key;
            IV = RijndaelAlg.IV;
        }

        // Encypted method
        public static string EncryptString(string plainText)
        {
            GetKey();
            byte[] encrypted;

            // Create an RijndaelManaged object
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                // Create an encryptor to perform the stream transform
                ICryptoTransform encryptor = rijAlg.CreateEncryptor(Key, IV);

                // Create the streams used for encryption
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            // Write all data to the stream
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            // Return the encrypted string from the memory stream
            return Convert.ToBase64String(encrypted);
        }

        // Decrypted method
        public static string DecryptString(string sCipher)
        {
            GetKey();
            byte[] cipherText = Convert.FromBase64String(sCipher);

            //Declare the string used to hold the decrypted text
            string plainText = null;

            // Create an RijndaelManaged Object
            using(RijndaelManaged rijAlg = new RijndaelManaged())
            {
                // Create a decryptor to perform the stream transform
                ICryptoTransform decryptor = rijAlg.CreateDecryptor(Key, IV);

                // Create the streams used for decryption
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using(CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            plainText = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            return plainText;
        }
    }
}
