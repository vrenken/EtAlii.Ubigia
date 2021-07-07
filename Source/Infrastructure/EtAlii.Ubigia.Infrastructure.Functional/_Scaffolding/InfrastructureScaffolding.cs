// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using EtAlii.xTechnology.MicroContainer;
    using EtAlii.xTechnology.Threading;

    internal class InfrastructureScaffolding : IScaffolding
    {
        private readonly IInfrastructureConfiguration _configuration;

        public InfrastructureScaffolding(IInfrastructureConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Register(Container container)
        {
            container.Register<IContextCorrelator, ContextCorrelator>();

            container.Register(() => _configuration);
            container.Register(() => _configuration.Root);
            container.Register(() => _configuration.Logical);
            container.Register(() => _configuration.SystemConnectionCreationProxy);
        }
    }
}
