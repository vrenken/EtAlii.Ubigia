// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Hosting.TestHost
{
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.xTechnology.MicroContainer;

    public abstract class InfrastructureUnitTestContextBase
    {
        public T CreateComponent<T>(FunctionalOptions options) => Factory.Create<T, IExtension>(options);
    }
}
