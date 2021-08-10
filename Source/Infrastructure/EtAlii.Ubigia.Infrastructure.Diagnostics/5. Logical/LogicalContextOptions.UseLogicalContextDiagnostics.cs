// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Diagnostics
{
    using EtAlii.Ubigia.Infrastructure.Logical;
    using EtAlii.xTechnology.MicroContainer;

    public static class LogicalContextOptionsUseLogicalContextDiagnostics
    {
        public static LogicalContextOptions UseLogicalContextDiagnostics(this LogicalContextOptions options)
        {
            var extensions = new IExtension[]
            {
                new DiagnosticsLogicalContextExtension(options.ConfigurationRoot),
            };

            return options.Use(extensions);
        }
    }
}
