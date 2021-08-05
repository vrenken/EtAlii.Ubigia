// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using EtAlii.xTechnology.MicroContainer;

    public class LogicalContextFactory : Factory<ILogicalContext, LogicalContextOptions, ILogicalContextExtension>
    {
        protected override IScaffolding[] CreateScaffoldings(LogicalContextOptions options)
        {
            return new IScaffolding[]
            {
                new ContextScaffolding(options),
                new GraphScaffolding(),
            };
        }
    }
}
