// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System;

    //using EtAlii.Ubigia.Diagnostics.Profiling

    internal class ProfilingSchemaProcessorFactory : ISchemaProcessorFactory
    {
        private readonly ISchemaProcessorFactory _decoree;
        //private readonly IProfiler _profiler

        public ProfilingSchemaProcessorFactory(
            ISchemaProcessorFactory decoree
            //IProfiler profiler
            )
        {
            _decoree = decoree;
            //_profiler = profiler
        }

        public ISchemaProcessor Create(SchemaProcessorConfiguration configuration)
        {
            var extensions = Array.Empty<ISchemaProcessorExtension>();
            // var extensions = new ISchemaProcessorExtension[]
            // [
            //     new ProfilingQueryProcessorExtension2(_profiler),
            // ]

            configuration.Use(extensions);

            return _decoree.Create(configuration);
        }
    }
}
