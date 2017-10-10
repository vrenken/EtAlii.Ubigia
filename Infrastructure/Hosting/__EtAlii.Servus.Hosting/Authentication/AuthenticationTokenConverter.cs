namespace EtAlii.Servus.Infrastructure.Hosting
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Bson;
    using System.IO;

    internal class AuthenticationTokenConverter
    {
        private static readonly JsonSerializer _serializer = new JsonSerializer();

        public static byte[] ToBytes(AuthenticationToken token)
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

        public static AuthenticationToken FromBytes(byte[] tokenAsBytes)
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
    }
}