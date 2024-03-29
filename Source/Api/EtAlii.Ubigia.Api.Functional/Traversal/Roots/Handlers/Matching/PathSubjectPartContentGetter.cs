// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using System.Reactive.Linq;
using System.Threading.Tasks;

internal class PathSubjectPartContentGetter : IPathSubjectPartContentGetter
{
    // Should this class really return null values if no decent string content can be acquired?
    // More info can be found in the Github item below:
    // https://github.com/vrenken/EtAlii.Ubigia/issues/69
    public Task<string> GetPartContent(PathSubjectPart part, ExecutionScope scope)
    {
        return part switch
        {
            ConstantPathSubjectPart constantPathSubjectPart => GetConstantPathSubjectPartContent(constantPathSubjectPart),
            VariablePathSubjectPart variablePathSubjectPart => GetVariablePathSubjectPartContent(variablePathSubjectPart, scope),
            _ => Task.FromResult((string)null),
        };
    }

    private async Task<string> GetVariablePathSubjectPartContent(VariablePathSubjectPart part, ExecutionScope scope)
    {
        if (scope.Variables.TryGetValue(part.Name, out var variable))
        {
            var variableValue = await variable.Value.SingleAsync();
            return variableValue?.ToString();
        }
        return null;
    }

    private Task<string> GetConstantPathSubjectPartContent(ConstantPathSubjectPart part)
    {
        return Task.FromResult(part.Name);
    }
}
