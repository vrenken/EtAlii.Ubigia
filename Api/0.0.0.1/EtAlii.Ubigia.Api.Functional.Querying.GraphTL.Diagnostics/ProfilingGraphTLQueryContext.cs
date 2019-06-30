namespace EtAlii.Ubigia.Api.Functional.Diagnostics
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api.Diagnostics.Profiling;

    public class ProfilingGraphTLQueryContext : IProfilingGraphTLQueryContext
    {
        private readonly IGraphTLQueryContext _decoree;
        public IProfiler Profiler { get; }

        public ProfilingGraphTLQueryContext(
            IGraphTLQueryContext decoree, 
            IProfiler profiler)
        {
            _decoree = decoree;
            Profiler = profiler.Create(ProfilingAspects.Functional.ScriptSet);  // TODO: this should be Functional.QuerySet.
        }

        public QueryParseResult Parse(string text)
        {
            var profile = Profiler.Begin("Parsing");

            var result = _decoree.Parse(text);

            Profiler.End(profile);

            return result;
        }

        public IObservable<QueryProcessingResult> Process(Query query, IQueryScope scope)
        {
            dynamic profile = Profiler.Begin("Process");
            profile.Query = query.ToString();

            var result = _decoree.Process(query, scope);

            Profiler.End(profile);

            return result;
        }

        public IObservable<QueryProcessingResult> Process(string[] text, IQueryScope scope)
        {
            dynamic profile = Profiler.Begin("Process");
            profile.Query = string.Join(Environment.NewLine, text);

            var result = _decoree.Process(text, scope);

            Profiler.End(profile);

            return result;
        }

        public IObservable<QueryProcessingResult> Process(string[] text)
        {
            dynamic profile = Profiler.Begin("Process");
            profile.Query = string.Join(Environment.NewLine, text);

            var result = _decoree.Process(text);

            Profiler.End(profile);

            return result;
        }

        public IObservable<QueryProcessingResult> Process(string text)
        {
            dynamic profile = Profiler.Begin("Processing");
            profile.Query = text;
            var result = _decoree.Process(text);

            Profiler.End(profile);

            return result;
        }

        public IObservable<QueryProcessingResult> Process(string text, params object[] args)
        {
            dynamic profile = Profiler.Begin("Processing");
            profile.Query = text;
            profile.Arguments = string.Join(", ", args.Select(a => a.ToString()));

            var result = _decoree.Process(text, args);

            Profiler.End(profile);

            return result;
        }
    }
}
