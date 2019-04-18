//namespace EtAlii.Ubigia.Infrastructure.Transport.Grpc
//{
//	using EtAlii.Ubigia.Api.Transport
//	using Microsoft.Extensions.DependencyInjection
//	using Newtonsoft.Json

//	public static partial class ServiceCollectionExtensions
//	{
//	    public static IServiceCollection AddInfrastructureSerialization(this IServiceCollection services)
//		{
//			var serializer = new SerializerFactory().Create()

//			// We need to use our in-house serialization. This to ensure that dictionaries, ulongs and floats are serialized correctly.
//			services
//				.AddSingleton<ISerializer>(serializer)
//				.AddSingleton<JsonSerializer>((JsonSerializer) serializer)
//				//.AddSingleton<IParameterResolver, GrpcParameterResolver>()

//			return services

//	    }
//	}
//}