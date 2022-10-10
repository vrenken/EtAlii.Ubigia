// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

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

        /// <inheritdoc />
		public void Register(IRegisterOnlyContainer container)
		{
            container.Register(() => _infrastructure);
			container.Register(() => _infrastructure.Accounts);
			container.Register(() => _infrastructure.Storages);
			container.Register(() => _infrastructure.Spaces);
            container.Register(() => _infrastructure.ContextCorrelator);
		}
	}
}
