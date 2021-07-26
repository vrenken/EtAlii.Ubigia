// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using EtAlii.Ubigia.Api.Logical.Tests;
    using EtAlii.xTechnology.MicroContainer;

    public class FunctionalTestContextFactory
    {
        public IFunctionalTestContext Create()
        {
            var container = new Container();

            container.Register<IFunctionalTestContext, FunctionalTestContext>();
            container.Register(() => new LogicalTestContextFactory().Create());
            return container.GetInstance<IFunctionalTestContext>();
        }
    }
}
