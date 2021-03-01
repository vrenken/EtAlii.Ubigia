namespace EtAlii.Ubigia.Api.Functional.Context.Diagnostics
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
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

        public IAsyncEnumerable<TResult> ProcessMultiple<TResult>(Schema schema)
        {
            return _decoree.ProcessMultiple<TResult>(schema);
        }

        public Task<TResult> ProcessSingle<TResult>(Schema schema)
        {
            return _decoree.ProcessSingle<TResult>(schema);
        }
    }
}
