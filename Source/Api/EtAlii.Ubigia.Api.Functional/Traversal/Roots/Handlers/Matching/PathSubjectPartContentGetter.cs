namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Reactive.Linq;
    using System.Threading.Tasks;

    internal class PathSubjectPartContentGetter : IPathSubjectPartContentGetter
    {
        public Task<string> GetPartContent(PathSubjectPart part, IScriptScope scope)
        {
            return part switch
            {
                ConstantPathSubjectPart constantPathSubjectPart => GetConstantPathSubjectPartContent(constantPathSubjectPart),
                VariablePathSubjectPart variablePathSubjectPart => GetVariablePathSubjectPartContent(variablePathSubjectPart, scope),
                {} when true => Task.FromResult((string)null),
                _ => throw new NotSupportedException($"Cannot find path content in: {part}")
            };
        }

        private async Task<string> GetVariablePathSubjectPartContent(VariablePathSubjectPart part, IScriptScope scope)
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
}
