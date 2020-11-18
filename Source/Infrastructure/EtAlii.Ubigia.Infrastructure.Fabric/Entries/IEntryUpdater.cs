namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IEntryUpdater
    {
        Task Update(Entry entry, IEnumerable<IComponent> changedComponents);
        Task Update(IEditableEntry entry, IEnumerable<IComponent> changedComponents);
    }
}