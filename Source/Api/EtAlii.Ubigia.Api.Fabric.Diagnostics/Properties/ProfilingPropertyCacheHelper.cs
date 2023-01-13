// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric.Diagnostics;

using EtAlii.Ubigia.Diagnostics.Profiling;

public sealed class ProfilingPropertyCacheHelper : IPropertyCacheHelper
{
    private readonly IProfiler _profiler;
    private readonly IPropertyCacheHelper _decoree;

    public ProfilingPropertyCacheHelper(
        IPropertyCacheHelper decoree,
        IProfiler profiler)
    {
        _profiler = profiler.Create(ProfilingAspects.Fabric.PropertyCache);
        _decoree = decoree;
    }

    public PropertyDictionary GetProperties(in Identifier identifier)
    {
        dynamic profile = _profiler.Begin($"Getting cached properties: {identifier.ToTimeString()}");
        profile.Identifier = identifier;

        var result = _decoree.GetProperties(identifier);
        profile.Result = result;
        profile.Action = $"Getting cached properties: {identifier.ToTimeString()} {(result == null ? "" : " - AVAILABLE")}";
        _profiler.End(profile);

        return result;
    }

    public void StoreProperties(in Identifier identifier, PropertyDictionary properties)
    {
        dynamic profile = _profiler.Begin($"Storing properties in cache: {identifier.ToTimeString()}");
        profile.Properties = properties;

        _decoree.StoreProperties(identifier, properties);

        _profiler.End(profile);
    }
}
