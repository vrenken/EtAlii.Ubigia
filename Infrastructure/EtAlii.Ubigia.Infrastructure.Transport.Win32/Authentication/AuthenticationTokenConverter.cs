namespace EtAlii.Ubigia.Infrastructure.Transport.Owin
{
    using System;
    using System.IO;
    using EtAlii.Ubigia.Api.Transport;
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
            var tokenAsBytes = new byte[] {};
            using (var stream = new MemoryStream())
            {
                using (var writer = new BsonDataWriter(stream))
                {
                    _serializer.Serialize(writer, token);
                }
                tokenAsBytes = stream.ToArray();
            }
            return tokenAsBytes;
        }

        public AuthenticationToken FromBytes(byte[] tokenAsBytes)
        {
            AuthenticationToken token;
            using (var stream = new MemoryStream(tokenAsBytes))
            {
                using (var reader = new BsonDataReader(stream))
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

        //public AuthenticationToken FromHttpActionContext(HttpActionContext actionContext)
        //{
        //    var authenticationTokenAsString = actionContext.Request.Headers
        //        .GetValues("Authentication-Token")
        //        .FirstOrDefault();
        //    return authenticationTokenAsString != null ? FromString(authenticationTokenAsString) : null;
        //}
    }
}