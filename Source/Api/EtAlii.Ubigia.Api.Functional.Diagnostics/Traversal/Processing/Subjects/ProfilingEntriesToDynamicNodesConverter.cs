// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Diagnostics.Profiling;

    internal class ProfilingEntriesToDynamicNodesConverter : IEntriesToDynamicNodesConverter
    {
        private readonly IEntriesToDynamicNodesConverter _decoree;
        private readonly IProfiler _profiler;

        public ProfilingEntriesToDynamicNodesConverter(
            IEntriesToDynamicNodesConverter decoree,
            IProfiler profiler)
        {
            _decoree = decoree;
            _profiler = profiler.Create(ProfilingAspects.Functional.ScriptProcessorEntryConversion);
        }

        public async IAsyncEnumerable<DynamicNode> Convert(IEnumerable<IReadOnlyEntry> entries, ExecutionScope scope)
        {
            dynamic profile = _profiler.Begin("Converting entries to nodes");
            profile.Entries = entries;

            var result = _decoree.Convert(entries, scope);
            await foreach (var item in result.ConfigureAwait(false))
            {
                yield return item;
            }

            profile.Result = result;
            _profiler.End(profile);
        }

        public async Task<DynamicNode> Convert(IReadOnlyEntry entry, ExecutionScope scope)
        {
            dynamic profile = _profiler.Begin("Converting entry to node");
            profile.Entry = entry;

            var result = await _decoree.Convert(entry, scope).ConfigureAwait(false);

            profile.Result = result;
            _profiler.End(profile);

            return result;
        }
    }
}
