// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Diagnostics
{
    using EtAlii.xTechnology.MicroContainer;

    public static class ProfilingLogicalContextFactoryExtension
    {
        public static IProfilingLogicalContext CreateForProfiling(this LogicalContextFactory logicalContextFactory, LogicalOptions options)
        {
            options.Use(new ILogicalContextExtension[]
            {
                new ProfilingLogicalContextExtension(options.ConfigurationRoot),
            });

            return (IProfilingLogicalContext)logicalContextFactory.Create(options);
        }
    }
}
