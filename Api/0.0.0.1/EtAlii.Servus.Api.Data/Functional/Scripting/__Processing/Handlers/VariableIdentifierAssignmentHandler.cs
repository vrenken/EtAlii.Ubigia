namespace EtAlii.Servus.Api.Data
{
    using System;

    public class VariableIdentifierAssignmentHandler : ActionHandlerBase<VariableIdentifierAssignment>
    {
        private readonly PathHelper _pathHelper;

        public VariableIdentifierAssignmentHandler(PathHelper pathHelper)
        {
            _pathHelper = pathHelper;
        }

        public override void Handle(VariableIdentifierAssignment action, ScriptScope scope, IDataConnection connection)
        {
            throw new NotImplementedException();
            //var node = _pathHelper.Get(action.Path, scope, connection);
            //var path = String.Format("/{0}", action.Path.ToString());
            //scope.Variables[action.Variable] = new ScopeVariable(node, path, typeof(DynamicNode));
        }
    }
}
