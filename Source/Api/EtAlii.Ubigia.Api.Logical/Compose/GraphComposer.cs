// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System.Threading.Tasks;

    public class GraphComposer : IGraphComposer
    {
        private readonly IGraphAdder _graphAdder;
        private readonly IGraphRemover _graphRemover;
        private readonly IGraphLinker _graphLinker;
        private readonly IGraphUnlinker _graphUnlinker;
        private readonly IGraphRenamer _graphRenamer;

        public GraphComposer(
            IGraphAdder graphAdder,
            IGraphRemover graphRemover,
            IGraphLinker graphLinker,
            IGraphUnlinker graphUnlinker,
            IGraphRenamer graphRenamer)
        {
            _graphAdder = graphAdder;
            _graphRemover = graphRemover;
            _graphLinker = graphLinker;
            _graphUnlinker = graphUnlinker;
            _graphRenamer = graphRenamer;
        }

        public async Task<IReadOnlyEntry> Add(Identifier parent, string child, ExecutionScope scope)
        {
            return await _graphAdder.Add(parent, child, scope).ConfigureAwait(false);
        }

        public async Task<IReadOnlyEntry> Add(Identifier parent, Identifier child, ExecutionScope scope)
        {
            return await _graphAdder.Add(parent, child, scope).ConfigureAwait(false);
        }

        public async Task<IReadOnlyEntry> Remove(Identifier parent, string child, ExecutionScope scope)
        {
            return await _graphRemover.Remove(parent, child, scope).ConfigureAwait(false);
        }

        public async Task<IReadOnlyEntry> Remove(Identifier parent, Identifier child, ExecutionScope scope)
        {
            return await _graphRemover.Remove(parent, child, scope).ConfigureAwait(false);
        }

        public async Task<IReadOnlyEntry> Link(Identifier location, string itemName, Identifier item, ExecutionScope scope)
        {
            return await _graphLinker.Link(location, itemName, item, scope).ConfigureAwait(false);
        }

        public async Task<IReadOnlyEntry> Unlink(Identifier location, string itemName, Identifier item, ExecutionScope scope)
        {
            return await _graphUnlinker.Unlink(location, itemName, item, scope).ConfigureAwait(false);
        }

        public async Task<IReadOnlyEntry> Rename(Identifier item, string newName, ExecutionScope scope)
        {
            return await _graphRenamer.Rename(item, newName, scope).ConfigureAwait(false);
        }
    }
}
