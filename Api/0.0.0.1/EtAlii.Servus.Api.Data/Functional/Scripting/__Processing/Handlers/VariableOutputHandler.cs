namespace EtAlii.Servus.Api.Data
{
    internal class VariableOutputHandler : ActionHandlerBase<VariableOutput>
    {
        private readonly ScriptScope _scope;

        public VariableOutputHandler(ScriptScope scope)
        {
            _scope = scope;
        }

        public override void Handle(VariableOutput action)
        {
            ScopeVariable variable;
            if(_scope.Variables.TryGetValue(action.Variable, out variable))
            {
                _scope.Output(variable.Value);
            }
        }
    }
}
