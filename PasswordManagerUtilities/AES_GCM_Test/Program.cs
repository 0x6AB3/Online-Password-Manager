using System;
using PasswordManagerUtilities;

namespace AES_GCM_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            AES_GCM StreamCipher = new AES_GCM();
            Console.WriteLine("Stream Cipher object initialised");

            byte[] key = StreamCipher.GenerateKey();
            Console.WriteLine($"256-bit key generated:\t{System.Convert.ToBase64String(key)}");

            Console.Write("Input data to be encrypted:\t");
            string data = Console.ReadLine();

            byte[] IV = StreamCipher.GenerateIV();
            Console.WriteLine($"Generated Initialisation Vector (Nonce):\t{System.Convert.ToBase64String(IV)}");

            byte[] encryptedData = StreamCipher.Encrypt(data);
            Console.WriteLine($"Data has been encrypted:\t{System.Convert.ToBase64String(encryptedData)}");

            string decryptedData = StreamCipher.Decrypt(encryptedData);
            Console.WriteLine($"Data has been decrypted:\t{decryptedData}");
        }
    }
}
