using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace PasswordManagerUtilities
{
    [Serializable]
    public class Vault : ISerializable
    {
        // A list which holds all accounts that are in the vault.
        private List<Account> accounts;

        // The encrypted internal key stored inside the vault.
        private InternalKey encryptedInternalKey;

        // Generating a random 256-bit key for use in AES-CBC encryption (the internal key).
        public Vault(byte[] vaultKey)
        {
            // Creating the internal key
            byte[] internalKey = new AES_CBC().Key;

            // Storing internal key after encrypting it with the vault key using GCM encryption.
            AES_GCM streamCipher = new AES_GCM();
            streamCipher.Key = vaultKey;

            encryptedInternalKey = new InternalKey(streamCipher.Encrypt(internalKey), streamCipher.IV, streamCipher.Tag);

            accounts = new List<Account>();

        }
        public Account GetAccount(int index)
        {
            return accounts[index];
        }

        public void AddAccount(Account account)
        {
            accounts.Add(account);
        }

        public void RemoveAccount(Account account)
        {
            accounts.Remove(account);
        }

        public int Length { get => accounts.Count; }

        public byte[] GetInternalKey(byte[] vaultKey)
        {
            // Decrypting the internal key with the vault key and the IV which was created during encryption.
            AES_GCM streamCipher = new AES_GCM();
            streamCipher.Key = vaultKey;
            streamCipher.IV = encryptedInternalKey.IV;
            streamCipher.Tag = encryptedInternalKey.Tag;

            byte[] internalKey = streamCipher.Decrypt(encryptedInternalKey.Data);
            return internalKey;
        }
        
        public void SetInternalKey(byte[] newVaultKey, byte[] oldVaultKey)
        {
            AES_GCM streamCipher = new AES_GCM();

            // Decrypting the internal key with the original vault key.
            streamCipher.Key = oldVaultKey;
            streamCipher.IV = encryptedInternalKey.IV;
            streamCipher.Tag = encryptedInternalKey.Tag;
            byte[] internalKey = streamCipher.Decrypt(encryptedInternalKey.Data);

            // Encrypting with new vault key.
            streamCipher.Key = newVaultKey;
            streamCipher.GenerateIV();
            internalKey = streamCipher.Encrypt(internalKey);

            // Storing the new internal key.
            encryptedInternalKey = new InternalKey(internalKey, streamCipher.IV, streamCipher.Tag);
        }

        // Methods for serialisation/deserialisation.
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("accounts", accounts, typeof(List<Account>));
            info.AddValue("encryptedInternalKey", encryptedInternalKey, typeof(InternalKey));
        }

        public Vault(SerializationInfo info, StreamingContext context)
        {
            accounts = (List<Account>) info.GetValue("accounts", typeof(List<Account>));
            encryptedInternalKey = (InternalKey)info.GetValue("encryptedInternalKey", typeof(InternalKey));
        }
    }

    // Contents are stored in encrypted form.
    public class Account
    {
        public AccountData AccountName { get; set; }
        public AccountData Username { get; set; }
        public AccountData Password { get; set; }
        public AccountData Note { get; set; }
    }

    // A single piece of encrypted data, with IV included.
    public class AccountData
    {
        public byte[] Data { get; set; }
        public byte[] IV { get; set; }

        public AccountData(byte[] accountData, byte[] dataIV)
        {
            Data = accountData;
            IV = dataIV;
        }
    }

    // The encrypted vault internal key used for decrypting AccountData objects, includes an authentication tag that was calculated during encryption.
    public class InternalKey : AccountData
    {
        public byte[] Tag { get; set; }
        public InternalKey(byte[] accountData, byte[] dataIV, byte[] tag) : base(accountData, dataIV)
        {
            Tag = tag;
        }
    }

}
