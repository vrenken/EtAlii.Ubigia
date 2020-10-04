namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;

    public interface IEntryPreparer
    {
        Entry Prepare(Guid spaceId);
        Entry Prepare(Guid spaceId, Identifier id);
    }
}