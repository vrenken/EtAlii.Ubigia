// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public interface IFunctionalOptions : IExtensible
    {
        /// <summary>
        /// The client configuration root that will be used to configure each of the functional contexts and parsers/processors.
        /// </summary>
        IConfigurationRoot ConfigurationRoot { get; }

        IFunctionHandlersProvider FunctionHandlersProvider { get; }

        IRootHandlerMappersProvider RootHandlerMappersProvider { get; }
        bool CachingEnabled { get; }

        ILogicalContext LogicalContext { get; }
    }
}
