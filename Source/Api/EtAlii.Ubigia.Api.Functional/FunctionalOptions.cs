// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Transport;
    using Microsoft.Extensions.Configuration;

    public class FunctionalOptions : LogicalContextOptions, IFunctionalOptions, IEditableFunctionalOptions
    {
        IFunctionHandlersProvider IEditableFunctionalOptions.FunctionHandlersProvider { get => FunctionHandlersProvider; set => FunctionHandlersProvider = value; }
        public IFunctionHandlersProvider FunctionHandlersProvider { get; private set; }

        IRootHandlerMappersProvider IEditableFunctionalOptions.RootHandlerMappersProvider { get => RootHandlerMappersProvider; set => RootHandlerMappersProvider = value; }
        public IRootHandlerMappersProvider RootHandlerMappersProvider { get; private set; }

        public FunctionalOptions CreateScope()
        {
            // TODO: We should somehow convert this scope in one that replicates the generated ServiceCollection.
            var extensions = Extensions
                .Where(e => e is not CommonFunctionalExtension)
                .ToArray();
            return new FunctionalOptions(ConfigurationRoot)
                .Use(this)
                .Use(extensions)
                .Use(FunctionHandlersProvider)
                .Use(RootHandlerMappersProvider)
                .Use(Connection)
                .UseCaching(CachingEnabled);
        }

        public FunctionalOptions(IConfigurationRoot configurationRoot)
            : base(configurationRoot)
        {
            CachingEnabled = true;
            FunctionHandlersProvider = Traversal.FunctionHandlersProvider.Empty;
            RootHandlerMappersProvider = Traversal.RootHandlerMappersProvider.Empty;

            ((ConfigurationBase)this).Use(new IFunctionalExtension[] { new CommonFunctionalExtension(this) });
        }


        public FunctionalOptions UseCaching(bool cachingEnabled)
        {
            CachingEnabled = cachingEnabled;
            return this;
        }

        // public FunctionalOptions Use(ExecutionScope scope)
        // {
        //     ExecutionScope = scope ?? throw new ArgumentException("No functional scope specified", nameof(scope));
        //     return this;
        // }

        public FunctionalOptions Use(IDataConnection dataConnection)
        {
            // LogicalContext = dataConnection ?? throw new ArgumentException("No data connection specified", nameof(dataConnection));
            //
            ((IEditableFabricContextOptions)this).Connection = dataConnection ?? throw new ArgumentException("No data connection specified", nameof(dataConnection));
            // if(((IFabricContextOptions)logicalContext.Options).Connection is var connection)
            // {
            //     ((FabricContextOptions)this).Use(connection);
            // }
            // return UseCaching(logicalContext.Options.CachingEnabled);
            return this;
        }

        // public FunctionalOptions Use(ITraversalContext traversalContext)
        // {
        //     TraversalContext = traversalContext ?? throw new ArgumentException("No traversal context specified", nameof(traversalContext));
        //     return this;
        // }
        //
    }
}
