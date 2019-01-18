namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Threading.Tasks;

    internal class AssignRootToOutputOperatorSubProcessor : IAssignRootToOutputOperatorSubProcessor
    {
        public async Task Assign(OperatorParameters parameters)
        {
            parameters.RightInput.Subscribe(
                onError: (e) => parameters.Output.OnError(e),
                onCompleted: () => parameters.Output.OnCompleted(),
                onNext: o => parameters.Output.OnNext(o));
            
            await Task.CompletedTask;
        }
    }
}