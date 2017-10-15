namespace EtAlii.Servus.Infrastructure.Fabric
{
    using System.Collections.Generic;
    using EtAlii.Servus.Api;

    public interface IEntryUpdater
    {
        void Update(Entry entry, IEnumerable<IComponent> changedComponents);
        void Update(IEditableEntry entry, IEnumerable<IComponent> changedComponents);
    }
}