namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Structure;

    class PathSubjectPartContentGetter : IPathSubjectPartContentGetter
    {
        private readonly ISelector<PathSubjectPart, Func<PathSubjectPart, IScriptScope, Task<string>>> _selector;

        public PathSubjectPartContentGetter()
        {
            _selector = new Selector<PathSubjectPart, Func<PathSubjectPart, IScriptScope, Task<string>>>()
                .Register(part => part is ConstantPathSubjectPart, GetConstantPathSubjectPartContent)
                .Register(part => part is VariablePathSubjectPart, GetVariablePathSubjectPartContent)
                .Register(part => true, (part, scope) => Task.FromResult((string)null));
        }

        public Task<string> GetPartContent(PathSubjectPart part, IScriptScope scope)
        {
            var getter = _selector.Select(part);
            return getter(part, scope);
        }

        private async Task<string> GetVariablePathSubjectPartContent(PathSubjectPart part, IScriptScope scope)
        {
            var variablePathSubjectPart = (VariablePathSubjectPart)part;
            if (scope.Variables.TryGetValue(variablePathSubjectPart.Name, out var variable))
            {
                object variableValue = await variable.Value.SingleAsync();
                return variableValue?.ToString();
            }
            return null;
        }

        private Task<string> GetConstantPathSubjectPartContent(PathSubjectPart part, IScriptScope scope)
        {
            var constantPathSubjectPart = (ConstantPathSubjectPart)part;
            return Task.FromResult(constantPathSubjectPart.Name);
        }

    }
}