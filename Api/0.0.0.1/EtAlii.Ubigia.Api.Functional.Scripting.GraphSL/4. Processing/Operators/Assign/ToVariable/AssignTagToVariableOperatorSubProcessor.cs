namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using EtAlii.Ubigia.Api.Logical;

    public class AssignTagToVariableOperatorSubProcessor : IAssignTagToVariableOperatorSubProcessor
    {
        private readonly IProcessingContext _context;

        public AssignTagToVariableOperatorSubProcessor(IProcessingContext context)
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
                onNext: o =>
                {
                    var entry = ((IInternalNode)o).Entry;
                    parameters.Output.OnNext(entry.Tag);
                });
        }
    }
}