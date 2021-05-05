namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System.Collections.Generic;
    using EtAlii.Ubigia.Diagnostics.Profiling;

    internal class ProfilingSchemaProcessor : IProfilingSchemaProcessor
    {
        public IProfiler Profiler { get; }

        private readonly ISchemaProcessor _decoree;

        public ProfilingSchemaProcessor(
            ISchemaProcessor decoree,
            IProfiler profiler)
        {
            _decoree = decoree;
            Profiler = profiler.Create(ProfilingAspects.Functional.ScriptProcessor);  // TODO: this should be Functional.QueryProcessor.
        }

        public IAsyncEnumerable<Structure> Process(Schema schema)
        {
            return _decoree.Process(schema);
        }
    }
}
