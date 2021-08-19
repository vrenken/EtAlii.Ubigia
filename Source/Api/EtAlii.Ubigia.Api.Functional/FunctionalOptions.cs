// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class FunctionalOptions : IFunctionalOptions, IEditableFunctionalOptions
    {
        public IConfigurationRoot ConfigurationRoot { get; }
        public bool CachingEnabled { get; private set; }

        public ILogicalContext LogicalContext { get; private set; }

        public IExtension[] Extensions { get; private set; }

        /// <inheritdoc/>
        IExtension[] IExtensible.Extensions { get => Extensions; set => Extensions = value; }

        IFunctionHandlersProvider IEditableFunctionalOptions.FunctionHandlersProvider { get => FunctionHandlersProvider; set => FunctionHandlersProvider = value; }
        public IFunctionHandlersProvider FunctionHandlersProvider { get; private set; }

        IRootHandlerMappersProvider IEditableFunctionalOptions.RootHandlerMappersProvider { get => RootHandlerMappersProvider; set => RootHandlerMappersProvider = value; }
        public IRootHandlerMappersProvider RootHandlerMappersProvider { get; private set; }

        public FunctionalOptions(IConfigurationRoot configurationRoot)
        {
            ConfigurationRoot = configurationRoot;
            CachingEnabled = true;
            FunctionHandlersProvider = Traversal.FunctionHandlersProvider.Empty;
            RootHandlerMappersProvider = Traversal.RootHandlerMappersProvider.Empty;
            Extensions = Array.Empty<IExtension>();

            this.Use(new IExtension[] { new CommonFunctionalExtension(this) });
        }


        public FunctionalOptions UseCaching(bool cachingEnabled)
        {
            CachingEnabled = cachingEnabled;
            return this;
        }

        public FunctionalOptions UseLogicalContext(ILogicalContext logicalContext)
        {
            LogicalContext = logicalContext ?? throw new ArgumentException("No logical context specified", nameof(logicalContext));

            return this;
        }
    }
}
