// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    internal interface IInternalNode : INode
    {
        /// <summary>
        /// The properties that make up the node.
        /// </summary>
        PropertyDictionary Properties { get; }

        /// <summary>
        /// The graph entry that the node is wrapping.
        /// </summary>
        IReadOnlyEntry Entry { get; }
    }
}
