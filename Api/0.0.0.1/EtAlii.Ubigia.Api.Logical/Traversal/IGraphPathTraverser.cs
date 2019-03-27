namespace EtAlii.Ubigia.Api.Logical
{
    using System;
    using System.Threading.Tasks;

    public interface IGraphPathTraverser
    {
        Task<IReadOnlyEntry> TraverseToSingle(Identifier identifier, ExecutionScope scope, bool traverseToFinal = true);
        void Traverse(GraphPath path, Traversal traversal, ExecutionScope scope, IObserver<IReadOnlyEntry> output, bool traverseToFinal = true);
    }
}