// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System;
    using System.Threading.Tasks;

    public class GraphUnlinker : IGraphUnlinker
    {
        //private readonly IGraphChildAdder _graphChildAdder
        //private readonly IGraphLinkAdder _graphLinkAdder

        public Task<IReadOnlyEntry> Unlink(Identifier location, string itemName, Identifier item, ExecutionScope scope)
        {
            return Task.FromException<IReadOnlyEntry>(new NotImplementedException());
        }
    }
}
