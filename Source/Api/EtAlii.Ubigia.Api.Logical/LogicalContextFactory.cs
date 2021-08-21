// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using EtAlii.xTechnology.MicroContainer;

    public class LogicalContextFactory : Factory<ILogicalContext, LogicalOptions, IExtension>
    {
        protected override IScaffolding[] CreateScaffoldings(LogicalOptions options)
        {
            return new IScaffolding[]
            {
                new ContextScaffolding(options),
                new GraphScaffolding(),
            };
        }
    }
}
