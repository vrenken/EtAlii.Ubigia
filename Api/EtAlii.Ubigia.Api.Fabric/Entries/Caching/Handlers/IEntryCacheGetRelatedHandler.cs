namespace EtAlii.Ubigia.Api.Fabric
{
    using System.Collections.Generic;

    internal interface IEntryCacheGetRelatedHandler
    {
        IAsyncEnumerable<IReadOnlyEntry> Handle(Identifier identifier, EntryRelation relations, ExecutionScope scope);
    }
}