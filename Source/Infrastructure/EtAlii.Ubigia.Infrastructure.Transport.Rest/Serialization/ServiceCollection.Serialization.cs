﻿namespace EtAlii.Ubigia.Infrastructure.Transport.Rest
{
    using EtAlii.Ubigia.Serialization;
    using Microsoft.Extensions.DependencyInjection;
	
	public static partial class ServiceCollectionExtensions
	{
		public static IMvcBuilder AddInfrastructureSerialization(this IMvcBuilder mvcBuilder)
		{
            mvcBuilder.Services.AddInfrastructureSerialization();
			
			// We need to use our in-house serialization. This to ensure that dictionaries, ulongs and floats are serialized correctly.
			return mvcBuilder.AddNewtonsoftJson(options => SerializerFactory.Configure(options.SerializerSettings));
		}
	}
}