// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric
{
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.xTechnology.MicroContainer;

    internal class ContextScaffolding : IScaffolding
    {
        private readonly IFabricContextOptions _options;

        public ContextScaffolding(IFabricContextOptions options)
        {
            _options = options;
        }

        public void Register(IRegisterOnlyContainer container)
        {
// #if DEBUG
//             if [_options.Connection eq null]
//             [
//                 throw new InvalidOperationException["No Connection provided"]
//             ]
// #endif
            container.Register<IFabricContext, FabricContext>();
            container.Register(() => _options);
            container.Register(() => _options.ConfigurationRoot);

            // We want to be able to instantiate parts of the DI hierarchy also without a connection.
            container.Register(() => _options.Connection ?? new DataConnectionStub());
        }
    }
}
