// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.Rest
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Reflection;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Serialization;
    using Microsoft.AspNetCore.Http.Features;
	using Microsoft.AspNetCore.Mvc.Formatters;
	using Microsoft.Net.Http.Headers;
    using Newtonsoft.Json.Bson;

	public class PayloadMediaTypeOutputFormatter : OutputFormatter
	{
        private static readonly MediaTypeHeaderValue _mediaType = new("application/bson");
		private readonly ISerializer _serializer;

		public PayloadMediaTypeOutputFormatter()
		{
		    // Set default supported media type
		    SupportedMediaTypes.Add(_mediaType);
			_serializer = new SerializerFactory().Create();
		}

		protected override bool CanWriteType(Type type)
		{
			return true;
		}

		public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context)
		{
			// Fix for: https://github.com/aspnet/AspNetCore/issues/7644
			var bodyControlFeature = context.HttpContext.Features.Get<IHttpBodyControlFeature>();
			if (bodyControlFeature != null)
			{
				bodyControlFeature.AllowSynchronousIO = true;
			}

			var response = context.HttpContext.Response;
			await WriteToStream(context.ObjectType, context.Object, response.Body).ConfigureAwait(false);
		}

        private async Task WriteToStream(Type type, object value, Stream writeStream)
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
                return;
            }

            // See comments in ReadFromStream() above about this special case and the need to include byte[] in it.
            // Using runtime type here because Json.Net will throw during serialization whenever it cannot handle the
            // runtime type at the top level. For e.g. passed type may be typeof(object) and value may be a string.
            var runtimeType = type;
            if (IsSimpleType(runtimeType) || runtimeType == typeof(byte[]))
            {
                // Wrap value in a Dictionary with a single property named "Value" to provide BSON with an Object.  Is
                // written out as binary equivalent of [ "Value": value ] JSON.
                var temporaryDictionary = new Dictionary<string, object>
                {
                    { "Value", value },
                };
                await WriteToStreamInternal(temporaryDictionary, writeStream).ConfigureAwait(false);
			}
			else
            {
                if (type == null)
                {
                    throw new ArgumentNullException(nameof(type));
                }
				await WriteToStreamInternal(value, writeStream).ConfigureAwait(false);
            }
        }

        private async Task WriteToStreamInternal(object value, Stream writeStream)
	    {
            if (writeStream == null)
            {
                throw new ArgumentNullException(nameof(writeStream));
            }

            using var writer = new BsonDataWriter(writeStream) { CloseOutput = false };

            _serializer.Serialize(writer, value);
			await writer.FlushAsync().ConfigureAwait(false);
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
