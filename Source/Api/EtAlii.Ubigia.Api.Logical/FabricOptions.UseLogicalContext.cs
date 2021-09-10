// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Fabric;

    public static class FabricOptionsUseLogicalContextExtension
    {
        public static LogicalOptions UseLogicalContext(this FabricOptions fabricOptions)
        {
            return new LogicalOptions(fabricOptions.ConfigurationRoot)
                .UseFabricOptions(fabricOptions);
        }
        public static async Task<LogicalOptions> UseLogicalContext(this Task<FabricOptions> fabricOptionsTask)
        {
            var fabricOptions = await fabricOptionsTask.ConfigureAwait(false);
            return new LogicalOptions(fabricOptions.ConfigurationRoot)
                .UseFabricOptions(fabricOptions);
        }
    }
}
