namespace EtAlii.Servus.Infrastructure.Fabric
{
    using System.Collections.Generic;
    using EtAlii.Servus.Api;

    public interface IEntryStorer
    {
        Entry Store(IEditableEntry entry);
        Entry Store(Entry entry);

        Entry Store(IEditableEntry entry, out IEnumerable<IComponent> storedComponents);
        Entry Store(Entry entry, out IEnumerable<IComponent> storedComponents);
    }
}