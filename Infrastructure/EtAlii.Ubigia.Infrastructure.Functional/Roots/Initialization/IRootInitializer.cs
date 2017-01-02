namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;
    using EtAlii.Ubigia.Api;

    public interface IRootInitializer
    {
        void Initialize(Guid spaceId, Root root);
    }
}