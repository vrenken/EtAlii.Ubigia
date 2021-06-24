// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport
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
			if (instance == null)
				throw new ArgumentNullException(nameof(instance));
			var descriptor = ServiceDescriptor.Singleton(typeof(TService), instance);
			collection.TryAdd(descriptor);
			return collection;
		}
	}
}