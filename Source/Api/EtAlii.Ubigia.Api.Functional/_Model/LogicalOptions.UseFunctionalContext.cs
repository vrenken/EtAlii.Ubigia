// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.Ubigia.Api.Logical;

    public static class LogicalOptionsUseFunctionalContextExtension
    {
        public static FunctionalOptions UseFunctionalContext(this LogicalOptions logicalOptions)
        {
            return new FunctionalOptions(logicalOptions.ConfigurationRoot)
                .UseLogicalOptions(logicalOptions);
        }
    }
}
