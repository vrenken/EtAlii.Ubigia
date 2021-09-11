// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric.Diagnostics
{
    using System.Threading.Tasks;
    using EtAlii.xTechnology.MicroContainer;

    public static class FabricOptionsDiagnosticsExtension
    {
        public static FabricOptions UseDiagnostics(this FabricOptions options)
        {
            var extensions = new IExtension[]
            {
                new LoggingFabricContextExtension(options.ConfigurationRoot),
            };
            return options.Use(extensions);
        }

        public static async Task<FabricOptions> UseDiagnostics(this Task<FabricOptions> optionsTask)
        {
            var options = await optionsTask.ConfigureAwait(false);

            return options.UseDiagnostics();
        }
    }
}
