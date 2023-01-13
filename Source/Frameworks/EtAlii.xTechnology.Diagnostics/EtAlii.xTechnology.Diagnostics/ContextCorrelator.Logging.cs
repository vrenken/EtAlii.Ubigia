// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Diagnostics;

using System;
using EtAlii.xTechnology.Threading;
using Serilog.Context;

public static class ContextCorrelatorLoggingExtension
{
    public static IDisposable BeginLoggingCorrelationScope(
        this IContextCorrelator contextCorrelator,
        string key, string value,
        bool throwWhenAlreadyCorrelated = true)
    {
        return contextCorrelator.BeginCorrelationScope(key, value, LogContext.PushProperty(key, value), throwWhenAlreadyCorrelated);
    }
}
