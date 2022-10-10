// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using EtAlii.xTechnology.MicroContainer;

    internal class CommonInfrastructureExtension : IExtension
    {
        private readonly InfrastructureOptions _options;

        public CommonInfrastructureExtension(InfrastructureOptions options)
        {
            _options = options;
        }

        public void Initialize(IRegisterOnlyContainer container)
        {
            new InfrastructureScaffolding(_options).Register(container);
            new DataScaffolding().Register(container);
            new ManagementScaffolding().Register(container);
        }
    }
}
