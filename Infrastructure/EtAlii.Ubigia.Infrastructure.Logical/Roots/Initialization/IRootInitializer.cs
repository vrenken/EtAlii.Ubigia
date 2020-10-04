namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;

    public interface IRootInitializer
    {
        void Initialize(Guid spaceId, Root root);
    }
}