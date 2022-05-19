// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;

    public static class DataConnectionUseFabricOptionsExtension
    {
        public static FabricOptions UseFabricContext(this DataConnectionOptions dataConnectionOptions)
        {
            var fabricOptions = new FabricOptions(dataConnectionOptions.ConfigurationRoot)
                .Use(dataConnectionOptions);
            return fabricOptions;
        }

        public static async Task<FabricOptions> UseFabricContext(this Task<(IDataConnection, DataConnectionOptions)> dataConnectionOptionsTask)
        {
            var (_, dataConnectionOptions) = await dataConnectionOptionsTask.ConfigureAwait(false);

            var fabricOptions = new FabricOptions(dataConnectionOptions.ConfigurationRoot)
                .Use(dataConnectionOptions);
            return fabricOptions;
        }
    }
}
