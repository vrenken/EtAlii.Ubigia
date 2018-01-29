namespace EtAlii.Ubigia.Infrastructure.Transport.AspNetCore
{
	using System;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.DependencyInjection.Extensions;

	public static partial class ServiceCollectionExtensions
	{
		public static IServiceCollection TryAddSingleton<TService>(this IServiceCollection collection, TService instance) 
			where TService : class
		{
			if (collection == null)
				throw new ArgumentNullException(nameof(collection));
			if ((object)instance == null)
				throw new ArgumentNullException(nameof(instance));
			ServiceDescriptor descriptor = ServiceDescriptor.Singleton(typeof(TService), (object)instance);
			collection.TryAdd(descriptor);
			return collection;
		}
	}
}