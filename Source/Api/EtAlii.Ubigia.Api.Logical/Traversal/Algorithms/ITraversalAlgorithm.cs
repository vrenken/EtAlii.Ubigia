// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System;
    using System.Threading.Tasks;

    public interface ITraversalAlgorithm
    {
        Task Traverse(
            GraphPath graphPath,
            Identifier current,
            IPathTraversalContext context,
            ExecutionScope scope,
            IObserver<Identifier> finalOutput);
    }
}