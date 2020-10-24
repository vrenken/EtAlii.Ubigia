namespace EtAlii.Ubigia.Api.Fabric
{
    internal interface IContentDefinitionCacheHelper
    {
        IReadOnlyContentDefinition Get(Identifier identifier);
        void Store(Identifier identifier, IReadOnlyContentDefinition definition);
    }
}