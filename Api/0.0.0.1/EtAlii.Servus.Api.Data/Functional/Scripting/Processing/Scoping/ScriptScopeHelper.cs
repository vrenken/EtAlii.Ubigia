namespace EtAlii.Servus.Api.Data
{
    internal class ScriptScopeHelper : IScriptScopeHelper
    {
        private readonly ScriptScope _scope;

        public ScriptScopeHelper(ScriptScope scope)
        {
            _scope = scope;
        }

        public object GetAsVariable(VariableComponent variableComponent)
        {
            object value = null;
            ScopeVariable variable;
            var variableName = variableComponent.Name;
            if (_scope.Variables.TryGetValue(variableName, out variable))
            {
                value = variable.Value;
            }
            return value;
        }
    }
}
