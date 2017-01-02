namespace EtAlii.Servus.Api.Data
{
    using EtAlii.Servus.Api;

    public interface IContentDefinitionQueryHandler
    {
        IReadOnlyContentDefinition Execute(ContentDefinitionQuery query);
    }
}
