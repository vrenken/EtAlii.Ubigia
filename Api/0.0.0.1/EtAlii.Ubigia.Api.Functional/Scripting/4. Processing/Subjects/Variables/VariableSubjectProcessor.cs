namespace EtAlii.Ubigia.Api.Functional
{
    using System;

    internal class VariableSubjectProcessor : IVariableSubjectProcessor
    {
        private readonly IProcessingContext _context;

        public VariableSubjectProcessor(IProcessingContext context)
        {
            _context = context;
        }

        public void Process(Subject subject, ExecutionScope scope, IObserver<object> output)
        {
            var variableName = ((VariableSubject)subject).Name;
            ScopeVariable variable;

            if (_context.Scope.Variables.TryGetValue(variableName, out variable))
            {
                variable.Value.Subscribe(
                    onError: (e) => output.OnError(e),
                    onCompleted: () => output.OnCompleted(),
                    onNext: (o) => output.OnNext(o));
            }
            else
            {
                output.OnCompleted();
                //string message = String.Format("Variable {0} not set (subject: {0})", variableName, parameters.Target.ToString());
                //throw new ScriptParserException(message);
            }
        }
    }
}
