// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using EtAlii.xTechnology.MicroContainer;

    public class LogicalContextFactory  : Factory<ILogicalContext, LogicalContextConfiguration, ILogicalContextExtension>
    {
        protected override IScaffolding[] CreateScaffoldings(LogicalContextConfiguration configuration)
        {
            return new IScaffolding[]
            {
                new ContextScaffolding(configuration),
                new GraphScaffolding(),
            };
        }
    }
}
