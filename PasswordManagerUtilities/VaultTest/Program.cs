using Newtonsoft.Json;
using PasswordManagerUtilities;
using System;
using System.Text;

namespace VaultTest
{
    class Program
    {
        static void Main(string[] args)
        {
            AES_CBC blockCipher = new AES_CBC();



            // input
            string username = "username3";
            string masterPassword = "s3cr3tP4ssw0rd";

            // Deriving 256-bit vault key and using it to generate a vault
            byte[] vaultKey = PBKDF2.DeriveKey(masterPassword, username, 100000);
            Vault personalVault = new Vault(vaultKey);

            // Getting and decrypting the internal key with the vault key
            byte[] internalKey = personalVault.GetInternalKey(vaultKey);
            blockCipher.Key = internalKey;

            // account input
            string schoolName = "richardHuish";
            string schoolUsername = "0044709";
            string schoolPassword = "5g7g57j567h";
            string schoolNote = "used to log in on hub.huish.ac.uk and moodle2";

            // raw account input
            byte[] rawName = UTF8Encoding.UTF8.GetBytes(schoolName);
            byte[] rawUsername = UTF8Encoding.UTF8.GetBytes(schoolUsername);
            byte[] rawPassword = UTF8Encoding.UTF8.GetBytes(schoolPassword);
            byte[] rawNote = UTF8Encoding.UTF8.GetBytes(schoolNote);

            // creating new account
            Account school = new Account();

            // using internal key to encrypt data
            blockCipher.Key = internalKey;

            school.AccountName = EncryptData(internalKey, rawName);
            school.Username = EncryptData(internalKey, rawUsername);
            school.Password = EncryptData(internalKey, rawPassword);
            school.Note = EncryptData(internalKey, rawNote);

            // Adding the school account to the vault
            personalVault.AddAccount(school);

            Console.WriteLine($"Accounts in vault: {personalVault.Length}");

            school = personalVault.GetAccount(0);
            blockCipher.IV = school.Password.IV;
            rawPassword = blockCipher.Decrypt(school.Password.Data);
            schoolPassword = UTF8Encoding.UTF8.GetString(rawPassword);
            Console.WriteLine($"Password for school account: {schoolPassword}");

            // Switching using a new vault key
            byte[] newVaultKey = PBKDF2.DeriveKey(schoolPassword, schoolName, 100000);

            personalVault.SetInternalKey(newVaultKey, vaultKey);



            string vault = JsonConvert.SerializeObject(personalVault, Formatting.Indented);
            Console.WriteLine(vault);

            byte[] rawVault = UTF8Encoding.UTF8.GetBytes(vault);
            Console.WriteLine(rawVault);


            Vault personalVaultCopy = JsonConvert.DeserializeObject<Vault>(vault);





            blockCipher.Key = personalVaultCopy.GetInternalKey(newVaultKey);
            AccountData encryptedPassword = personalVaultCopy.GetAccount(0).Password;
            blockCipher.IV = encryptedPassword.IV;
            byte[] password = blockCipher.Decrypt(encryptedPassword.Data);
            Console.WriteLine($"Decrypted password from copy vault: {UTF8Encoding.UTF8.GetString(password)}");


        }

        private static AccountData EncryptData(byte[] key, byte[] rawData)
        {
            AES_CBC blockCipher = new AES_CBC();
            blockCipher.Key = key;
            blockCipher.GenerateIV();
            AccountData encryptedData = new AccountData(blockCipher.Encrypt(rawData), blockCipher.IV);
            return encryptedData;
        }
    }
}
