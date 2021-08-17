// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

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
            Profiler = profiler.Create(ProfilingAspects.Functional.SchemaProcessor);
        }

        public IAsyncEnumerable<Structure> Process(Schema schema, ExecutionScope scope)
        {
            return _decoree.Process(schema, scope);
        }
    }
}
