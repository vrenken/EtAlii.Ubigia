// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
    using EtAlii.xTechnology.MicroContainer;

    public class CommonSpaceConnectionExtension : IExtension
    {
        private readonly SpaceConnectionOptions _options;

        public CommonSpaceConnectionExtension(SpaceConnectionOptions options)
        {
            _options = options;
        }

        public void Initialize(IRegisterOnlyContainer container)
        {
            new SpaceConnectionScaffolding(_options).Register(container);
        }
    }
}
