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
            
            container.Register<IQueryValueProcessor, QueryValueProcessor>();
            container.Register<IMutationValueProcessor, MutationValueProcessor>();
            container.Register<IValueGetter, ValueGetter>();
            container.Register<IValueSetter, ValueSetter>();
            container.Register<IPropertiesValueGetter, PropertiesValueGetter>();
            container.Register<IPropertiesValueSetter, PropertiesValueSetter>();
            container.Register<IPathValueGetter, PathValueGetter>();
            container.Register<IPathValueSetter, PathValueSetter>();

            container.Register<IQueryStructureProcessor, QueryStructureProcessor>();
            container.Register<IMutationStructureProcessor, MutationStructureProcessor>();

            container.Register<IRelatedIdentityFinder, RelatedIdentityFinder>();
            container.Register<IPathDeterminer, PathDeterminer>();
            container.Register<IPathStructureBuilder, PathStructureBuilder>();
            container.Register<IPathCorrecter, PathCorrecter>();

            container.Register<IScriptProcessingContext, ScriptProcessingContext>();
            container.Register(() => _configuration.ScriptContext);
            container.Register(() => _configuration.QueryScope);
            container.Register(() => _configuration);
        }
    }
}
