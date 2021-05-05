namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.Ubigia.Api.Functional.Traversal;

    public interface ISchemaProcessorConfiguration : IConfiguration
    {
        ISchemaScope SchemaScope { get; }

        ITraversalContext TraversalContext { get; }

        SchemaProcessorConfiguration Use(ISchemaScope scope);
        SchemaProcessorConfiguration Use(ITraversalContext traversalContext);
    }
}
