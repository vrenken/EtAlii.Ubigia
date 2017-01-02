namespace EtAlii.Servus.Infrastructure.WebApi
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Bson;

    internal class BsonMediaTypeFormatter : MediaTypeFormatter
    {
        private JsonSerializerSettings _jsonSerializerSettings;

        public BsonMediaTypeFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/bson"));
            _jsonSerializerSettings = CreateDefaultSerializerSettings();
        }

        public JsonSerializerSettings CreateDefaultSerializerSettings()
        {
            return new JsonSerializerSettings()
            {
                MissingMemberHandling = MissingMemberHandling.Ignore,
                TypeNameHandling = TypeNameHandling.None
            };
        }

        public JsonSerializerSettings SerializerSettings
        {
            get { return _jsonSerializerSettings; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Value is null");
                }

                _jsonSerializerSettings = value;
            }
        }

        public override bool CanReadType(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type is null");
            }
            return true;
        }

        public override bool CanWriteType(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("Type is null");
            }
            return true;
        }

        public override Task WriteToStreamAsync(Type type, object value, Stream stream, HttpContent content, TransportContext transportContext)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type is null");
            }
            if (stream == null)
            {
                throw new ArgumentNullException("Write stream is null");
            }

            var tcs = new TaskCompletionSource<object>();
            using (var bsonWriter = new BsonWriter(stream) { CloseOutput = false })
            {
                var jsonSerializer = JsonSerializer.Create(_jsonSerializerSettings);
                jsonSerializer.Serialize(bsonWriter, value);
                bsonWriter.Flush();
                tcs.SetResult(null);
            }

            return tcs.Task;
        }

        public override Task<object> ReadFromStreamAsync(Type type, Stream stream, HttpContent content, IFormatterLogger formatterLogger)
        {
            var tcs = new TaskCompletionSource<object>();
            if (content != null && content.Headers.ContentLength == 0)
            {
                return null;
            }

            try
            {
                var reader = new BsonReader(stream);
                if (typeof(IEnumerable).IsAssignableFrom(type))
                {
                    reader.ReadRootValueAsArray = true;
                }
                using (reader)
                {
                    var jsonSerializer = JsonSerializer.Create(_jsonSerializerSettings);
                    var output = jsonSerializer.Deserialize(reader, type);
                    if (formatterLogger != null)
                    {
                        jsonSerializer.Error += (sender, e) =>
                        {
                            Exception exception = e.ErrorContext.Error;
                            formatterLogger.LogError(e.ErrorContext.Path, exception.Message);
                            e.ErrorContext.Handled = true;
                        };
                    }
                    tcs.SetResult(output);
                }
            }
            catch (Exception e)
            {
                if (formatterLogger == null) throw;
                formatterLogger.LogError(String.Empty, e.Message);
                tcs.SetResult(GetDefaultValueForType(type));
            }

            return tcs.Task;
        }
    }
}