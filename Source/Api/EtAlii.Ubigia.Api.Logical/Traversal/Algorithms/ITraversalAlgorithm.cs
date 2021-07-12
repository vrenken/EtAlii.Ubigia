// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System.Collections.Generic;

    public interface ITraversalAlgorithm
    {
        IAsyncEnumerable<Identifier> Traverse(
            GraphPath graphPath,
            Identifier current,
            IPathTraversalContext context,
            ExecutionScope scope);
    }
}
