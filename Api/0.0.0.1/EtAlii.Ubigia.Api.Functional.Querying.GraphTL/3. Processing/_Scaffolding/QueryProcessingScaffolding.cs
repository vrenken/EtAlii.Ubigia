namespace EtAlii.Ubigia.Api.Functional 
{
    using EtAlii.xTechnology.MicroContainer;

    internal class QueryProcessingScaffolding  : IScaffolding
    {
        private readonly IQueryProcessorConfiguration _configuration;

        public QueryProcessingScaffolding (IQueryProcessorConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Register(Container container)
        {
            container.Register<IQueryProcessor, QueryProcessor>();
            
            container.Register<IValueQueryProcessor, ValueQueryProcessor>();
            container.Register<IValueMutationProcessor, ValueMutationProcessor>();
            container.Register<IValueGetter, ValueGetter>();
            container.Register<IValueSetter, ValueSetter>();
            container.Register<IPropertiesValueGetter, PropertiesValueGetter>();
            container.Register<IPropertiesValueSetter, PropertiesValueSetter>();
            container.Register<IPathValueGetter, PathValueGetter>();
            container.Register<IPathValueSetter, PathValueSetter>();

            container.Register<IStructureQueryProcessor, StructureQueryProcessor>();
            container.Register<IStructureMutationProcessor, StructureMutationProcessor>();
            container.Register<IStructureQueryBuilder, StructureQueryBuilder >();
            container.Register<IStructureMutationBuilder, StructureMutationBuilder>();


            container.Register<IRelatedIdentityFinder, RelatedIdentityFinder>();

            container.Register<IScriptProcessingContext, ScriptProcessingContext>();
            container.Register(() => _configuration.ScriptContext);
            container.Register(() => _configuration.QueryScope);
            container.Register(() => _configuration);
        }
    }
}
