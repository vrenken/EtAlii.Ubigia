namespace EtAlii.Ubigia.Api.Fabric
{
    internal interface IContentCacheHelper
    {
        IReadOnlyContent Get(Identifier identifier);
        IReadOnlyContentPart Get(Identifier identifier, ulong contentPartId);

        void Store(Identifier identifier, IReadOnlyContent content);
        void Store(Identifier identifier, IReadOnlyContentPart contentPart);
    }
}