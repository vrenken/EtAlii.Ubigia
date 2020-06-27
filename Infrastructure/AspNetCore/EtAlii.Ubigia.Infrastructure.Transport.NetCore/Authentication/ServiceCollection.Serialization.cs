namespace EtAlii.Ubigia.Infrastructure.Transport.NetCore
{
	using EtAlii.Ubigia.Api.Transport;
	using Microsoft.Extensions.DependencyInjection;
	using Newtonsoft.Json;

	public static partial class ServiceCollectionExtensions
	{
		public static IServiceCollection AddInfrastructureSerialization(this IServiceCollection services)
		{
			var serializer = new SerializerFactory().Create();

			// We need to use our in-house serialization. This to ensure that dictionaries, ulongs and floats are serialized correctly.
			services
				.AddSingleton(serializer)
				.AddSingleton((JsonSerializer) serializer);

			return services;
		}
		
		public static IMvcBuilder AddInfrastructureSerialization(this IMvcBuilder mvcBuilder)
		{
			AddInfrastructureSerialization(mvcBuilder.Services);
			
			// We need to use our in-house serialization. This to ensure that dictionaries, ulongs and floats are serialized correctly.
			return mvcBuilder.AddNewtonsoftJson(options => SerializerFactory.Configure(options.SerializerSettings));
		}
	}
}