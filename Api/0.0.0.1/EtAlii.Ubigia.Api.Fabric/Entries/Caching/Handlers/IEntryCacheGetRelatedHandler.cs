namespace EtAlii.Ubigia.Api.Fabric
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    internal interface IEntryCacheGetRelatedHandler
    {
        Task<IEnumerable<IReadOnlyEntry>> Handle(Identifier identifier, EntryRelation relations, ExecutionScope scope);
    }
}