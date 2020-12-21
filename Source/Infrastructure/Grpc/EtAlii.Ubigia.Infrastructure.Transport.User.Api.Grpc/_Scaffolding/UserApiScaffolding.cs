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
			container.Register(() => _infrastructure.Storages);
			container.Register(() => _infrastructure.Accounts);
			container.Register(() => _infrastructure.Spaces);
			container.Register(() => _infrastructure.Roots);
			container.Register(() => _infrastructure.Entries);
			container.Register(() => _infrastructure.Properties);
			container.Register(() => _infrastructure.Content);
			container.Register(() => _infrastructure.ContentDefinition);
			container.Register(() => _infrastructure.Configuration);
            container.Register(() => _infrastructure.ContextCorrelator);
        }
	}
}
