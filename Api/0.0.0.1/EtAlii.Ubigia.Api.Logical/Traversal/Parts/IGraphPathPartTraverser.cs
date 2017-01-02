namespace EtAlii.Ubigia.Api.Logical
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IGraphPathPartTraverser
    {
        void Configure(TraversalParameters parameters);
        Task<IEnumerable<Identifier>> Traverse(GraphPathPart part, Identifier start, ITraversalContext context, ExecutionScope scope);
    }
}