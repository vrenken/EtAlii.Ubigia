// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using EtAlii.xTechnology.MicroContainer;

    internal class SchemaProcessingScaffolding  : IScaffolding
    {
        public void Register(IRegisterOnlyContainer container)
        {
            container.Register<IScriptProcessor, ScriptProcessor>();

            container.Register<IScriptProcessingContext, ScriptProcessingContext>();

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
        }
    }
}
