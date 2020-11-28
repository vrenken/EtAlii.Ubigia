﻿namespace EtAlii.Ubigia.Infrastructure.Transport.NetCore
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.IO;
	using System.Reflection;
	using System.Threading.Tasks;
    using EtAlii.Ubigia.Serialization;
    using Microsoft.AspNetCore.Http.Features;
	using Microsoft.AspNetCore.Mvc.Formatters;
	using Microsoft.Net.Http.Headers;
	using Newtonsoft.Json;
	using Newtonsoft.Json.Bson;

	public class PayloadMediaTypeInputFormatter : InputFormatter
	{
        private static readonly Type OpenDictionaryType = typeof(Dictionary<,>);
        private static readonly TypeInfo EnumerableTypeInfo = typeof(IEnumerable).GetTypeInfo();
        private static readonly TypeInfo DictionaryTypeInfo = typeof(IDictionary).GetTypeInfo();

        // ReSharper disable once InconsistentNaming
        public static readonly MediaTypeHeaderValue MediaType = new MediaTypeHeaderValue("application/bson");
		private readonly ISerializer _serializer;

		public PayloadMediaTypeInputFormatter()
		{
		    // Set default supported media type
		    SupportedMediaTypes.Add(MediaType);
			_serializer = new SerializerFactory().Create();
		}


		protected override bool CanReadType(Type type)
		{
			return true;
		}

		public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context)
		{
			// Fix for: https://github.com/aspnet/AspNetCore/issues/7644
			var bodyControlFeature = context.HttpContext.Features.Get<IHttpBodyControlFeature>();
			if (bodyControlFeature != null)
			{
				bodyControlFeature.AllowSynchronousIO = true;
			}

			var request = context.HttpContext.Request;
			var result = ReadFromStream(context.ModelType, request.Body);
			return await InputFormatterResult.SuccessAsync(result).ConfigureAwait(false);
		}

		private object ReadFromStream(Type type, Stream readStream)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (readStream == null) throw new ArgumentNullException(nameof(readStream));

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
            if (!IsSimpleType(type) && type != typeof(byte[])) return ReadFromStreamInternal(type, readStream);
            
            // Read as exact expected Dictionary<string, T> to ensure NewtonSoft.Json does correct top-level conversion.
            var dictionaryType = OpenDictionaryType.MakeGenericType(typeof(string), type);
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
                if (dictionary.Count == 1 && item.Key as string == "Value")
	            {
		            // Success
		            return item.Value;
	            }

                firstKey = item.Key.ToString();
// S1751 = Loops with at most one iteration should be refactored
// However I see no easy, simple way to refactor this into a form without a break - It's a non-generic dictionary which
// has way less ways to access its contents compared to its modern generic brother.
#pragma warning disable S1751                 
                break;
#pragma warning restore S1751                
            }

            var e2 = new InvalidOperationException("Unexpected Data");
            e2.Data.Add("dictionary.Count", dictionary.Count);
            e2.Data.Add("firstKey", firstKey);
            throw e2;

        }

	    private object ReadFromStreamInternal(Type type, Stream readStream)
        {
            using var reader = CreateJsonReader(type, readStream);
            reader.CloseInput = false;
            return _serializer.Deserialize(reader, type);
        }

	    private JsonReader CreateJsonReader(Type type, Stream readStream)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (readStream == null)
            {
                throw new ArgumentNullException(nameof(readStream));
            }

            var reader = new BsonDataReader(new BinaryReader(readStream));

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

        // Return true if Json.Net will likely convert value of given type to a Json primitive, not JsonArray nor
        // JsonObject.
        // To do: https://aspnetwebstack.codeplex.com/workitem/1467
        private static bool IsSimpleType(Type type)
        {
            var isSimpleType = type.GetTypeInfo().IsValueType || type == typeof(string);

            return isSimpleType;
        }
	}
}