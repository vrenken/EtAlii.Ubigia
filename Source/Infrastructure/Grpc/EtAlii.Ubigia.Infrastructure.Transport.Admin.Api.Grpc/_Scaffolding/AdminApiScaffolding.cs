﻿namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.Grpc
{
	using EtAlii.Ubigia.Infrastructure.Functional;
	using EtAlii.xTechnology.MicroContainer;
    using EtAlii.xTechnology.Threading;

	public class AdminApiScaffolding : IScaffolding
	{
		private readonly IInfrastructure _infrastructure;

		public AdminApiScaffolding(IInfrastructure infrastructure)
		{
			_infrastructure = infrastructure;
		}

		public void Register(Container container)
		{
			container.Register(() => _infrastructure.Accounts);
			container.Register(() => _infrastructure.Storages);
			container.Register(() => _infrastructure.Spaces);
			container.Register(() => _infrastructure.Configuration);
            container.Register<IContextCorrelator, ContextCorrelator>();
		}
	}
}
