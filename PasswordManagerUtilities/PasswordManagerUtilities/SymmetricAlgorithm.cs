using System.Security.Cryptography;

namespace PasswordManagerUtilities
{
    // Base class for all implementations of Symmetric Encryption Algorithms.
    public abstract class SymmetricAlgorithm
    {
        // The IV and key which will be used for encryption/decryption.
        public byte[] IV { get; set; }
        public byte[] Key { get; set; }

        // Empty class constructor
        public SymmetricAlgorithm() { }

        // Encryption/Decryption functions that a child class overrides.
        public abstract byte[] Encrypt(byte[] plaintext);
        public abstract byte[] Decrypt(byte[] ciphertext);

        // Pseudorandom IV/key generation.
        public byte[] GenerateKey()
        {
            RandomNumberGenerator.Fill(Key);
            return Key;
        }
        public byte[] GenerateIV()
        {
            RandomNumberGenerator.Fill(IV);
            return IV;
        }

    }
}
