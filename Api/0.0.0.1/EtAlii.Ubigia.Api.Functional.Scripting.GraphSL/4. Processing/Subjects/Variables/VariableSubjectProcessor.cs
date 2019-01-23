namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Threading.Tasks;

    internal class VariableSubjectProcessor : IVariableSubjectProcessor
    {
        private readonly IProcessingContext _context;

        public VariableSubjectProcessor(IProcessingContext context)
        {
            _context = context;
        }

        public async Task Process(Subject subject, ExecutionScope scope, IObserver<object> output)
        {
            var variableName = ((VariableSubject)subject).Name;

            if (_context.Scope.Variables.TryGetValue(variableName, out var variable))
            {
                variable.Value.Subscribe(
                    onError: output.OnError,
                    onCompleted: output.OnCompleted,
                    onNext: output.OnNext);
            }
            else
            {
                output.OnCompleted();
            }

            await Task.CompletedTask;
        }
    }
}
