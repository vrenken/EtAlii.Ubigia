namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Infrastructure.Fabric;

    internal class RootInitializer : IRootInitializer
    {
        private readonly IFabricContext _fabric;
        private readonly ILogicalEntrySet _entries;

        public RootInitializer(IFabricContext fabric, ILogicalEntrySet entries)
        {
            _fabric = fabric;
            _entries = entries;
        }

        public void Initialize(Guid spaceId, Root root)
        {
            if (root.Identifier == Identifier.Empty)
            {
                var entry = (IEditableEntry) _entries.Prepare(spaceId);
                entry.Type = root.Name;

                //var tailRoot = Roots.Get(spaceId, DefaultRoot.Tail)
                //entry.Parent = Relation.NewRelation(tailRoot.Identifier)

                _fabric.Entries.Store(entry);
                root.Identifier = entry.Id;
                _fabric.Roots.Update(spaceId, root.Id, root);
            }
        }
    }
}