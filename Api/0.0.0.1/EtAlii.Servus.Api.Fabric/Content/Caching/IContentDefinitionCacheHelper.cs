namespace EtAlii.Servus.Api.Fabric
{
    internal interface IContentDefinitionCacheHelper
    {
        IReadOnlyContentDefinition Get(Identifier identifier);
        void Store(Identifier identifier, IReadOnlyContentDefinition definition);
    }
}