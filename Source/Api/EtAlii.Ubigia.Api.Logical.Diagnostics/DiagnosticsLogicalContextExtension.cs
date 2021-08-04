// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class DiagnosticsLogicalContextExtension : ILogicalContextExtension
    {
        public void Initialize(Container container)
        {
            var configurationRoot = container.GetInstance<IConfiguration>();
            var configuration = configurationRoot
                .GetSection("Api:Logical:Diagnostics")
                .Get<DiagnosticsConfigurationSection>();

            if (configuration.InjectLogging)
            {
                // Doesn't this pattern break with the general scaffolding principles?
                // More details can be found in the GitHub issue below:
                // https://github.com/vrenken/EtAlii.Ubigia/issues/88
                container.RegisterDecorator(typeof(ILogicalRootSet), typeof(LoggingLogicalRootSet));
            }
        }
    }
}
