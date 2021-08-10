// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

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

        /// <inheritdoc />
		public void Register(IRegisterOnlyContainer container)
		{
			container.Register(() => _infrastructure.Storages);
			container.Register(() => _infrastructure.Accounts);
			container.Register(() => _infrastructure.Spaces);
			container.Register(() => _infrastructure.Roots);
			container.Register(() => _infrastructure.Entries);
			container.Register(() => _infrastructure.Properties);
			container.Register(() => _infrastructure.Content);
			container.Register(() => _infrastructure.ContentDefinition);
			container.Register(() => _infrastructure.Options);
            container.Register(() => _infrastructure.ContextCorrelator);
        }
	}
}
