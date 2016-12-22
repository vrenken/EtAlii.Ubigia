namespace EtAlii.Servus.Api.Functional
{
    using System;

    public class AssignCombinedToVariableOperatorSubProcessor : IAssignCombinedToVariableOperatorSubProcessor
    {
        private readonly IProcessingContext _context;

        public AssignCombinedToVariableOperatorSubProcessor(IProcessingContext context)
        {
            _context = context;
        }

        public void Assign(OperatorParameters parameters)
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
        }
    }
}