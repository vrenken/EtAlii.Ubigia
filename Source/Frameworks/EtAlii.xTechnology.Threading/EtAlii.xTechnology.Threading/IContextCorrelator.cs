// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Threading;

using System;

public interface IContextCorrelator
{
    bool TryGetValue(string key, out string value);

    IDisposable BeginCorrelationScope(string key, string value, bool throwWhenAlreadyCorrelated = true);

    IDisposable BeginCorrelationScope(string key, string value, IDisposable relatedDisposable, bool throwWhenAlreadyCorrelated = true);
}
