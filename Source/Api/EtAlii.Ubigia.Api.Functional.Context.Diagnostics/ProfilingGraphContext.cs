namespace EtAlii.Ubigia.Api.Functional.Context.Diagnostics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Diagnostics.Profiling;

    public class ProfilingGraphContext : IProfilingGraphContext
    {
        private readonly IGraphContext _decoree;
        public IProfiler Profiler { get; }

        public ProfilingGraphContext(
            IGraphContext decoree,
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

        public async IAsyncEnumerable<Structure> Process(Schema schema, ISchemaScope scope)
        {
            dynamic profile = Profiler.Begin("Process");
            profile.Query = schema.ToString();

            var items = _decoree
                .Process(schema, scope)
                .ConfigureAwait(false);
            await foreach (var item in items)
            {
                yield return item;
            }

            Profiler.End(profile);
        }

        public async IAsyncEnumerable<Structure> Process(string[] text, ISchemaScope scope)
        {
            dynamic profile = Profiler.Begin("Process");
            profile.Query = string.Join(Environment.NewLine, text);

            var items = _decoree
                .Process(text, scope)
                .ConfigureAwait(false);
            await foreach (var item in items)
            {
                yield return item;
            }

            Profiler.End(profile);
        }

        public async IAsyncEnumerable<Structure> Process(string[] text)
        {
            dynamic profile = Profiler.Begin("Process");
            profile.Query = string.Join(Environment.NewLine, text);

            var items = _decoree
                .Process(text)
                .ConfigureAwait(false);
            await foreach (var item in items)
            {
                yield return item;
            }

            Profiler.End(profile);
        }

        public async IAsyncEnumerable<Structure> Process(string text)
        {
            dynamic profile = Profiler.Begin("Processing");
            profile.Query = text;
            var items = _decoree
                .Process(text)
                .ConfigureAwait(false);
            await foreach (var item in items)
            {
                yield return item;
            }

            Profiler.End(profile);
        }

        public async Task<TResult> ProcessSingle<TResult>(string text, IResultMapper<TResult> resultMapper)
        {
            dynamic profile = Profiler.Begin("Processing");
            profile.Query = text;
            var item = await _decoree
                .ProcessSingle(text, resultMapper)
                .ConfigureAwait(false);

            Profiler.End(profile);

            return item;
        }

        public async IAsyncEnumerable<TResult> ProcessMultiple<TResult>(string text, IResultMapper<TResult> resultMapper)
        {
            dynamic profile = Profiler.Begin("Processing");
            profile.Query = text;
            var items = _decoree
                .ProcessMultiple(text, resultMapper)
                .ConfigureAwait(false);
            await foreach (var item in items)
            {
                yield return item;
            }

            Profiler.End(profile);
        }

        public async IAsyncEnumerable<Structure> Process(string text, params object[] args)
        {
            dynamic profile = Profiler.Begin("Processing");
            profile.Query = text;
            profile.Arguments = string.Join(", ", args.Select(a => a.ToString()));

            var items = _decoree
                .Process(text, args)
                .ConfigureAwait(false);
            await foreach (var item in items)
            {
                yield return item;
            }

            Profiler.End(profile);
        }
    }
}
