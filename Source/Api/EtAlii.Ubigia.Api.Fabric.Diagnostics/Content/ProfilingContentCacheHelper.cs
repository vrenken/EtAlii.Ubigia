// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric.Diagnostics;

using EtAlii.Ubigia.Diagnostics.Profiling;

internal sealed class ProfilingContentCacheHelper : IContentCacheHelper
{
    private readonly IProfiler _profiler;
    private readonly IContentCacheHelper _decoree;

    public ProfilingContentCacheHelper(
        IContentCacheHelper decoree,
        IProfiler profiler)
    {
        _profiler = profiler.Create(ProfilingAspects.Fabric.ContentCache);
        _decoree = decoree;
    }

    public Content Get(in Identifier identifier)
    {
        dynamic profile = _profiler.Begin("Getting cached content: " + identifier.ToTimeString());
        profile.Identifier = identifier;

        var result = _decoree.Get(identifier);
        profile.Result = result;
        profile.Action = "Getting cached content: " + identifier.ToTimeString() + (result == null ? "" : " - AVAILABLE");
        _profiler.End(profile);

        return result;
    }

    public ContentPart Get(in Identifier identifier, ulong contentPartId)
    {
        dynamic profile = _profiler.Begin("Getting cached content part: " + identifier.ToTimeString() + " - " + contentPartId);
        profile.Identifier = identifier;
        profile.ContentPartId = contentPartId;

        var result = _decoree.Get(identifier, contentPartId);
        profile.Result = result;
        profile.Action = "Getting cached content part: " + identifier.ToTimeString() + " - " + contentPartId + " - " + (result == null ? "" : " - AVAILABLE");
        _profiler.End(profile);

        return result;
    }

    public void Store(in Identifier identifier, Content content)
    {
        dynamic profile = _profiler.Begin("Storing content in cache: " + identifier.ToTimeString());
        profile.Content = content;

        _decoree.Store(identifier, content);

        _profiler.End(profile);
    }

    public void Store(in Identifier identifier, ContentPart contentPart)
    {
        dynamic profile = _profiler.Begin("Storing content part in cache: " + identifier.ToTimeString() + " - " + contentPart.Id);
        profile.ContentPart = contentPart;

        _decoree.Store(identifier, contentPart);

        _profiler.End(profile);
    }
}
