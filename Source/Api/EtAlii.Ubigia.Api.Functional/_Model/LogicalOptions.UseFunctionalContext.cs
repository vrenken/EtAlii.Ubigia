// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional;

using System.Threading.Tasks;
using EtAlii.Ubigia.Api.Logical;

public static class LogicalOptionsUseFunctionalContextExtension
{
    public static FunctionalOptions UseFunctionalContext(this LogicalOptions logicalOptions)
    {
        return new FunctionalOptions(logicalOptions.ConfigurationRoot)
            .UseLogicalOptions(logicalOptions);
    }

    public static async Task<FunctionalOptions> UseFunctionalContext(this Task<LogicalOptions> logicalOptionsTask)
    {
        var logicalOptions = await logicalOptionsTask.ConfigureAwait(false);

        return new FunctionalOptions(logicalOptions.ConfigurationRoot)
            .UseLogicalOptions(logicalOptions);
    }
}
