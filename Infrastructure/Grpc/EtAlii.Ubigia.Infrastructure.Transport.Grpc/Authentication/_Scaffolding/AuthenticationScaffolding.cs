namespace EtAlii.Ubigia.Infrastructure.Transport.Grpc
{
	using EtAlii.Ubigia.Infrastructure.Functional;
	using EtAlii.xTechnology.MicroContainer;

	public class AuthenticationScaffolding : IScaffolding
	{
		private readonly IInfrastructure _infrastructure;

		public AuthenticationScaffolding(IInfrastructure infrastructure)
		{
			_infrastructure = infrastructure;
		}

		public void Register(Container container)
		{
			container.Register<IAccountRepository>(() => _infrastructure.Accounts);
			container.Register<IStorageRepository>(() => _infrastructure.Storages);
			container.Register<IInfrastructureConfiguration>(() => _infrastructure.Configuration);
			container.Register<IAuthenticationTokenConverter, AuthenticationTokenConverter>();
			container.Register<ISimpleAuthenticationVerifier, SimpleAuthenticationVerifier>();
			container.Register<ISimpleAuthenticationBuilder, SimpleAuthenticationBuilder>();
			container.Register<ISimpleAuthenticationTokenVerifier, SimpleAuthenticationTokenVerifier>();
		}
	}
}