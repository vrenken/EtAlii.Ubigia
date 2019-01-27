namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Threading.Tasks;

    public class AssignEmptyToVariableOperatorSubProcessor : IAssignEmptyToVariableOperatorSubProcessor
    {
        private readonly IProcessingContext _context;

        public AssignEmptyToVariableOperatorSubProcessor(IProcessingContext context)
        {
            _context = context;
        }

        public async Task Assign(OperatorParameters parameters)
        {
            var variableSubject = (VariableSubject)parameters.LeftSubject;

            var variableName = variableSubject.Name;

            _context.Scope.Variables.Remove(variableName);

            parameters.RightInput.Subscribe(
                onError: (e) => parameters.Output.OnError(e),
                onCompleted: () => parameters.Output.OnCompleted(),
                onNext: o => parameters.Output.OnNext(o));

            await Task.CompletedTask;
        }
    }
}