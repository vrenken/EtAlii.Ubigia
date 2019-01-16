namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.Grpc
{
	using EtAlii.Ubigia.Infrastructure.Functional;
	using EtAlii.xTechnology.MicroContainer;

	public class AdminApiScaffolding : IScaffolding
	{
		private readonly IInfrastructure _infrastructure;

		public AdminApiScaffolding(IInfrastructure infrastructure)
		{
			_infrastructure = infrastructure;
		}

		public void Register(Container container)
		{
			container.Register<IAccountRepository>(() => _infrastructure.Accounts);
			container.Register<IStorageRepository>(() => _infrastructure.Storages);
			container.Register<ISpaceRepository>(() => _infrastructure.Spaces);
			container.Register<IInfrastructureConfiguration>(() => _infrastructure.Configuration);
		}
	}
}