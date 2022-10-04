// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System;
    using System.IO;

    public class AuthenticationTokenConverter : IAuthenticationTokenConverter
    {
        public byte[] ToBytes(AuthenticationToken token)
        {
            if (token == null)
            {
                throw new ArgumentNullException(nameof(token));
            }

            using var stream = new MemoryStream();
            using var writer = new BinaryWriter(stream);

            writer.Write(token, AuthenticationToken.Write);

            var tokenAsBytes = stream.ToArray();
            return tokenAsBytes;
        }

        public AuthenticationToken FromBytes(byte[] tokenAsBytes)
        {
            if (tokenAsBytes == null)
            {
                throw new ArgumentNullException(nameof(tokenAsBytes));
            }
            if (tokenAsBytes.Length == 0)
            {
                return null;
            }
            using var stream = new MemoryStream(tokenAsBytes);
            using var reader = new BinaryReader(stream);

            return reader.Read(AuthenticationToken.Read);
        }

        public AuthenticationToken FromString(string authenticationTokenAsString)
        {
            var authenticationTokenAsBytes = Convert.FromBase64String(authenticationTokenAsString);
            authenticationTokenAsBytes = Aes.Decrypt(authenticationTokenAsBytes);
            var authenticationToken = FromBytes(authenticationTokenAsBytes);
            return authenticationToken;
        }
    }
}
