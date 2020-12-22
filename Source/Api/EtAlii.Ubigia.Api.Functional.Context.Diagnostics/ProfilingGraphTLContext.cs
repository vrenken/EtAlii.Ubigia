namespace EtAlii.Ubigia.Api.Functional.Context.Diagnostics
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Diagnostics.Profiling;

    public class ProfilingGraphXLContext : IProfilingGraphXLContext
    {
        private readonly IGraphXLContext _decoree;
        public IProfiler Profiler { get; }

        public ProfilingGraphXLContext(
            IGraphXLContext decoree,
            IProfiler profiler)
        {
            _decoree = decoree;
            Profiler = profiler.Create(ProfilingAspects.Functional.ScriptSet);  // TODO: this should be Functional.QuerySet.
        }

        public SchemaParseResult Parse(string text)
        {
            var profile = Profiler.Begin("Parsing");

            var result = _decoree.Parse(text);

            Profiler.End(profile);

            return result;
        }

        public Task<SchemaProcessingResult> Process(Schema schema, ISchemaScope scope)
        {
            dynamic profile = Profiler.Begin("Process");
            profile.Query = schema.ToString();

            var result = _decoree.Process(schema, scope);

            Profiler.End(profile);

            return result;
        }

        public Task<SchemaProcessingResult> Process(string[] text, ISchemaScope scope)
        {
            dynamic profile = Profiler.Begin("Process");
            profile.Query = string.Join(Environment.NewLine, text);

            var result = _decoree.Process(text, scope);

            Profiler.End(profile);

            return result;
        }

        public Task<SchemaProcessingResult> Process(string[] text)
        {
            dynamic profile = Profiler.Begin("Process");
            profile.Query = string.Join(Environment.NewLine, text);

            var result = _decoree.Process(text);

            Profiler.End(profile);

            return result;
        }

        public Task<SchemaProcessingResult> Process(string text)
        {
            dynamic profile = Profiler.Begin("Processing");
            profile.Query = text;
            var result = _decoree.Process(text);

            Profiler.End(profile);

            return result;
        }

        public Task<SchemaProcessingResult> Process(string text, params object[] args)
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
