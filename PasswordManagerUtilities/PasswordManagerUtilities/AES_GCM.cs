using System.Security.Cryptography;

namespace PasswordManagerUtilities
{
    // Implementation of Aes operating in Galois/Counter mode. Used for encryption the the internal key and for network transmission.
    public class AES_GCM : SymmetricAlgorithm
    {
        // The tag is used to verify that received data has not been modified.
        public byte[] Tag { get; set; }

        // Generating a 256-bit key along with an IV and Tag, both of maximum size supported by the algorithm (96-bit IV, 128-bit).
        public AES_GCM()
        {
            Key = new byte[32];
            GenerateKey();
            IV = new byte[AesGcm.NonceByteSizes.MaxSize];
            GenerateIV();
            Tag = new byte[AesGcm.TagByteSizes.MaxSize];
        }


        public override byte[] Encrypt(byte[] plaintext)
        {
            // As the algorithm is a stream cipher, the ciphertext length will be equal to plaintext length.
            byte[] ciphertext = new byte[plaintext.Length];

            // Encrypting the plaintext with a AesGcm instance.
            using (AesGcm aes = new AesGcm(Key))
            {
                // The tag and ciphertext are set by the function.
                aes.Encrypt(IV, plaintext, ciphertext, Tag);
            }

            return ciphertext;
        }
        public override byte[] Decrypt(byte[] ciphertext)
        {
            byte[] plaintext = new byte[ciphertext.Length];

            using (AesGcm aes = new AesGcm(Key))
            {
                aes.Decrypt(IV, ciphertext, Tag, plaintext);
            }

            return plaintext;
        }

    }
}
