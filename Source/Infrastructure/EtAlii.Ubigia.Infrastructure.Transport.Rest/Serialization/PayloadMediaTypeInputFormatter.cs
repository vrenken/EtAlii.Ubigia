// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.Rest
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.IO;
	using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Serialization;
    using Microsoft.AspNetCore.Http.Features;
	using Microsoft.AspNetCore.Mvc.Formatters;
	using Microsoft.Net.Http.Headers;
    using Newtonsoft.Json.Bson;

	public class PayloadMediaTypeInputFormatter : InputFormatter
	{
        private static readonly Type _openDictionaryType = typeof(Dictionary<,>);
        private static readonly TypeInfo _enumerableTypeInfo = typeof(IEnumerable).GetTypeInfo();
        private static readonly TypeInfo _dictionaryTypeInfo = typeof(IDictionary).GetTypeInfo();
        private static readonly MediaTypeHeaderValue _mediaType = new("application/bson");
		private readonly ISerializer _serializer;

		public PayloadMediaTypeInputFormatter()
		{
		    // Set default supported media type
		    SupportedMediaTypes.Add(_mediaType);
			_serializer = Serializer.Default;
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
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (readStream == null)
            {
                throw new ArgumentNullException(nameof(readStream));
            }

            using var innerReader = new BinaryReader(readStream, Encoding.UTF8);
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
