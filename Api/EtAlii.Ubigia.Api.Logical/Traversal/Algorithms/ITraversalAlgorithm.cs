namespace EtAlii.Ubigia.Api.Logical
{
    using System;
    using System.Threading.Tasks;

    public interface ITraversalAlgorithm
    {
        Task Traverse(
            GraphPath graphPath,
            Identifier current,
            ITraversalContext context,
            ExecutionScope scope,
            IObserver<Identifier> finalOutput);
    }
}