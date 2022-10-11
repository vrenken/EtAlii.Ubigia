// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using EtAlii.Ubigia.Serialization;
    using Microsoft.Extensions.DependencyInjection;
	using Newtonsoft.Json;

	public static partial class ServiceCollectionExtensions
	{
		public static IServiceCollection AddInfrastructureSerialization(this IServiceCollection services)
		{
			var serializer = Serializer.Default;

			// We need to use our in-house serialization. This to ensure that dictionaries, ulongs and floats are serialized correctly.
			services
				.AddSingleton(serializer)
				.AddSingleton((JsonSerializer) serializer);

			return services;
		}
	}
}
