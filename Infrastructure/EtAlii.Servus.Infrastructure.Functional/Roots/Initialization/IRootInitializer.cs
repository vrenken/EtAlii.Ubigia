namespace EtAlii.Servus.Infrastructure.Functional
{
    using System;
    using EtAlii.Servus.Api;

    public interface IRootInitializer
    {
        void Initialize(Guid spaceId, Root root);
    }
}