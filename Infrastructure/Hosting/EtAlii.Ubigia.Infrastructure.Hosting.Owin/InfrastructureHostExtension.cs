namespace EtAlii.Ubigia.Infrastructure.Hosting
{
	using EtAlii.xTechnology.MicroContainer;
	using EtAlii.Ubigia.Infrastructure.Functional;
	using EtAlii.Ubigia.Infrastructure.Transport;
	using EtAlii.Ubigia.Storage;
	using xTechnology.Hosting;

	public class InfrastructureHostExtension : IHostExtension
	{
		private readonly IStorage _storage;
		private readonly IInfrastructure _infrastructure;

		public InfrastructureHostExtension(IStorage storage, IInfrastructure infrastructure)
		{
			_storage = storage;
			_infrastructure = infrastructure;
		}

		public void Register(Container container)
		{
			container.Register(() => _storage);
			container.Register(() => _infrastructure);
			container.Register<IStorageService, StorageService>();
			container.Register<IInfrastructureService, InfrastructureService>();
		}
	}
}
