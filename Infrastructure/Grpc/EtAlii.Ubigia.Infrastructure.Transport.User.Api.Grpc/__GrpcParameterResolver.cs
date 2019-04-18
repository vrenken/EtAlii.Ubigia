//namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Grpc
//{
//    using System.IO
//    using System.Reflection
//    using EtAlii.Ubigia.Api.Transport
//    using Microsoft.Grpc.Hubs
//    using Microsoft.Grpc.Json

//    internal class GrpcParameterResolver : DefaultParameterResolver
//    {
//        private readonly Serializer _serializer

//        private readonly System.Reflection.FieldInfo _valueFieldInfo

//        public GrpcParameterResolver(ISerializer serializer)
//        {
//            _serializer = (Serializer)serializer

//            _valueFieldInfo = typeof(IJsonValue).Assembly.GetType("Microsoft.AspNet.Grpc.Json.JRawValue")
//                .GetTypeInfo()
//                .GetDeclaredField("_value")
//        }

//        public override object ResolveParameter(ParameterDescriptor descriptor, IJsonValue value)
//        {
//            var pureJson = (string)_valueFieldInfo.GetValue(value)
//            using (var reader = new StringReader(pureJson))
//            {
//                var result = _serializer.Deserialize(reader, descriptor.ParameterType)
//                return result
//            }
//        }
//    }
//}