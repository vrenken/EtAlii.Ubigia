// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Diagnostics
{
    using EtAlii.xTechnology.MicroContainer;

    public static class LogicalOptionsUseDiagnosticsExtension
    {
        public static LogicalOptions UseDiagnostics(this LogicalOptions options)
        {
            var extensions = new IExtension[]
            {
                new DiagnosticsGraphPathTraverserExtension(options.ConfigurationRoot),
                new DiagnosticsLogicalContextExtension(options.ConfigurationRoot),
            };

            options = options.Use(extensions);

            return options;
        }
    }
}
