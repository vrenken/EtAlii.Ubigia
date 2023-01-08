// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Fabric;

    public class NextIdentifierGetter : INextIdentifierGetter
    {
        private readonly ILogicalContext _context;
        private readonly IFabricContext _fabric;

        public NextIdentifierGetter(
            ILogicalContext context,
            IFabricContext fabric)
        {
            _context = context;
            _fabric = fabric;
        }

        /// <inheritdoc />
        public async Task<Identifier> GetNext(Guid storageId, Guid spaceId, Identifier previousHeadIdentifier)
        {
            var space = await _context.Spaces.Get(spaceId).ConfigureAwait(false);
            var accountId = space.AccountId;

            return await _fabric.Identifiers
                .GetNextIdentifierForPreviousHeadIdentifier(storageId, accountId, spaceId, previousHeadIdentifier)
                .ConfigureAwait(false);
        }
    }
}
