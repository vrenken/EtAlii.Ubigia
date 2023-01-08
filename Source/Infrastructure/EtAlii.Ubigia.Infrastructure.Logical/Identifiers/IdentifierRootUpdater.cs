// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

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

        public async Task Update(Guid storageId, Guid spaceId, RootTemplate rootTemplate, Identifier id)
        {
            var root = await _context.Roots.Get(spaceId, rootTemplate.Name).ConfigureAwait(false);
            if (root == null)
            {
                // QUESTION: Should this be possible?
                root = new Root
                {
                    // RCI2022: We want to make roots case insensitive.
                    Name = rootTemplate.Name.ToUpper(),
                    Identifier = id,
                };
                await _context.Roots.Add(storageId, spaceId, root).ConfigureAwait(false);
            }
            else
            {
                root.Identifier = id;
                await _context.Roots.Update(spaceId, root.Id, root).ConfigureAwait(false);
            }
        }
    }
}
