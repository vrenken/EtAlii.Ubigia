﻿namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System.Security.Cryptography;

    public static class Aes
    {
        private static readonly SymmetricAlgorithm _algorithm = CreateAesCryptoServiceProvider();

        private static SymmetricAlgorithm CreateAesCryptoServiceProvider()
        {
            var algorithm = new AesCryptoServiceProvider();
            algorithm.GenerateKey();
            algorithm.GenerateIV();
            return algorithm;
        }

        public static byte[] Encrypt(byte[] bytes)
        {
            using var encryptor = _algorithm.CreateEncryptor();
            bytes = encryptor.TransformFinalBlock(bytes, 0, bytes.Length);
            return bytes;
        }

        public static byte[] Decrypt(byte[] bytes)
        {
            using var decryptor = _algorithm.CreateDecryptor();
            bytes = decryptor.TransformFinalBlock(bytes, 0, bytes.Length);
            return bytes;
        }
    }
}