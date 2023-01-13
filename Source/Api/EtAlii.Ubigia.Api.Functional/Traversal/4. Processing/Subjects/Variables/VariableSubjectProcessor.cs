// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using System;
using System.Threading.Tasks;

internal class VariableSubjectProcessor : IVariableSubjectProcessor
{
    public Task Process(Subject subject, ExecutionScope scope, IObserver<object> output)
    {
        var variableName = ((VariableSubject)subject).Name;

        if (scope.Variables.TryGetValue(variableName, out var variable))
        {
            variable.Value.Subscribe(
                onError: output.OnError,
                onCompleted: output.OnCompleted,
                onNext: output.OnNext);
        }
        else
        {
            output.OnCompleted();
            //string message = string.Format("Variable [0] not set (subject: [0])", variableName, parameters.Target.ToString())
            //throw new ScriptParserException(message)
        }
        return Task.CompletedTask;
    }
}
