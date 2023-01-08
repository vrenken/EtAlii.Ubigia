// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Fabric;

    public class NextIdentifierGetter : INextIdentifierGetter
    {
        private readonly IFabricContext _fabric;

        public NextIdentifierGetter(
            IFabricContext fabric)
        {
            _fabric = fabric;
        }

        /// <inheritdoc />
        public async Task<Identifier> GetNext(Identifier previousHeadIdentifier)
        {
            return await _fabric.Identifiers
                .GetNextIdentifierForPreviousHeadIdentifier(previousHeadIdentifier.Storage, previousHeadIdentifier.Account, previousHeadIdentifier.Space, previousHeadIdentifier)
                .ConfigureAwait(false);
        }
    }
}
