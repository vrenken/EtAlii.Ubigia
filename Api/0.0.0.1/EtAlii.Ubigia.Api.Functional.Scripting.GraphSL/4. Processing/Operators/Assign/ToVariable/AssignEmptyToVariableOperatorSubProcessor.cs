namespace EtAlii.Ubigia.Api.Functional
{
    using System;

    public class AssignEmptyToVariableOperatorSubProcessor : IAssignEmptyToVariableOperatorSubProcessor
    {
        private readonly IProcessingContext _context;

        public AssignEmptyToVariableOperatorSubProcessor(IProcessingContext context)
        {
            _context = context;
        }

        public void Assign(OperatorParameters parameters)
        {
            var variableSubject = (VariableSubject)parameters.LeftSubject;
            //var subject = parameters.RightSubject;
            //var source = subject.ToString();

            var variableName = variableSubject.Name;

            _context.Scope.Variables.Remove(variableName);

            parameters.RightInput.Subscribe(
                onError: (e) => parameters.Output.OnError(e),
                onCompleted: () => parameters.Output.OnCompleted(),
                onNext: o => parameters.Output.OnNext(o));
        }
    }
}