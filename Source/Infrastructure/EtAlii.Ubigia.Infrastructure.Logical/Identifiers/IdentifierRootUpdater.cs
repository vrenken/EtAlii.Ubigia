namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;
    using System.Threading.Tasks;

    public class IdentifierRootUpdater : IIdentifierRootUpdater
    {
        private readonly ILogicalContext _context;

        public IdentifierRootUpdater(ILogicalContext context)
        {
            _context = context;
        }

        public async Task Update(Guid spaceId, string name, Identifier id)
        {
            var root = await _context.Roots.Get(spaceId, name);
            if (root == null)
            {
                await _context.Roots.Add(spaceId, new Root { Name = name, Identifier = id });
            }
            else
            {
                root.Identifier = id;
                await _context.Roots.Update(spaceId, root.Id, root);
            }
        }
    }
}