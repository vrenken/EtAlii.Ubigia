namespace EtAlii.Ubigia.Api.Logical
{
    using System.Collections.Generic;

    public interface IGraphPathPartTraverser
    {
        void Configure(TraversalParameters parameters);
        IAsyncEnumerable<Identifier> Traverse(GraphPathPart part, Identifier start, IPathTraversalContext context, ExecutionScope scope);
    }
}