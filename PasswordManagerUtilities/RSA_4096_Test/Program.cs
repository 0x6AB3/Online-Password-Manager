using System;
using PasswordManagerUtilities;

namespace RSA_4096_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            RSA_4096 rsa1 = new RSA_4096();
            RSA_4096 rsa2 = new RSA_4096();

            int rsalength = rsa1.GetPublicKey().Length;
            Console.WriteLine(rsalength);
            
            string rsa1Pub = System.Convert.ToBase64String(rsa1.GetPublicKey());
            string rsa2Pub = System.Convert.ToBase64String(rsa2.GetPublicKey());

            Console.WriteLine($"\nRSA 1 Pub: {rsa1Pub}\n" +
                              $"\nRSA 2 Pub: {rsa2Pub}\n");

            rsa1.SetPublicKey(rsa2.GetPublicKey());

            rsa1Pub = System.Convert.ToBase64String(rsa1.GetPublicKey());

            Console.WriteLine($"\nNew RSA 1 Pub: {rsa1Pub}\n");
            
            byte[] vaultKey = PBKDF2.DeriveKey("verysecretpassword", "averageusername", 100000);

            for (int i = 0; i < 5; i++)
            {
                byte[] encryptedVaultKey = rsa2.Encrypt(vaultKey);
                byte[] decryptedVaultKey = rsa2.Decrypt(encryptedVaultKey);

                string vaultkey = ""; // System.Convert.ToBase64String(vaultKey);
                string encryptedkey = System.Convert.ToBase64String(encryptedVaultKey);
                string decryptedkey = ""; // System.Convert.ToBase64String(decryptedVaultKey);

                Console.WriteLine($"\nVault Key: {vaultkey}\n\nEncrypted: {encryptedkey}\n\nDecrypted: {decryptedkey}\n");
            }

            
            

        }
    }
}
