// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class LoggingFabricContextExtension : IFabricContextExtension
    {
        public void Initialize(Container container)
        {
            var configurationRoot = container.GetInstance<IConfiguration>();
            var options = configurationRoot
                .GetSection("Api:Fabric:Diagnostics")
                .Get<DiagnosticsOptions>();

            if (options.InjectLogging)
            {
                container.RegisterDecorator(typeof(IEntryContext), typeof(LoggingEntryContext));
            }
        }
    }
}
