namespace EtAlii.Ubigia.Api.Fabric
{
    using System;

    internal interface IContentCacheHelper
    {
        IReadOnlyContent Get(Identifier identifier);
        IReadOnlyContentPart Get(Identifier identifier, UInt64 contentPartId);

        void Store(Identifier identifier, IReadOnlyContent content);
        void Store(Identifier identifier, IReadOnlyContentPart contentPart);
    }
}