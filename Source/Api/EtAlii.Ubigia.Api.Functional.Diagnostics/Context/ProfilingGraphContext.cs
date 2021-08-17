// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Diagnostics.Profiling;

    /// <inheritdoc />
    public class ProfilingGraphContext : IProfilingGraphContext
    {
        private readonly IGraphContext _decoree;
        public IProfiler Profiler { get; }

        public ProfilingGraphContext(
            IGraphContext decoree,
            IProfiler profiler)
        {
            _decoree = decoree;
            Profiler = profiler.Create(ProfilingAspects.Functional.GraphContext);
        }

        /// <inheritdoc />
        SchemaParseResult IGraphContext.Parse(string text, ExecutionScope scope)
        {
            var profile = Profiler.Begin("Parsing");

            var result = _decoree.Parse(text, scope);

            Profiler.End(profile);

            return result;
        }

        /// <inheritdoc />
        async IAsyncEnumerable<Structure> IGraphContext.Process(Schema schema, ExecutionScope scope)
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

        /// <inheritdoc />
        async IAsyncEnumerable<Structure> IGraphContext.Process(string[] text, ExecutionScope scope)
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

        /// <inheritdoc />
        async IAsyncEnumerable<Structure> IGraphContext.Process(string text, ExecutionScope scope)
        {
            dynamic profile = Profiler.Begin("Processing");
            profile.Query = text;
            var items = _decoree
                .Process(text, scope)
                .ConfigureAwait(false);
            await foreach (var item in items)
            {
                yield return item;
            }

            Profiler.End(profile);
        }

        /// <inheritdoc />
        async IAsyncEnumerable<Structure> IGraphContext.Process(string text, ExecutionScope scope, params object[] args)
        {
            dynamic profile = Profiler.Begin("Processing");
            profile.Query = text;
            profile.Arguments = string.Join(", ", args.Select(a => a.ToString()));

            var items = _decoree
                .Process(text, scope, args)
                .ConfigureAwait(false);
            await foreach (var item in items)
            {
                yield return item;
            }

            Profiler.End(profile);
        }

        /// <inheritdoc />
        public async Task<TResult> ProcessSingle<TResult>(string text, IResultMapper<TResult> resultMapper, ExecutionScope scope)
        {
            dynamic profile = Profiler.Begin("Processing");
            profile.Query = text;
            var item = await _decoree
                .ProcessSingle(text, resultMapper, scope)
                .ConfigureAwait(false);

            Profiler.End(profile);

            return item;
        }

        /// <inheritdoc />
        public async IAsyncEnumerable<TResult> ProcessMultiple<TResult>(string text, IResultMapper<TResult> resultMapper, ExecutionScope scope)
        {
            dynamic profile = Profiler.Begin("Processing");
            profile.Query = text;
            var items = _decoree
                .ProcessMultiple(text, resultMapper, scope)
                .ConfigureAwait(false);
            await foreach (var item in items)
            {
                yield return item;
            }

            Profiler.End(profile);
        }
    }
}
