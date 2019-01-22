namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System.Security.Cryptography;

    public static class Aes
    {
        private static readonly SymmetricAlgorithm Algorithm;

        static Aes()
        {
            Algorithm = new AesCryptoServiceProvider();
            Algorithm.GenerateKey();
            Algorithm.GenerateIV();
        }

        public static byte[] Encrypt(byte[] bytes)
        {
            using (var encryptor = Algorithm.CreateEncryptor())
            {
                bytes = encryptor.TransformFinalBlock(bytes, 0, bytes.Length);
                return bytes;
            }
        }

        public static byte[] Decrypt(byte[] bytes)
        {
            using (var decryptor = Algorithm.CreateDecryptor())
            {
                bytes = decryptor.TransformFinalBlock(bytes, 0, bytes.Length);
                return bytes;
            }
        }
    }
}