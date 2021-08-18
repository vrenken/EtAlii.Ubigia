// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional
{
    using System;
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
    }
}
