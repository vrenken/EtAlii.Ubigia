namespace EtAlii.Servus.Api.Transport
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http.Formatting;
    using System.Net.Http.Headers;
    using System.Reflection;
    using System.Text;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Bson;

    public class PayloadMediaTypeFormatter : BaseJsonMediaTypeFormatter
    {
        private static readonly Type OpenDictionaryType = typeof(Dictionary<,>);
        private static readonly TypeInfo EnumerableTypeInfo = typeof(IEnumerable).GetTypeInfo();
        private static readonly TypeInfo DictionaryTypeInfo = typeof(IDictionary).GetTypeInfo();

        public static readonly MediaTypeWithQualityHeaderValue MediaType = new MediaTypeWithQualityHeaderValue("application/bson");

        /// <summary>
        /// Initializes a new instance of the <see cref="BsonMediaTypeFormatter"/> class.
        /// </summary>
        public PayloadMediaTypeFormatter()
        {
            // Set default supported media type
            SupportedMediaTypes.Add(MediaType);

            SerializerFactory.AddDefaultConverters(SerializerSettings.Converters);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BsonMediaTypeFormatter"/> class.
        /// </summary>
        /// <param name="formatter">The <see cref="BsonMediaTypeFormatter"/> instance to copy settings from.</param>
        protected PayloadMediaTypeFormatter(BsonMediaTypeFormatter formatter)
            : base(formatter)
        {
        }

        /// <inheritdoc />
        public override object ReadFromStream(Type type, Stream readStream, Encoding effectiveEncoding, IFormatterLogger formatterLogger)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            if (readStream == null)
            {
                throw new ArgumentNullException("readStream");
            }

            if (effectiveEncoding == null)
            {
                throw new ArgumentNullException("effectiveEncoding");
            }

            // Special-case for simple types: Deserialize a Dictionary with a single element named Value.
            // Serialization created this Dictionary<string, object> to work around BSON restrictions: BSON cannot
            // handle a top-level simple type.  NewtonSoft.Json throws a JsonWriterException with message "Error
            // writing Binary value. BSON must start with an Object or Array. Path ''." when WriteToStream() is given
            // such a value.
            //
            // Added clause for typeof(byte[]) needed because NewtonSoft.Json sometimes throws above Exception when
            // WriteToStream() is given a byte[] value.  (Not clear where the bug lies and, worse, it doesn't reproduce
            // reliably.)
            //
            // Request for typeof(object) may cause a simple value to round trip as a JObject.
            if (IsSimpleType(type) || type == typeof(byte[]))
            {
                // Read as exact expected Dictionary<string, T> to ensure NewtonSoft.Json does correct top-level conversion.
                var dictionaryType = OpenDictionaryType.MakeGenericType(new Type[] { typeof(string), type });
                var dictionary = base.ReadFromStream(dictionaryType, readStream, effectiveEncoding, formatterLogger) as IDictionary;
                if (dictionary == null)
                {
                    // Not valid since BaseJsonMediaTypeFormatter.ReadFromStream(Type, Stream, HttpContent, IFormatterLogger)
                    // handles empty content and does not call ReadFromStream(Type, Stream, Encoding, IFormatterLogger)
                    // in that case.
                    var e1 = new InvalidOperationException("Missing data");
                    e1.Data.Add("dictionaryType.Name", dictionaryType.Name);
                    throw e1;
                }

                // Unfortunately IDictionary doesn't have TryGetValue()...
                var firstKey = String.Empty;
                foreach (DictionaryEntry item in dictionary)
                {
                    if (dictionary.Count == 1 && (item.Key as string) == "Value")
                    {
                        // Success
                        return item.Value;
                    }
                    else
                    {
                        if (item.Key != null)
                        {
                            firstKey = item.Key.ToString();
                        }

                        break;
                    }
                }

                var e2 = new InvalidOperationException("Unexpected Data");
                e2.Data.Add("dictionary.Count", dictionary.Count);
                e2.Data.Add("firstKey", firstKey);
                throw e2;
            }
            else
            {
                return base.ReadFromStream(type, readStream, effectiveEncoding, formatterLogger);
            }
        }

        /// <inheritdoc />
        public override JsonReader CreateJsonReader(Type type, Stream readStream, Encoding effectiveEncoding)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            if (readStream == null)
            {
                throw new ArgumentNullException("readStream");
            }

            if (effectiveEncoding == null)
            {
                throw new ArgumentNullException("effectiveEncoding");
            }

            var reader = new BsonReader(new BinaryReader(readStream, effectiveEncoding));

            try
            {
                // Special case discussed at http://stackoverflow.com/questions/16910369/bson-array-deserialization-with-json-net
                // Dispensed with string (aka IEnumerable<char>) case above in ReadFromStream()
                reader.ReadRootValueAsArray = EnumerableTypeInfo.IsAssignableFrom(type.GetTypeInfo()) && !DictionaryTypeInfo.IsAssignableFrom(type.GetTypeInfo());
            }
            catch
            {
                // Ensure instance is cleaned up in case of an issue
                ((IDisposable)reader).Dispose();
                throw;
            }

            return reader;
        }

        /// <inheritdoc />
        public override void WriteToStream(Type type, object value, Stream writeStream, Encoding effectiveEncoding)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            if (writeStream == null)
            {
                throw new ArgumentNullException("writeStream");
            }

            if (effectiveEncoding == null)
            {
                throw new ArgumentNullException("effectiveEncoding");
            }

            if (value == null)
            {
                // Cannot serialize null at the top level.  Json.Net throws Newtonsoft.Json.JsonWriterException : Error
                // writing Null value. BSON must start with an Object or Array. Path ''.  Fortunately
                // BaseJsonMediaTypeFormatter.ReadFromStream(Type, Stream, HttpContent, IFormatterLogger) treats zero-
                // length content as null or the default value of a struct.
                return;
            }

            // See comments in ReadFromStream() above about this special case and the need to include byte[] in it.
            // Using runtime type here because Json.Net will throw during serialization whenever it cannot handle the
            // runtime type at the top level. For e.g. passed type may be typeof(object) and value may be a string.
            var runtimeType = value.GetType();
            if (IsSimpleType(runtimeType) || runtimeType == typeof(byte[]))
            {
                // Wrap value in a Dictionary with a single property named "Value" to provide BSON with an Object.  Is
                // written out as binary equivalent of { "Value": value } JSON.
                var temporaryDictionary = new Dictionary<string, object>
                {
                    { "Value", value },
                };
                base.WriteToStream(typeof(Dictionary<string, object>), temporaryDictionary, writeStream, effectiveEncoding);
            }
            else
            {
                base.WriteToStream(type, value, writeStream, effectiveEncoding);
            }
        }

        /// <inheritdoc />
        public override JsonWriter CreateJsonWriter(Type type, Stream writeStream, Encoding effectiveEncoding)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            if (writeStream == null)
            {
                throw new ArgumentNullException("writeStream");
            }

            if (effectiveEncoding == null)
            {
                throw new ArgumentNullException("effectiveEncoding");
            }

            return new BsonWriter(new BinaryWriter(writeStream, effectiveEncoding));
        }

        // Return true if Json.Net will likely convert value of given type to a Json primitive, not JsonArray nor
        // JsonObject.
        // To do: https://aspnetwebstack.codeplex.com/workitem/1467
        private static bool IsSimpleType(Type type)
        {
            // Cannot happen.
            // Contract.Assert(type != null);

            bool isSimpleType;

            isSimpleType = type.GetTypeInfo().IsValueType || type == typeof(string);

            return isSimpleType;
        }    }
}