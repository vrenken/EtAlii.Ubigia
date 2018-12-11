namespace EtAlii.Ubigia.Api.Functional.Diagnostics
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Diagnostics.Profiling;
    using EtAlii.Ubigia.Api.Functional;

    public class ProfilingGraphQLQueryContext : IProfilingGraphQLQueryContext
    {
        private readonly IGraphQLQueryContext _decoree;
        public IProfiler Profiler { get; }

        public ProfilingGraphQLQueryContext(
            IGraphQLQueryContext decoree, 
            IProfiler profiler)
        {
            _decoree = decoree;
            Profiler = profiler.Create(ProfilingAspects.Functional.ScriptSet);
        }

        public async Task<QueryExecutionResult> Execute(string query)
        {
            dynamic profile = Profiler.Begin("Execution");
            profile.Query = query;
            
            var result = await _decoree.Execute(query);

            Profiler.End(profile);

            return result;
        }
    }
}
