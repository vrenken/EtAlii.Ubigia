// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;
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

        public Identifier GetNext(Guid spaceId, in Identifier previousHeadIdentifier)
        {
            var space = _context.Spaces.Get(spaceId);
            var storageId = _context.Storages.GetLocal().Id;
            var accountId = space.AccountId;

            return _fabric.Identifiers.GetNextIdentifierForPreviousHeadIdentifier(storageId, accountId, spaceId, previousHeadIdentifier);
        }
    }
}