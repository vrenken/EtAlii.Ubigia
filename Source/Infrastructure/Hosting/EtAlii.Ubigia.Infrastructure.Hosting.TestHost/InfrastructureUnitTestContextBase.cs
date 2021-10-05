// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Hosting.TestHost
{
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.xTechnology.MicroContainer;

    public abstract class InfrastructureUnitTestContextBase
    {
        public TInstance CreateComponent<TInstance>(FunctionalOptions options) => Factory.Create<TInstance>(options);
    }
}
