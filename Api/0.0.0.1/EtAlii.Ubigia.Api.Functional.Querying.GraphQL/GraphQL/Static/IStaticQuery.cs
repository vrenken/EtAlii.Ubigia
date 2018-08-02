namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using global::GraphQL.Types;

    public interface IStaticQuery : IObjectGraphType, IComplexGraphType, IGraphType, IProvideMetadata, INamedType, IImplementInterfaces
    {
    }
}