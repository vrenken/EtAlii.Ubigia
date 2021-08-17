// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Reactive.Linq;

    internal class VariableSubjectFunctionParameterConverter : IVariableSubjectFunctionParameterConverter
    {
        public object Convert(FunctionSubjectArgument argument, ExecutionScope scope)
        {
            var variableSubjectArgument = (VariableFunctionSubjectArgument)argument;

            object result = Observable.Empty<object>();
            var variableName = variableSubjectArgument.Name;
            if (scope.Variables.TryGetValue(variableName, out var variable))
            {
                result = variable.Value;
            }
            else
            {
                //string message = string.Format("Variable [0] not set (subject: [0])", variableName, parameters.Target.ToString())
                //throw new ScriptParserException(message)
            }
            return result;
        }
    }
}
