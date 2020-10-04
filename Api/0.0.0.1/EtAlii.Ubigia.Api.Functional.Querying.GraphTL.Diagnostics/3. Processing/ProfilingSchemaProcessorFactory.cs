namespace EtAlii.Ubigia.Api.Functional.Diagnostics 
{
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
            configuration.Use(new ISchemaProcessorExtension[]
            {
                //new ProfilingQueryProcessorExtension2(_profiler), 
            });

            return _decoree.Create(configuration);
        }
    }
}