// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System;
    using EtAlii.xTechnology.MicroContainer;

    public class GraphContextFactory : Factory<IGraphContext, FunctionalOptions, IFunctionalExtension>
    {
        protected override IScaffolding[] CreateScaffoldings(FunctionalOptions options)
        {
            return Array.Empty<IScaffolding>();
        }
    }
}
