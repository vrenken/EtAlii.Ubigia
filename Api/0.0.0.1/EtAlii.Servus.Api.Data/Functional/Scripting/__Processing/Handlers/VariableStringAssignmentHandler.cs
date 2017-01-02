namespace EtAlii.Servus.Api.Data
{
    internal class VariableStringAssignmentHandler : ActionHandlerBase<VariableStringAssignment>
    {
        private readonly ScriptScope _scope;

        public VariableStringAssignmentHandler(ScriptScope scope)
        {
            _scope = scope;
        }

        public override void Handle(VariableStringAssignment action)
        {
            _scope.Variables[action.Variable] = new ScopeVariable(action.Text, action.Text);
        }
    }
}
