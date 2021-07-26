// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class DiagnosticsGraphPathTraverserExtension : IGraphPathTraverserExtension
    {
        private readonly DiagnosticsConfigurationSection _configuration;

        internal DiagnosticsGraphPathTraverserExtension(IConfigurationRoot configurationRoot)
        {
            _configuration = new DiagnosticsConfigurationSection();
            configurationRoot.Bind("Api:Logical:Diagnostics", _configuration);
        }

        public void Initialize(Container container)
        {
            if (_configuration.InjectLogging)
            {
                // Do stuff...
            }
        }
    }
}
