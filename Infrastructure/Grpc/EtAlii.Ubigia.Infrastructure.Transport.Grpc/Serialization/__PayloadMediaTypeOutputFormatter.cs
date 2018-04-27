//namespace EtAlii.Ubigia.Infrastructure.Transport.Grpc
//{
//	using System;
//    using System.Collections;
//    using System.Collections.Generic;
//    using System.IO;
//    using System.Reflection;
//    using System.Text;
//    using System.Threading.Tasks;
//	using EtAlii.Ubigia.Api.Transport;
//	using Microsoft.Grpc.Mvc.Formatters;
//    using Microsoft.Net.Http.Headers;
//    using Newtonsoft.Json;
//    using Newtonsoft.Json.Bson;

//    public class PayloadMediaTypeOutputFormatter : OutputFormatter
//	{
//        public static readonly MediaTypeHeaderValue MediaType = new MediaTypeHeaderValue("application/bson");
//		private readonly ISerializer _serializer;

//		public PayloadMediaTypeOutputFormatter()
//		{
//		    // Set default supported media type
//		    SupportedMediaTypes.Add(MediaType);
//			_serializer = new SerializerFactory().Create();
//		}

//		protected override bool CanWriteType(Type type)
//		{
//			return true;
//		}

//		public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context)
//		{
//			var response = context.HttpContext.Response;
//			WriteToStream(context.ObjectType, context.Object, response.Body);
//			await Task.CompletedTask;
//		}

//		public void WriteToStream(Type type, object value, Stream writeStream)
//        {
//            if (writeStream == null)
//            {
//                throw new ArgumentNullException(nameof(writeStream));
//            }

//            if (value == null)
//            {
//                // Cannot serialize null at the top level.  Json.Net throws Newtonsoft.Json.JsonWriterException : Error
//                // writing Null value. BSON must start with an Object or Array. Path ''.  Fortunately
//                // BaseJsonMediaTypeFormatter.ReadFromStream(Type, Stream, HttpContent, IFormatterLogger) treats zero-
//                // length content as null or the default value of a struct.
//                return;
//            }

//            // See comments in ReadFromStream() above about this special case and the need to include byte[] in it.
//            // Using runtime type here because Json.Net will throw during serialization whenever it cannot handle the
//            // runtime type at the top level. For e.g. passed type may be typeof(object) and value may be a string.
//            var runtimeType = value.GetType();
//            if (IsSimpleType(runtimeType) || runtimeType == typeof(byte[]))
//            {
//                // Wrap value in a Dictionary with a single property named "Value" to provide BSON with an Object.  Is
//                // written out as binary equivalent of { "Value": value } JSON.
//                var temporaryDictionary = new Dictionary<string, object>
//                {
//                    { "Value", value },
//                };
//	            WriteToStreamInternal(typeof(Dictionary<string, object>), temporaryDictionary, writeStream);
//			}
//			else
//            {

//				WriteToStreamInternal(type, value, writeStream);
//            }
//        }

//	    private void WriteToStreamInternal(Type type, object value, Stream writeStream)
//	    {
//			using (var writer = CreateJsonWriter(type, writeStream))
//		    {
//			    writer.CloseOutput = false;
//			    _serializer.Serialize(writer, value);
//			    writer.Flush();
//		    };
//		}

//		public JsonWriter CreateJsonWriter(Type type, Stream writeStream)
//        {
//            if (type == null)
//            {
//                throw new ArgumentNullException(nameof(type));
//            }

//            if (writeStream == null)
//            {
//                throw new ArgumentNullException(nameof(writeStream));
//            }

//            return new BsonDataWriter(new BinaryWriter(writeStream));
//        }

//        // Return true if Json.Net will likely convert value of given type to a Json primitive, not JsonArray nor
//        // JsonObject.
//        // To do: https://aspnetwebstack.codeplex.com/workitem/1467
//        private static bool IsSimpleType(Type type)
//        {
//            var isSimpleType = type.GetTypeInfo().IsValueType || type == typeof(string);

//            return isSimpleType;
//        }
//	}
//}