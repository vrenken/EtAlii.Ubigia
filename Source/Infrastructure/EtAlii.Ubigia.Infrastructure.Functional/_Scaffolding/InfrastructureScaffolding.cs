// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional;

using System;
using System.Linq;
using EtAlii.Ubigia.Infrastructure.Logical;
using EtAlii.xTechnology.MicroContainer;
using EtAlii.xTechnology.Threading;

internal class InfrastructureScaffolding : IScaffolding
{
    private readonly FunctionalContextOptions _options;

    public InfrastructureScaffolding(FunctionalContextOptions options)
    {
        _options = options;
    }

    /// <inheritdoc />
    public void Register(IRegisterOnlyContainer container)
    {
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
        container.Register<ILocalStorageGetter>(() => new LocalStorageGetter(_options));
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

        var functionalContext = new FunctionalContext(_options, information, spaces, entries, roots, accounts, content, contentDefinition, properties, storages, logicalContext, contextCorrelator, systemConnectionCreationProxy);

        systemConnectionCreationProxy.Initialize(functionalContext);

        return functionalContext;
    }
}
