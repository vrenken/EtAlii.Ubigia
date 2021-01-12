namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System;
    using EtAlii.Ubigia.Api.Functional.Traversal;

    public class SchemaProcessorConfiguration : ConfigurationBase, ISchemaProcessorConfiguration
    {
        public ISchemaScope SchemaScope { get; private set; }

        public ITraversalContext TraversalContext { get; private set; }


        public SchemaProcessorConfiguration Use(ISchemaScope scope)
        {
            SchemaScope = scope ?? throw new ArgumentException("No scope specified", nameof(scope));
            return this;
        }

        public SchemaProcessorConfiguration Use(ITraversalContext traversalContext)
        {
            TraversalContext = traversalContext ?? throw new ArgumentException("No script context specified", nameof(traversalContext));
            return this;
        }
    }
}
