namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System.Collections.Generic;

    public interface IEntryUpdater
    {
        void Update(Entry entry, IEnumerable<IComponent> changedComponents);
        void Update(IEditableEntry entry, IEnumerable<IComponent> changedComponents);
    }
}