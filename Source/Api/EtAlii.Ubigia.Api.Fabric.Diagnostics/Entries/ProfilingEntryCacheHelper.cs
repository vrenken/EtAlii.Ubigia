// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric.Diagnostics
{
    using EtAlii.Ubigia.Diagnostics.Profiling;

    public class ProfilingEntryCacheHelper : IEntryCacheHelper
    {
        private readonly IProfiler _profiler;
        private readonly IEntryCacheHelper _decoree;
        public ProfilingEntryCacheHelper(
            IEntryCacheHelper decoree,
            IProfiler profiler)
        {
            _profiler = profiler.Create(ProfilingAspects.Fabric.EntryCache);
            _decoree = decoree;
        }

        public IReadOnlyEntry Get(in Identifier identifier)
        {
            dynamic profile = _profiler.Begin("Getting cached entry: " + identifier.ToTimeString());
            profile.Identifier = identifier;

            var result = _decoree.Get(identifier);
            profile.Result = result;
            profile.Action = "Getting cached entry: " + identifier.ToTimeString() + (result == null ? "" : " - AVAILABLE");
            _profiler.End(profile);

            return result;
        }

        public void Store(IReadOnlyEntry entry)
        {
            dynamic profile = _profiler.Begin("Storing entry in cache: " + entry.Id.ToTimeString());
            profile.Entry = entry;

            _decoree.Store(entry);

            _profiler.End(profile);
        }

        public bool ShouldStore(IReadOnlyEntry entry)
        {
            dynamic profile = _profiler.Begin("Should store entry in cache: " + entry.Id.ToTimeString());
            profile.Entry = entry;

            var result = _decoree.ShouldStore(entry);

            profile.Action = "Should store entry in cache: " + entry.Id.ToTimeString() + " = " + (result ? "YES" : "NO");
            profile.Result = result;
            _profiler.End(profile);

            return result;
        }


        public void InvalidateRelated(IReadOnlyEntry entry)
        {
            dynamic profile = _profiler.Begin("Invalidating related entries in cache: " + entry.Id.ToTimeString());
            profile.Entry = entry;

            _decoree.InvalidateRelated(entry);

            _profiler.End(profile);
        }
    }
}