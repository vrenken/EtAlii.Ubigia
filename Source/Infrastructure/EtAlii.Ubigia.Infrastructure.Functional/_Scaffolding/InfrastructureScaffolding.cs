// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using EtAlii.xTechnology.MicroContainer;
    using EtAlii.xTechnology.Threading;

    internal class InfrastructureScaffolding : IScaffolding
    {
        private readonly IInfrastructureOptions _options;

        public InfrastructureScaffolding(IInfrastructureOptions options)
        {
            _options = options;
        }

        /// <inheritdoc />
        public void Register(Container container)
        {
            container.Register<IContextCorrelator, ContextCorrelator>();

            container.Register(() => _options);
            container.Register(() => _options.ConfigurationRoot);
            container.Register(() => _options.Logical);
            container.Register(() => _options.SystemConnectionCreationProxy);
        }
    }
}
