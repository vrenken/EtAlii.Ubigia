namespace EtAlii.Ubigia.Api.Fabric
{
    internal interface IContentCacheHelper
    {
        IReadOnlyContent Get(in Identifier identifier);
        IReadOnlyContentPart Get(in Identifier identifier, ulong contentPartId);

        void Store(in Identifier identifier, IReadOnlyContent content);
        void Store(in Identifier identifier, IReadOnlyContentPart contentPart);
    }
}