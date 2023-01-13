// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Diagnostics;

using System.Threading.Tasks;
using EtAlii.xTechnology.MicroContainer;

public static class LogicalOptionsUseProfilingExtension
{
    public static LogicalOptions UseProfiling(this LogicalOptions options)
    {
        return options.Use(new IExtension[]
        {
            new ProfilingLogicalContextExtension(options.ConfigurationRoot),
            new ProfilingGraphPathTraverserExtension(),
        });

    }

    public static async Task<LogicalOptions> UseProfiling(this Task<LogicalOptions> optionsTask)
    {
        var options = await optionsTask.ConfigureAwait(false);

        return UseProfiling(options);
    }
}
