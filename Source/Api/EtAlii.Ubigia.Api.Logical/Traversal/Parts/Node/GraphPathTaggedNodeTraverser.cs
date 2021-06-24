// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System.Collections.Generic;

    internal class GraphPathTaggedNodeTraverser : IGraphPathTaggedNodeTraverser
    {
        public void Configure(TraversalParameters parameters)
        {
            var graphTaggedNode = (GraphTaggedNode) parameters.Part;
            var name = graphTaggedNode.Name;
            var tag = graphTaggedNode.Tag;

            parameters.Input.SubscribeAsync(
                onError: e => parameters.Output.OnError(e),
                onNext: async start =>
                {
                    if (start == Identifier.Empty)
                    {
                        throw new GraphTraversalException("Tagged node traversal cannot be done at the root of a graph");
                    }

                    var entry = await parameters.Context.Entries.Get(start, parameters.Scope).ConfigureAwait(false);

                    if (name != string.Empty && name != entry.Type)
                    {
                        return;
                    }

                    if (tag != string.Empty && tag != entry.Tag)
                    {
                        return;
                    }

                    parameters.Output.OnNext(entry.Id);
                },
                onCompleted: () => parameters.Output.OnCompleted());
        }

        public async IAsyncEnumerable<Identifier> Traverse(GraphPathPart part, Identifier start, IPathTraversalContext context, ExecutionScope scope)
        {
            var graphTaggedNode = (GraphTaggedNode)part;
            var name = graphTaggedNode.Name;
            var tag = graphTaggedNode.Tag;

            if (start == Identifier.Empty)
            {
                throw new GraphTraversalException("Tagged node traversal cannot be done at the root of a graph");
            }
            var entry = await context.Entries.Get(start, scope).ConfigureAwait(false);
            if (name != string.Empty && name != entry.Type)
            {
                yield break;
            }
            if (tag != string.Empty && tag != entry.Tag)
            {
                yield break;
            }

            yield return entry.Id;
        }

    }
}