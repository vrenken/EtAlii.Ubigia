namespace EtAlii.Servus.Infrastructure.Logical
{
    using System;
    using EtAlii.Servus.Api;

    public interface IRootInitializer
    {
        void Initialize(Guid spaceId, Root root);
    }
}