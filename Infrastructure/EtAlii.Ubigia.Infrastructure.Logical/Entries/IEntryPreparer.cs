namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;
    using EtAlii.Ubigia.Api;

    public interface IEntryPreparer
    {
        Entry Prepare(Guid spaceId);
        Entry Prepare(Guid spaceId, Identifier id);
    }
}