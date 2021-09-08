// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Diagnostics
{
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
    }
}
