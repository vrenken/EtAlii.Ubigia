// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class DiagnosticsLogicalContextExtension : ILogicalContextExtension
    {
        private readonly IConfigurationRoot _configurationRoot;

        public DiagnosticsLogicalContextExtension(IConfigurationRoot configurationRoot)
        {
            _configurationRoot = configurationRoot;
        }


        public void Initialize(IRegisterOnlyContainer container)
        {
            var options = _configurationRoot
                .GetSection("Api:Logical:Diagnostics")
                .Get<DiagnosticsOptions>();

            if (options.InjectLogging)
            {
                // Doesn't this pattern break with the general scaffolding principles?
                // More details can be found in the GitHub issue below:
                // https://github.com/vrenken/EtAlii.Ubigia/issues/88
                container.RegisterDecorator(typeof(ILogicalRootSet), typeof(LoggingLogicalRootSet));
            }
        }
    }
}
