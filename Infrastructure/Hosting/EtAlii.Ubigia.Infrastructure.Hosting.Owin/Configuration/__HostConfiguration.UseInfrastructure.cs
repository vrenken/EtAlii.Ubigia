//namespace EtAlii.Ubigia.Infrastructure.Hosting
//{
//	using System;
//	using EtAlii.xTechnology.Hosting;
//	using EtAlii.Ubigia.Infrastructure.Functional;
//	using EtAlii.Ubigia.Storage;

//	public static class HostConfigurationInfrastructureExtension
//	{
//		public static IHostConfiguration UseInfrastructure(this IHostConfiguration configuration, IStorage storage, IInfrastructure infrastructure)
//		{
//			if (infrastructure == null)
//			{
//				throw new NotSupportedException("A Infrastructure is required to construct a Host instance");
//			}
//			if (storage == null)
//			{
//				throw new NotSupportedException("A Storage is required to construct a Host instance");
//			}

//			configuration.Use(new InfrastructureHostExtension(storage, infrastructure));

//			var services = new Type[]
//			{
//				typeof(IStorageService),
//				typeof(IInfrastructureService),
//			};
//			configuration.Use(services);

//			return configuration;
//		}
//	}
//}