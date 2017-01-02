namespace EtAlii.Servus.Api.Data
{
    using System;

    internal class VariableItemAssignmentHandler : ActionHandlerBase<VariableItemAssignment>
    {
        private readonly IPathHelper _pathHelper;
        private readonly ScriptScope _scope;

        public VariableItemAssignmentHandler(
            IPathHelper pathHelper, 
            ScriptScope scope)
        {
            _pathHelper = pathHelper;
            _scope = scope;
        }

        public override void Handle(VariableItemAssignment action)
        {
            var node = _pathHelper.Get(action.Path);
            var path = String.Format("/{0}", action.Path.ToString());
            _scope.Variables[action.Variable] = new ScopeVariable(node, path);
        }
    }
}
