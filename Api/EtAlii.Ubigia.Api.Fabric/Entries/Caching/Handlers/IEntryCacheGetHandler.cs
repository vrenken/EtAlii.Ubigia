namespace EtAlii.Ubigia.Api.Fabric
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    internal interface IEntryCacheGetHandler
    {
        Task<IReadOnlyEntry> Handle(Identifier identifier, ExecutionScope scope);
        Task<IEnumerable<IReadOnlyEntry>> Handle(IEnumerable<Identifier> identifiers, ExecutionScope scope);
    }
}