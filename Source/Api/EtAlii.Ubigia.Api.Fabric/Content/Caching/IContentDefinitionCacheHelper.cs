namespace EtAlii.Ubigia.Api.Fabric
{
    internal interface IContentDefinitionCacheHelper
    {
        ContentDefinition Get(in Identifier identifier);
        void Store(in Identifier identifier, ContentDefinition definition);
    }
}