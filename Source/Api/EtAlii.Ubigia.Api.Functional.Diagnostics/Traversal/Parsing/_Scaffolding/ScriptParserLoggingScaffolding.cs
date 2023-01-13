// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using EtAlii.xTechnology.Diagnostics;
using EtAlii.xTechnology.MicroContainer;

internal class ScriptParserLoggingScaffolding : IScaffolding
{
    private readonly DiagnosticsOptions _options;

    public ScriptParserLoggingScaffolding(DiagnosticsOptions options)
    {
        _options = options;
    }

    public void Register(IRegisterOnlyContainer container)
    {
        if (_options?.InjectLogging ?? false) // logging is enabled.
        {
            container.RegisterDecorator<IScriptParser, LoggingScriptParser>();
            container.RegisterDecorator<IPathParser, LoggingPathParser>();
        }
    }
}
