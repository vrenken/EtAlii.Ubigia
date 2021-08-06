// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

// ReSharper disable once CheckNamespace
namespace EtAlii.Ubigia.Infrastructure.Fabric.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class FabricContextDiagnosticsExtension : IFabricContextExtension
    {
        public void Initialize(Container container)
        {
            var configurationRoot = container.GetInstance<IConfigurationRoot>();
            var options = configurationRoot
                .GetSection("Infrastructure:Fabric:Diagnostics")
                .Get<DiagnosticsOptions>();

            var scaffoldings = new IScaffolding[]
            {
                new FabricContextLoggingScaffolding(options),
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }
        }
    }
}
