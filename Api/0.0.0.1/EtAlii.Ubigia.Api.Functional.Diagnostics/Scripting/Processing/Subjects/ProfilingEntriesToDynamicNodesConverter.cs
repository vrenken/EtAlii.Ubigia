namespace EtAlii.Ubigia.Api.Functional.Diagnostics.Scripting
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Diagnostics.Profiling;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Logical;

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

        public async Task<IEnumerable<DynamicNode>> Convert(IEnumerable<IReadOnlyEntry> entries, ExecutionScope scope)
        {
            dynamic profile = _profiler.Begin("Converting entries to nodes");
            profile.Entries = entries;

            var result = await _decoree.Convert(entries, scope);

            profile.Result = result;
            _profiler.End(profile);

            return result;
        }

        public async Task<DynamicNode> Convert(IReadOnlyEntry entry, ExecutionScope scope)
        {
            dynamic profile = _profiler.Begin("Converting entry to node");
            profile.Entry = entry;

            var result = await _decoree.Convert(entry, scope);

            profile.Result = result;
            _profiler.End(profile);

            return result;
        }
    }
}