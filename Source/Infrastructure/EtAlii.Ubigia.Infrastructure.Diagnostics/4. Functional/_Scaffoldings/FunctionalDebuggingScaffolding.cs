// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Diagnostics;

using EtAlii.Ubigia.Infrastructure.Functional;
using EtAlii.xTechnology.Diagnostics;
using EtAlii.xTechnology.MicroContainer;

internal class FunctionalDebuggingScaffolding : IScaffolding
{
    private readonly DiagnosticsOptions _options;

    public FunctionalDebuggingScaffolding(DiagnosticsOptions options)
    {
        _options = options;
    }

    public void Register(IRegisterOnlyContainer container)
    {
        if (_options.InjectDebugging) // debugging is enabled
        {
            container.RegisterDecorator<IEntryRepository, DebuggingEntryRepositoryDecorator>();
        }
    }
}
