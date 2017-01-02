namespace EtAlii.Servus.Infrastructure.Hosting
{
    using System.Security.Cryptography;

    internal class Aes
    {
        private static readonly SymmetricAlgorithm _algorithm;

        static Aes()
        {
            _algorithm = new AesCryptoServiceProvider();
            _algorithm.GenerateKey();
            _algorithm.GenerateIV();
        }

        public static byte[] Encrypt(byte[] bytes)
        {
            using (var encryptor = _algorithm.CreateEncryptor())
            {
                bytes = encryptor.TransformFinalBlock(bytes, 0, bytes.Length);
                return bytes;
            }
        }

        public static byte[] Decrypt(byte[] bytes)
        {
            using (var decryptor = _algorithm.CreateDecryptor())
            {
                bytes = decryptor.TransformFinalBlock(bytes, 0, bytes.Length);
                return bytes;
            }
        }
    }
}