namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using EtAlii.Ubigia.Api.Logical;

    internal class AssignTagToOutputOperatorSubProcessor : IAssignTagToOutputOperatorSubProcessor
    {
        public void Assign(OperatorParameters parameters)
        {
            parameters.RightInput.Subscribe(
                onError: (e) => parameters.Output.OnError(e),
                onCompleted: () => parameters.Output.OnCompleted(),
                onNext: o =>
                {
                    var entry = ((IInternalNode)o).Entry;
                    parameters.Output.OnNext(entry.Tag);
                });
        }
    }
}