namespace EtAlii.Servus.Infrastructure.Logical
{
    using System;
    using EtAlii.Servus.Api;

    public class IdentifierRootUpdater : IIdentifierRootUpdater
    {
        private readonly ILogicalContext _context;

        public IdentifierRootUpdater(ILogicalContext context)
        {
            _context = context;
        }

        public void Update(Guid spaceId, string name, Identifier id)
        {
            var root = _context.Roots.Get(spaceId, name);
            if (root == null)
            {
                _context.Roots.Add(spaceId, new Root { Name = name, Identifier = id });
            }
            else
            {
                root.Identifier = id;
                _context.Roots.Update(spaceId, root.Id, root);
            }
        }
    }
}