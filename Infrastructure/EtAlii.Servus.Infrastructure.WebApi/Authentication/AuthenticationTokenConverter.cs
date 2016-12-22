namespace EtAlii.Servus.Infrastructure
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Web.Http.Controllers;
    using EtAlii.Servus.Api.Transport;
    using Newtonsoft.Json.Bson;

    internal class AuthenticationTokenConverter : IAuthenticationTokenConverter
    {
        private readonly ISerializer _serializer;

        public AuthenticationTokenConverter(ISerializer serializer)
        {
            _serializer = serializer;
        }

        public byte[] ToBytes(AuthenticationToken token)
        {
            var tokenAsBytes = new byte[] {};
            using (var stream = new MemoryStream())
            {
                using (var writer = new BsonWriter(stream))
                {
                    _serializer.Serialize(writer, token);
                }
                tokenAsBytes = stream.GetBuffer();
            }
            return tokenAsBytes;
        }

        public AuthenticationToken FromBytes(byte[] tokenAsBytes)
        {
            AuthenticationToken token;
            using (var stream = new MemoryStream(tokenAsBytes))
            {
                using (var reader = new BsonReader(stream))
                {
                    token = _serializer.Deserialize<AuthenticationToken>(reader);
                }
            }
            return token;
        }

        public AuthenticationToken FromString(string authenticationTokenAsString)
        {
            var authenticationTokenAsBytes = Convert.FromBase64String(authenticationTokenAsString);
            authenticationTokenAsBytes = Aes.Decrypt(authenticationTokenAsBytes);
            var authenticationToken = FromBytes(authenticationTokenAsBytes);
            return authenticationToken;
        }

        public AuthenticationToken FromHttpActionContext(HttpActionContext actionContext)
        {
            var authenticationTokenAsString = actionContext.Request.Headers
                .GetValues("Authentication-Token")
                .FirstOrDefault();
            return authenticationTokenAsString != null ? FromString(authenticationTokenAsString) : null;
        }
    }
}