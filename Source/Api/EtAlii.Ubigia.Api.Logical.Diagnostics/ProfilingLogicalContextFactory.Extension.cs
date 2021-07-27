// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Diagnostics
{
    using Microsoft.Extensions.Configuration;

    public static class ProfilingLogicalContextFactoryExtension
    {
        public static IProfilingLogicalContext CreateForProfiling(this LogicalContextFactory logicalContextFactory, LogicalContextConfiguration configuration, IConfiguration configurationRoot)
        {
            configuration.Use(new ILogicalContextExtension[]
            {
                new ProfilingLogicalContextExtension(configurationRoot),
            });

            return (IProfilingLogicalContext)logicalContextFactory.Create(configuration);
        }
    }
}
