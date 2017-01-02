namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Structure;

    class PathSubjectPartContentGetter : IPathSubjectPartContentGetter
    {
        private readonly ISelector<PathSubjectPart, Func<PathSubjectPart, IScriptScope, string>> _selector;

        public PathSubjectPartContentGetter()
        {
            _selector = new Selector<PathSubjectPart, Func<PathSubjectPart, IScriptScope, string>>()
                .Register(part => part is ConstantPathSubjectPart, GetConstantPathSubjectPartContent)
                .Register(part => part is VariablePathSubjectPart, GetVariablePathSubjectPartContent)
                .Register(part => true, (part, scope) => null);
        }

        public string GetPartContent(PathSubjectPart part, IScriptScope scope)
        {
            var getter = _selector.Select(part);
            return getter(part, scope);
        }

        private string GetVariablePathSubjectPartContent(PathSubjectPart part, IScriptScope scope)
        {
            var variablePathSubjectPart = (VariablePathSubjectPart)part;
            ScopeVariable variable;
            if (scope.Variables.TryGetValue(variablePathSubjectPart.Name,
                out variable))
            {
                object variableValue = null;
                var task = Task.Run(async () =>
                {
                    variableValue = await variable.Value.SingleAsync();
                });
                task.Wait();
                return variableValue?.ToString();
            }
            return null;
        }

        private string GetConstantPathSubjectPartContent(PathSubjectPart part, IScriptScope scope)
        {
            var constantPathSubjectPart = (ConstantPathSubjectPart)part;
            return constantPathSubjectPart.Name;
        }

    }
}