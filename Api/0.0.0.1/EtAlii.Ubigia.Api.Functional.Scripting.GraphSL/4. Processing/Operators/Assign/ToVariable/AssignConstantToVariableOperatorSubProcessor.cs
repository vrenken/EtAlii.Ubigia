namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Threading.Tasks;

    internal class AssignConstantToVariableOperatorSubProcessor : IAssignConstantToVariableOperatorSubProcessor
    {
        private readonly IScriptProcessingContext _context;

        public AssignConstantToVariableOperatorSubProcessor(IScriptProcessingContext context)
        {
            _context = context;
        }

        public Task Assign(OperatorParameters parameters)
        {
            var variableSubject = (VariableSubject)parameters.LeftSubject;
            var subject = parameters.RightSubject;
            var source = subject.ToString();

            var variableName = variableSubject.Name;

            var variable = new ScopeVariable(parameters.RightInput, source);
            _context.Scope.Variables[variableName] = variable;

            parameters.RightInput.Subscribe(
                onError: (e) => parameters.Output.OnError(e),
                onCompleted: () => parameters.Output.OnCompleted(),
                onNext: o => parameters.Output.OnNext(o));
            return Task.CompletedTask;
        }
    }
}