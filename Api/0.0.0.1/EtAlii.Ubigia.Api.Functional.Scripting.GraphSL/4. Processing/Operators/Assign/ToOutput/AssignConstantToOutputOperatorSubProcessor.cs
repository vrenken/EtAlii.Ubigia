namespace EtAlii.Ubigia.Api.Functional
{
    using System;

    internal class AssignConstantToOutputOperatorSubProcessor : IAssignConstantToOutputOperatorSubProcessor
    {
        public void Assign(OperatorParameters parameters)
        {
            parameters.RightInput.Subscribe(
                onError: (e) => parameters.Output.OnError(e),
                onCompleted: () => parameters.Output.OnCompleted(),
                onNext: o => parameters.Output.OnNext(o));
        }
    }
}