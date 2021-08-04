// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using EtAlii.xTechnology.MicroContainer;

    internal class SchemaProcessingScaffolding  : IScaffolding
    {
        private readonly ISchemaProcessorOptions _options;

        public SchemaProcessingScaffolding (ISchemaProcessorOptions options)
        {
            _options = options;
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
            container.Register(() => _options.TraversalContext);
            container.Register(() => _options.SchemaScope);
            container.Register(() => _options);
        }
    }
}
