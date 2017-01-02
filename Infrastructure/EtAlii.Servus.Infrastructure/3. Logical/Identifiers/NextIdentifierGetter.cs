namespace EtAlii.Servus.Infrastructure.Logical
{
    using System;
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Infrastructure.Fabric;
    using EtAlii.Servus.Storage;

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

        public Identifier GetNext(Guid spaceId, Identifier previousHeadIdentifier)
        {
            var space = _context.Spaces.Get(spaceId);
            var storageId = _context.Storages.GetLocal().Id;
            var accountId = space.AccountId;

            return _fabric.Identifiers.GetNextIdentifierForPreviousHeadIdentifier(storageId, spaceId, accountId, previousHeadIdentifier);
        }
    }
}