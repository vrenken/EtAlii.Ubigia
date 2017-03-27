namespace EtAlii.Ubigia.Infrastructure.Hosting.UnitTests
{
    using EtAlii.Ubigia.Storage.Tests;
    using Xunit;
    using System;
    using EtAlii.Ubigia.Infrastructure.Transport.Owin;


    public class Aes_Test
    {
        [Fact]
        public void Aes_Encrypt_With_Few_Bytes()
        {
            var originalBytes = CreateBytes(64);
            var encryptedBytes = Aes.Encrypt(originalBytes);
            var decryptedBytes = Aes.Decrypt(encryptedBytes);
            AssertData.AreEqual(originalBytes, decryptedBytes);
        }

        [Fact]
        public void Aes_Encrypt_With_Many_Bytes()
        {
            var originalBytes = CreateBytes(1024 * 1024);
            var encryptedBytes = Aes.Encrypt(originalBytes);
            var decryptedBytes = Aes.Decrypt(encryptedBytes);
            AssertData.AreEqual(originalBytes, decryptedBytes);
        }

        [Fact]
        public void Aes_Encrypt_With_Few_Bytes_And_Not_Equal()
        {
            var originalBytes = CreateBytes(64);
            var encryptedBytes = Aes.Encrypt(originalBytes);
            var decryptedBytes = Aes.Decrypt(encryptedBytes);

            var decryptedByte = decryptedBytes[2];
            decryptedBytes[2] = (byte)(decryptedByte == 0 ? decryptedByte + 1 : decryptedByte - 1);

            AssertData.AreNotEqual(originalBytes, decryptedBytes);
        }

        [Fact]
        public void Aes_Decrypt_With_Invalid_Bytes()
        {
            var originalBytes = CreateBytes(64);
            var encryptedBytes = Aes.Encrypt(originalBytes);

            var encryptedByte = encryptedBytes[2];
            encryptedBytes[2] = (byte)(encryptedByte == 0 ? encryptedByte + 1 : encryptedByte - 1);

            var decryptedBytes = Aes.Decrypt(encryptedBytes);

            AssertData.AreNotEqual(originalBytes, encryptedBytes);
        }

        private byte[] CreateBytes(int length)
        {
            var random = new Random();
            var bytes = new byte[length];
            random.NextBytes(bytes);
            return bytes;
        }
    }
}
