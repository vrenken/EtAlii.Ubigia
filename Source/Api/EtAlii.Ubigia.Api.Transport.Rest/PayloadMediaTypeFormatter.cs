namespace EtAlii.Ubigia.Api.Transport.Rest
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.IO;
	using System.Net;
	using System.Net.Http;
	using System.Net.Http.Formatting;
	using System.Net.Http.Headers;
	using System.Reflection;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Serialization;
    using Newtonsoft.Json.Bson;

	public class PayloadMediaTypeFormatter : MediaTypeFormatter
    {
        private static readonly Type _openDictionaryType = typeof(Dictionary<,>);
        private static readonly TypeInfo _enumerableTypeInfo = typeof(IEnumerable).GetTypeInfo();
        private static readonly TypeInfo _dictionaryTypeInfo = typeof(IDictionary).GetTypeInfo();

        public static readonly MediaTypeWithQualityHeaderValue MediaType = new("application/bson");
	    public static readonly MediaTypeHeaderValue ContentMediaType = new("application/bson");
	    private readonly ISerializer _serializer;

		public PayloadMediaTypeFormatter()
		{
			// Set default supported media type
			SupportedMediaTypes.Add(MediaType);
			_serializer = new SerializerFactory().Create();
		}

		public override bool CanReadType(Type type)
		{
			return true;
		}

	    public override bool CanWriteType(Type type)
	    {
		    return true;
	    }

	    public override Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
	    {
		    if (type == null)
		    {
			    throw new ArgumentNullException(nameof(type));
		    }

		    if (readStream == null)
		    {
			    throw new ArgumentNullException(nameof(readStream));
		    }

		    return ReadFromStreamInternalAsync(type, readStream);//, content, formatterLogger
	    }

	    private Task<object> ReadFromStreamInternalAsync(Type type, Stream readStream)//, HttpContent content, IFormatterLogger formatterLogger
	    {
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
            if (IsSimpleType(type))
            {
                // Read as exact expected Dictionary<string, T> to ensure NewtonSoft.Json does correct top-level conversion.
                var dictionaryType = _openDictionaryType.MakeGenericType(typeof(string), type);
                if (!(ReadFromStreamInternal(dictionaryType, readStream) is IDictionary dictionary))
                {
                    // Not valid since BaseJsonMediaTypeFormatter.ReadFromStream(Type, Stream, HttpContent, IFormatterLogger)
                    // handles empty content and does not call ReadFromStream(Type, Stream, Encoding, IFormatterLogger)
                    // in that case.
                    var e1 = new InvalidOperationException("Missing data");
                    e1.Data.Add("dictionaryType.Name", dictionaryType.Name);
                    throw e1;
                }

                // Unfortunately IDictionary doesn't have TryGetValue()...
                var firstKey = string.Empty;
                foreach (DictionaryEntry item in dictionary)
                {
                    if (dictionary.Count == 1 && (item.Key as string) == "Value")
                    {
                        // Success
                        return Task.FromResult(item.Value);
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
				var result = ReadFromStreamInternal(type, readStream);
	            return Task.FromResult(result);
            }
        }

	    private object ReadFromStreamInternal(Type type, Stream readStream)
	    {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (readStream == null)
            {
                throw new ArgumentNullException(nameof(readStream));
            }

            using var innerReader = new BinaryReader(readStream);
            using var reader = new BsonDataReader(innerReader) {CloseInput = false};

            try
            {
                // Special case discussed at http://stackoverflow.com/questions/16910369/bson-array-deserialization-with-json-net
                // Dispensed with string (aka IEnumerable<char>) case above in ReadFromStream()
                reader.ReadRootValueAsArray = _enumerableTypeInfo.IsAssignableFrom(type.GetTypeInfo()) && !_dictionaryTypeInfo.IsAssignableFrom(type.GetTypeInfo());
            }
            catch
            {
                // Ensure instance is cleaned up in case of an issue
                ((IDisposable)reader).Dispose();
                throw;
            }

		    return _serializer.Deserialize(reader, type);
	    }


	    public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext)
	    {
		    if (type == null)
		    {
			    throw new ArgumentNullException(nameof(type));
		    }

		    if (writeStream == null)
		    {
			    throw new ArgumentNullException(nameof(writeStream));
		    }

		    return WriteToStreamInternalAsync(value, writeStream);//, content, transportContext
	    }

	    private Task WriteToStreamInternalAsync(object value, Stream writeStream)//, HttpContent content, TransportContext transportContext
	    {
            if (writeStream == null)
            {
                throw new ArgumentNullException(nameof(writeStream));
            }

            if (value == null)
            {
                // Cannot serialize null at the top level.  Json.Net throws Newtonsoft.Json.JsonWriterException : Error
                // writing Null value. BSON must start with an Object or Array. Path ''.  Fortunately
                // BaseJsonMediaTypeFormatter.ReadFromStream(Type, Stream, HttpContent, IFormatterLogger) treats zero-
                // length content as null or the default value of a struct.
                return Task.CompletedTask;
            }

            // See comments in ReadFromStream() above about this special case and the need to include byte[] in it.
            // Using runtime type here because Json.Net will throw during serialization whenever it cannot handle the
            // runtime type at the top level. For e.g. passed type may be typeof(object) and value may be a string.
            var runtimeType = value.GetType();
            if (IsSimpleType(runtimeType))
            {
				// Wrap value in a Dictionary with a single property named "Value" to provide BSON with an Object.
	            // Is written out as binary equivalent of [ "Value": value ] JSON.
                var temporaryDictionary = new Dictionary<string, object> { { "Value", value } };
	            WriteToStreamInternal(temporaryDictionary, writeStream);
			}
			else
            {

				WriteToStreamInternal(value, writeStream);
            }

		    return Task.CompletedTask;
	    }

	    private void WriteToStreamInternal(object value, Stream writeStream)
	    {
            if (writeStream == null) throw new ArgumentNullException(nameof(writeStream));

            using var writer = new BsonDataWriter(writeStream) {CloseOutput = false};

            _serializer.Serialize(writer, value);
		    writer.Flush();
	    }

        // Return true if Json.Net will likely convert value of given type to a Json primitive, not JsonArray nor
        // JsonObject.
        // To do: https://aspnetwebstack.codeplex.com/workitem/1467
        private static bool IsSimpleType(Type type)
        {
            var isSimpleType = type.GetTypeInfo().IsValueType || type == typeof(string) || type == typeof(byte[]);

            return isSimpleType;
        }
    }
}
