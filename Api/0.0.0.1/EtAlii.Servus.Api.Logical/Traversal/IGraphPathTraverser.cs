namespace EtAlii.Servus.Api.Logical
{
    using System;

    public interface IGraphPathTraverser
    {
        void Traverse(GraphPath path, Traversal traversal, ExecutionScope scope, IObserver<IReadOnlyEntry> output, bool traverseToFinal = true);
    }
}