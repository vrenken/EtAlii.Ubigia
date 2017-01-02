namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Infrastructure.Logical;

    internal class RootInitializer : IRootInitializer
    {
        private readonly ILogicalContext _context;

        public RootInitializer(ILogicalContext context)
        {
            _context = context;
        }

        public void Initialize(Guid spaceId, Root root)
        {
            if (root.Identifier == Identifier.Empty)
            {
                var entry = (IEditableEntry) _context.Entries.Prepare(spaceId);
                entry.Type = root.Name;

                //var tailRoot = Roots.Get(spaceId, DefaultRoot.Tail);
                //entry.Parent = Relation.NewRelation(tailRoot.Identifier);

                _context.Entries.Store(entry);
                root.Identifier = entry.Id;
                _context.Roots.Update(spaceId, root.Id, root);
            }
        }
    }
}