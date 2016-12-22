namespace EtAlii.Servus.Infrastructure.Logical
{
    using System;
    using EtAlii.Servus.Api;

    public interface IEntryPreparer
    {
        Entry Prepare(Guid spaceId);
        Entry Prepare(Guid spaceId, Identifier id);
    }
}