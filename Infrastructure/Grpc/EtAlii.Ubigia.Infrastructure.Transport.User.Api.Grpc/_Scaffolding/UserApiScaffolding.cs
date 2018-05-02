namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Grpc
{
	using EtAlii.Ubigia.Infrastructure.Functional;
	using EtAlii.xTechnology.MicroContainer;

	public class UserApiScaffolding : IScaffolding
	{
		private readonly IInfrastructure _infrastructure;

		public UserApiScaffolding(IInfrastructure infrastructure)
		{
			_infrastructure = infrastructure;
		}

		public void Register(Container container)
		{
			container.Register<IStorageRepository>(() => _infrastructure.Storages);
			container.Register<IAccountRepository>(() => _infrastructure.Accounts);
			container.Register<ISpaceRepository>(() => _infrastructure.Spaces);
			container.Register<IRootRepository>(() => _infrastructure.Roots);
			container.Register<IEntryRepository>(() => _infrastructure.Entries);
			container.Register<IPropertiesRepository>(() => _infrastructure.Properties);
			container.Register<IContentRepository>(() => _infrastructure.Content);
			container.Register<IContentDefinitionRepository>(() => _infrastructure.ContentDefinition);
			container.Register<IInfrastructureConfiguration>(() => _infrastructure.Configuration);
		}
	}
}