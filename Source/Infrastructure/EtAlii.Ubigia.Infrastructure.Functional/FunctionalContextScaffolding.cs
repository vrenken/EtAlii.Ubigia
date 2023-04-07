// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional;

using System;
using System.Linq;
using EtAlii.Ubigia.Infrastructure.Logical;
using EtAlii.xTechnology.MicroContainer;
using EtAlii.xTechnology.Threading;

internal class FunctionalContextScaffolding : IScaffolding
{
     private readonly FunctionalContextOptions _options;

     public FunctionalContextScaffolding(FunctionalContextOptions options)
     {
         _options = options;
     }

    public void Register(IRegisterOnlyContainer container)
    {
        // Infrastructure
        if (string.IsNullOrWhiteSpace(_options.Name))
        {
            throw new NotSupportedException("The name is required to construct a Infrastructure instance");
        }

        var serviceDetails = _options.ServiceDetails.First(); // We'll take the first ServiceDetails to build the system connection with.
        if (serviceDetails == null)
        {
            throw new NotSupportedException("No system service details found. These are required to construct a Infrastructure instance");
        }
        if (serviceDetails.ManagementAddress == null)
        {
            throw new NotSupportedException("The management address is required to construct a Infrastructure instance");
        }
        if (serviceDetails.DataAddress == null)
        {
            throw new NotSupportedException("The data address is required to construct a Infrastructure instance");
        }

        container.Register(CreateFunctionalContext);

        container.Register<IContextCorrelator, ContextCorrelator>();
        container.Register(() => _options.ConfigurationRoot);
        container.Register(() => _options.Logical);
        container.Register<ILocalStorageGetter>(services =>
        {
            var logicalContext = services.GetInstance<ILogicalContext>();
            var localStorageInitializer = services.GetInstance<ILocalStorageInitializer>();
            return new LocalStorageGetter(_options, logicalContext, localStorageInitializer);
        });

        // System
        container.Register<ISystemConnectionCreationProxy, SystemConnectionCreationProxy>();
        container.Register<ISystemStatusContext, SystemStatusContext>();
        if (_options.SystemStatusChecker != null)
        {
            container.Register(_ => _options.SystemStatusChecker);
        }
        else
        {
            container.Register<ISystemStatusChecker, SystemStatusChecker>();
        }

        // Data
        container.Register<IInformationRepository, InformationRepository>();

        container.Register<IRootRepository, RootRepository>();
        container.Register<IEntryRepository, EntryRepository>();
        container.Register<IContentRepository, ContentRepository>();
        container.Register<IContentDefinitionRepository, ContentDefinitionRepository>();
        container.Register<IPropertiesRepository, PropertiesRepository>();

        container.Register<IAccountInitializer, AccountInitializer>();
        container.Register<ISpaceInitializer, DirectSpaceInitializer>();
        // container.Register<ISpaceInitializer, ScriptedSpaceInitializer>();

        // Management
        container.Register<IStorageInitializer, StorageInitializer>();
        container.Register<ILocalStorageInitializer, LocalStorageInitializer>();

        container.Register<IStorageRepository, StorageRepository>();
        container.Register<IAccountRepository, AccountRepository>();
        container.Register<ISpaceRepository, SpaceRepository>();
    }


    private IFunctionalContext CreateFunctionalContext(IServiceCollection services)
    {
        var information = services.GetInstance<IInformationRepository>();
        var spaces = services.GetInstance<ISpaceRepository>();
        var entries = services.GetInstance<IEntryRepository>();
        var roots = services.GetInstance<IRootRepository>();
        var accounts = services.GetInstance<IAccountRepository>();
        var content = services.GetInstance<IContentRepository>();
        var contentDefinition = services.GetInstance<IContentDefinitionRepository>();
        var properties = services.GetInstance<IPropertiesRepository>();
        var storages = services.GetInstance<IStorageRepository>();
        var logicalContext = services.GetInstance<ILogicalContext>();
        var contextCorrelator = services.GetInstance<IContextCorrelator>();
        var systemConnectionCreationProxy = services.GetInstance<ISystemConnectionCreationProxy>();
        var localStorageGetter = services.GetInstance<ILocalStorageGetter>();
        var systemStatusContext = services.GetInstance<ISystemStatusContext>();
        var systemStatusChecker = services.GetInstance<ISystemStatusChecker>();

        var functionalContext = new FunctionalContext(_options, information, spaces, entries, roots, accounts, content, contentDefinition, properties, storages, logicalContext, contextCorrelator, systemConnectionCreationProxy, localStorageGetter, systemStatusContext);

        systemConnectionCreationProxy.Initialize(functionalContext);
        systemStatusChecker.Initialize(functionalContext);

        return functionalContext;
    }
}
