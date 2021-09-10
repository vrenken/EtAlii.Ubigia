// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Diagnostics
{
    using System.Threading.Tasks;
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

        public static async Task<LogicalOptions> UseDiagnostics(this Task<LogicalOptions> optionsTask)
        {
            var options = await optionsTask.ConfigureAwait(false);

            return options.UseDiagnostics();
        }
    }
}
