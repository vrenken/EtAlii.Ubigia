// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Threading.Tasks;

    public class AssignEmptyToVariableOperatorSubProcessor : IAssignEmptyToVariableOperatorSubProcessor
    {
        private readonly IScriptProcessingContext _context;

        public AssignEmptyToVariableOperatorSubProcessor(IScriptProcessingContext context)
        {
            _context = context;
        }

        public Task Assign(OperatorParameters parameters)
        {
            var variableSubject = (VariableSubject)parameters.LeftSubject;
            //var subject = parameters.RightSubject
            //var source = subject.ToString()

            var variableName = variableSubject.Name;

            _context.Scope.Variables.Remove(variableName);

            parameters.RightInput.Subscribe(
                onError: (e) => parameters.Output.OnError(e),
                onCompleted: () => parameters.Output.OnCompleted(),
                onNext: o => parameters.Output.OnNext(o));
            return Task.CompletedTask;
        }
    }
}
