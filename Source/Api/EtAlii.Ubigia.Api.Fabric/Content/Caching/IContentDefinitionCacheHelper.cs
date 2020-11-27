namespace EtAlii.Ubigia.Api.Fabric
{
    internal interface IContentDefinitionCacheHelper
    {
        IReadOnlyContentDefinition Get(in Identifier identifier);
        void Store(in Identifier identifier, IReadOnlyContentDefinition definition);
    }
}