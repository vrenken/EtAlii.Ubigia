namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using System;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Diagnostics.Profiling;
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Logging;
    using SimpleInjector;

    public class SpaceBrowserFunctionHandlersProvider : IFunctionHandlersProvider
    {
        public IFunctionHandler[] FunctionHandlers { get; }

        public SpaceBrowserFunctionHandlersProvider()
        {
            FunctionHandlers = new IFunctionHandler[]
            {
                new ViewFunctionHandler()
            };
        }
    }
}
