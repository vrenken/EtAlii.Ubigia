// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System;
    using System.IO;
    using EtAlii.Ubigia.Serialization;
    using Newtonsoft.Json.Bson;

    public class AuthenticationTokenConverter : IAuthenticationTokenConverter
    {
        private readonly ISerializer _serializer;

        public AuthenticationTokenConverter(ISerializer serializer)
        {
            _serializer = serializer;
        }

        public byte[] ToBytes(AuthenticationToken token)
        {
            using var stream = new MemoryStream();
            using (var writer = new BsonDataWriter(stream))
            {
                _serializer.Serialize(writer, token);
            }
            var tokenAsBytes = stream.ToArray();
            return tokenAsBytes;
        }

        public AuthenticationToken FromBytes(byte[] tokenAsBytes)
        {
            using var stream = new MemoryStream(tokenAsBytes);
            using var reader = new BsonDataReader(stream);
            var token = _serializer.Deserialize<AuthenticationToken>(reader);

            return token;
        }

        public AuthenticationToken FromString(string authenticationTokenAsString)
        {
            var authenticationTokenAsBytes = Convert.FromBase64String(authenticationTokenAsString);
            authenticationTokenAsBytes = Aes.Decrypt(authenticationTokenAsBytes);
            var authenticationToken = FromBytes(authenticationTokenAsBytes);
            return authenticationToken;
        }

        //public AuthenticationToken FromHttpActionContext(HttpActionContext actionContext)
        //[
        //    var authenticationTokenAsString = actionContext.Request.Headers
        //        .GetValues("Authentication-Token")
        //        .FirstOrDefault()
        //    return authenticationTokenAsString != null ? FromString(authenticationTokenAsString) : null
        //]
    }
}