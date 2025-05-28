using System;
using PasswordManagerUtilities;

namespace NEAEncryptionTesting
{
    class CBC_Test
    {
        static void Main()
        {
            /*
            Console.Write("Enter username (at least 8 characters): ");
            string username = Console.ReadLine();

            Console.Write("Enter password (at least 8 characters): ");
            string password = Console.ReadLine();
            */
            /*
            string password = "testpassword";
            string username = "testusername";

            byte[] derivedKey = PBKDF2.DeriveKey(password, username, 100000);

            byte[] keyToEncrypt = new AES_CBC().GenerateKey();

            AES_GCM streamCipher = new AES_GCM();
            streamCipher.Key = derivedKey;

            byte[] encryptedData = streamCipher.Encrypt(keyToEncrypt);
            byte[] decryptedData = streamCipher.Decrypt(encryptedData);

            bool suc = true;
            for (int i = 0; i < decryptedData.Length; i++)
            {
                if (decryptedData[i] != keyToEncrypt[i])
                {
                    suc = false;
                    break;
                }
            }

            /*
            AES_CBC blockCipher = new AES_CBC();
            byte[] IV = blockCipher.IV;
            blockCipher.Key = derivedKey;

            blockCipher.IV = IV;
            byte[] encryptedKey = blockCipher.Encrypt(keyToEncrypt);
            blockCipher.IV = IV;
            byte[] decryptedKey = blockCipher.Decrypt(encryptedKey);
            Console.WriteLine();

            /*
            Console.WriteLine($"Derived a key of length [{derivedKey.Length}]\nDerived Key (Base64): {System.Convert.ToBase64String(derivedKey)}");

            AES_CBC BlockCipher = new AES_CBC();

            byte[] IV = BlockCipher.GenerateIV();

            Console.WriteLine($"Generated an IV of length [{IV.Length}]\nInitialisation Vector (Base64): {System.Convert.ToBase64String(IV)}");

            BlockCipher.Key = derivedKey;

            Console.Write("Enter the data that you wish to encrypt: ");
            string data = Console.ReadLine();

            byte[] encryptedData = BlockCipher.Encrypt(data);

            Console.WriteLine($"Encrypted data size is [{encryptedData.Length}]\nEncrypted data (Base64): {System.Convert.ToBase64String(encryptedData)}");

            string decryptedData = BlockCipher.Decrypt(encryptedData);

            Console.WriteLine($"Decrypted data of length [{decryptedData.Length}]: {decryptedData}");

            */
        }
    }
}
