namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using EtAlii.xTechnology.MicroContainer;

    internal class SchemaProcessingScaffolding  : IScaffolding
    {
        private readonly ISchemaProcessorConfiguration _configuration;

        public SchemaProcessingScaffolding (ISchemaProcessorConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Register(Container container)
        {
            container.Register<ISchemaProcessor, SchemaProcessor>();

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
            container.Register(() => _configuration.TraversalContext);
            container.Register(() => _configuration.SchemaScope);
            container.Register(() => _configuration);
        }
    }
}
