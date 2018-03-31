namespace EtAlii.Ubigia.Infrastructure.Transport.AspNetCore
{
	using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
	using EtAlii.Ubigia.Api.Transport;
	using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.Net.Http.Headers;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Bson;

    public class PayloadMediaTypeOutputFormatter : OutputFormatter//: BaseJsonMediaTypeFormatter
	{
        public static readonly MediaTypeHeaderValue MediaType = new MediaTypeHeaderValue("application/bson");

		///// <summary>
		///// Initializes a new instance of the <see cref="BsonMediaTypeFormatter"/> class.
		///// </summary>
		public PayloadMediaTypeOutputFormatter()
		{
		    // Set default supported media type
		    SupportedMediaTypes.Add(MediaType);

		//    SerializerFactory.AddDefaultConverters(SerializerSettings.Converters);
		}

		///// <summary>
		///// Initializes a new instance of the <see cref="BsonMediaTypeFormatter"/> class.
		///// </summary>
		///// <param name="formatter">The <see cref="BsonMediaTypeFormatter"/> instance to copy settings from.</param>
		//protected PayloadMediaTypeFormatter(BsonMediaTypeFormatter formatter)
		//    : base(formatter)
		//{
		//}

		/// <inheritdoc />

		protected override bool CanWriteType(Type type)
		{
			return true;
			//if (typeof(Contact).IsAssignableFrom(type) || typeof(IEnumerable<Contact>).IsAssignableFrom(type))
			//{
			//	return base.CanWriteType(type);
			//}
			//return false;
		}

		public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context)
		{
			var response = context.HttpContext.Response;

			//var stream = new MemoryStream();
			WriteToStream(context.ObjectType, context.Object, response.Body);//stream);//, Encoding.UTF8);
			//response.Body = stream;

			await Task.CompletedTask;
		}

		public void WriteToStream(Type type, object value, Stream writeStream)//, Encoding effectiveEncoding)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (writeStream == null)
            {
                throw new ArgumentNullException(nameof(writeStream));
            }

            //if (effectiveEncoding == null)
            //{
            //    throw new ArgumentNullException(nameof(effectiveEncoding));
            //}

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
	            WriteToStreamInternal(typeof(Dictionary<string, object>), temporaryDictionary, writeStream);//, effectiveEncoding);
			}
			else
            {

				WriteToStreamInternal(type, value, writeStream);//, effectiveEncoding);
            }
        }

	    private void WriteToStreamInternal(Type type, object value, Stream writeStream)//, Encoding effectiveEncoding)
	    {
			using (var writer = CreateJsonWriter(type, writeStream))//, effectiveEncoding))
		    {
			    writer.CloseOutput = false;
			    var serializer = new SerializerFactory().Create();
			    //var serializer = JsonSerializer.CreateDefault();
			    serializer.Serialize(writer, value);
			    writer.Flush();
		    };
		}

		/// <inheritdoc />
		public JsonWriter CreateJsonWriter(Type type, Stream writeStream)//, Encoding effectiveEncoding)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (writeStream == null)
            {
                throw new ArgumentNullException(nameof(writeStream));
            }

            //if (effectiveEncoding == null)
            //{
            //    throw new ArgumentNullException(nameof(effectiveEncoding));
            //}

            return new BsonDataWriter(new BinaryWriter(writeStream));//, effectiveEncoding));
        }

        // Return true if Json.Net will likely convert value of given type to a Json primitive, not JsonArray nor
        // JsonObject.
        // To do: https://aspnetwebstack.codeplex.com/workitem/1467
        private static bool IsSimpleType(Type type)
        {
            // Cannot happen.
            // Contract.Assert(type != null);

            var isSimpleType = type.GetTypeInfo().IsValueType || type == typeof(string);

            return isSimpleType;
        }
	}
}