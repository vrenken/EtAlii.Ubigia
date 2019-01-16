namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.Ubigia.Api.Logical;

    public interface IGraphQLQueryContextConfiguration
    {
        ILogicalContext LogicalContext { get; }
        IGraphQLQueryContextExtension[] Extensions { get; }

        IGraphQLQueryContextConfiguration Use(IGraphQLQueryContextExtension[] extensions);
        IGraphQLQueryContextConfiguration Use(ILogicalContext logicalContext);
    }
}