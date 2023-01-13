// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional;

using System;
using EtAlii.Ubigia.Api.Functional.Traversal;
using EtAlii.Ubigia.Api.Logical;
using EtAlii.xTechnology.MicroContainer;
using Microsoft.Extensions.Configuration;

public sealed class FunctionalOptions : IExtensible
{
    public IConfigurationRoot ConfigurationRoot { get; }

    public ILogicalContext LogicalContext => _logicalContext.Value;
    private Lazy<ILogicalContext> _logicalContext;

    /// <inheritdoc/>
    IExtension[] IExtensible.Extensions { get; set; }

    public IFunctionHandlersProvider FunctionHandlersProvider { get; private set; }

    public IRootHandlerMappersProvider RootHandlerMappersProvider { get; private set; }

    public FunctionalOptions(IConfigurationRoot configurationRoot)
    {
        ConfigurationRoot = configurationRoot;
        FunctionHandlersProvider = Traversal.FunctionHandlersProvider.Empty;
        RootHandlerMappersProvider = Traversal.RootHandlerMappersProvider.Empty;
        ((IExtensible)this).Extensions = new IExtension[] { new CommonFunctionalExtension(this) };
    }

    public FunctionalOptions UseLogicalContext(ILogicalContext logicalContext)
    {
        if (logicalContext == null)
        {
            throw new ArgumentNullException(nameof(logicalContext));
        }

        _logicalContext = new Lazy<ILogicalContext>(logicalContext);
        return this;
    }

    internal FunctionalOptions UseLogicalOptions(LogicalOptions logicalOptions)
    {
        if (logicalOptions == null)
        {
            throw new ArgumentNullException(nameof(logicalOptions));
        }

        _logicalContext = new Lazy<ILogicalContext>(() => Factory.Create<ILogicalContext>(logicalOptions));
        return this;
    }

    public FunctionalOptions Use(IFunctionHandlersProvider functionHandlersProvider)
    {
        FunctionHandlersProvider = new FunctionHandlersProvider(functionHandlersProvider.FunctionHandlers, FunctionHandlersProvider.FunctionHandlers);

        return this;
    }

    public FunctionalOptions Use(IRootHandlerMappersProvider rootHandlerMappersProvider)
    {
        RootHandlerMappersProvider = rootHandlerMappersProvider;

        return this;
    }

    public FunctionalOptions Use(FunctionalOptions otherOptions)
    {
        FunctionHandlersProvider = otherOptions.FunctionHandlersProvider;
        RootHandlerMappersProvider = otherOptions.RootHandlerMappersProvider;

        return this;
    }
}
