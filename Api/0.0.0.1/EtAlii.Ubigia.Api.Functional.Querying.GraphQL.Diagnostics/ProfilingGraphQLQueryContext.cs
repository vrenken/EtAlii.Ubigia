namespace EtAlii.Ubigia.Api.Functional.Diagnostics
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Diagnostics.Profiling;
    using EtAlii.Ubigia.Api.Functional.Querying.GraphQL;

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

        public async Task<QueryParseResult> Parse(string text)
        {
            var profile = Profiler.Begin("Parsing");

            var result = await _decoree.Parse(text);

            Profiler.End(profile);

            return result;
        }

        public async Task<GraphQLQueryProcessingResult> Process(Query query)
        {
            dynamic profile = Profiler.Begin("Process");
            profile.Query = query;
            
            var result = await _decoree.Process(query);

            Profiler.End(profile);

            return result;
        }
    }
}
