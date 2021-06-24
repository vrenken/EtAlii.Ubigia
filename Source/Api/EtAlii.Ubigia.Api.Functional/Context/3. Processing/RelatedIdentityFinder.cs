// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    /// <inheritdoc />
    internal class RelatedIdentityFinder : IRelatedIdentityFinder
    {
        /// <inheritdoc />
        public Identifier Find(Structure structure)
        {
            var node = structure.Node;
            if (node != null)
            {
                return node.Id;
            }
            var parent = structure.Parent;
            return parent != null
                ? Find(parent)
                : Identifier.Empty;
        }
    }
}
