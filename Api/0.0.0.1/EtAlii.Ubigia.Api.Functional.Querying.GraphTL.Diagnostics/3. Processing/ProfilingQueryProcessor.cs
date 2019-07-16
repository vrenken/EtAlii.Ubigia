namespace EtAlii.Ubigia.Api.Functional.Diagnostics.Querying 
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Diagnostics.Profiling;

    internal class ProfilingQueryProcessor : IProfilingQueryProcessor
    {
        public IProfiler Profiler { get; }

        private readonly IQueryProcessor _decoree;

        public ProfilingQueryProcessor(
            IQueryProcessor decoree, 
            IProfiler profiler)
        {
            _decoree = decoree;
            Profiler = profiler.Create(ProfilingAspects.Functional.ScriptProcessor);  // TODO: this should be Functional.QueryProcessor.
        }

        public Task<QueryProcessingResult> Process(Query query)
        {
            return _decoree.Process(query);
        }
    }
}