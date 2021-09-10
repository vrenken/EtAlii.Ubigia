// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Hosting.Tests
{
    using System.Collections.Generic;
    using System.Security.Cryptography;
    using Xunit;
    using Aes = EtAlii.Ubigia.Infrastructure.Transport.Aes;
    using EtAlii.Ubigia.Tests;

    // TODO: Move all instances of this test class to single testproject
    [CorrelateUnitTests]
    public class AesTest
    {
        [Fact]
        public void Aes_Encrypt_With_Few_Bytes()
        {
            var originalBytes = CreateBytes(64);
            var encryptedBytes = Aes.Encrypt(originalBytes);
            var decryptedBytes = Aes.Decrypt(encryptedBytes);
            Assert.Equal(originalBytes, decryptedBytes, EqualityComparer<byte>.Default);
        }

        [Fact]
        public void Aes_Encrypt_With_Many_Bytes()
        {
            var originalBytes = CreateBytes(1024 * 1024);
            var encryptedBytes = Aes.Encrypt(originalBytes);
            var decryptedBytes = Aes.Decrypt(encryptedBytes);
            Assert.Equal(originalBytes, decryptedBytes, EqualityComparer<byte>.Default);
        }

        [Fact]
        public void Aes_Encrypt_With_Few_Bytes_And_Not_Equal()
        {
            var originalBytes = CreateBytes(64);
            var encryptedBytes = Aes.Encrypt(originalBytes);
            var decryptedBytes = Aes.Decrypt(encryptedBytes);

            var decryptedByte = decryptedBytes[2];
            decryptedBytes[2] = (byte)(decryptedByte == 0 ? decryptedByte + 1 : decryptedByte - 1);

            Assert.NotEqual(originalBytes, decryptedBytes, EqualityComparer<byte>.Default);
        }

        [Fact]
        public void Aes_Decrypt_With_Invalid_Bytes()
        {
            var originalBytes = CreateBytes(64);
            var encryptedBytes = Aes.Encrypt(originalBytes);

            var encryptedByte = encryptedBytes[2];
            encryptedBytes[2] = (byte)(encryptedByte == 0 ? encryptedByte + 1 : encryptedByte - 1);

            var decryptedBytes = Aes.Decrypt(encryptedBytes);

            Assert.NotEqual(originalBytes, decryptedBytes, EqualityComparer<byte>.Default);
        }

        private byte[] CreateBytes(int length)
        {
            var bytes = new byte[length];
            using var rnd = RandomNumberGenerator.Create();
            rnd.GetNonZeroBytes(bytes);
            return bytes;
        }
    }
}
