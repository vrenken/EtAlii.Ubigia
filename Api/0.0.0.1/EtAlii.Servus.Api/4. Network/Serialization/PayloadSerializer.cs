﻿namespace EtAlii.Servus.Api.Transport
{
    using System.IO;
    using System.Text;
    using Newtonsoft.Json;

    public class PayloadSerializer : IPayloadSerializer
    {
        private readonly ISerializer _serializer;

        public PayloadSerializer(ISerializer serializer)
        {
            _serializer = serializer;
        }

        public byte[] Serialize(object value)
        {
            var bytes = new byte[] { };

            if (value != null)
            {
                using (var textWriter = new StringWriter())
                {
                    using (var writer = new JsonTextWriter(textWriter))
                    {
                        writer.CloseOutput = false;
                        _serializer.Serialize(writer, value);
                    }
                    var serializedValue = textWriter.ToString();
                    bytes = Encoding.UTF8.GetBytes(serializedValue);
                }
            }

            return bytes;
        }

        public T Deserialize<T>(byte[] bytes)
        {
            var serializedValue = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
            
            using (var textReader = new StringReader(serializedValue))
            {
                using (var reader = new JsonTextReader(textReader))
                {
                    return _serializer.Deserialize<T>(reader);
                }
            }
        }
    }
}
