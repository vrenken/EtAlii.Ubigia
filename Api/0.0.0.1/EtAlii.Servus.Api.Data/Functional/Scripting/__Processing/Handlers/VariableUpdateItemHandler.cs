namespace EtAlii.Servus.Api.Data
{
    using System;
    using EtAlii.Servus.Api;

    internal class VariableUpdateItemHandler : ActionHandlerBase<VariableUpdateItem>
    {
        private readonly UpdateItemHelper _updateItemHelper;
        private readonly ScriptScope _scope;

        public VariableUpdateItemHandler(
            UpdateItemHelper updateItemHelper,
            ScriptScope scope)
        {
            _updateItemHelper = updateItemHelper;
            _scope = scope;
        }

        public override void Handle(VariableUpdateItem action)
        {
            string variableName = action.UpdateVariable;
            ScopeVariable variable;
            if (!_scope.Variables.TryGetValue(variableName, out variable))
            {
                var message = String.Format("Variable '{0}' not set.", variableName);
                throw new InvalidOperationException(message);
            }

            //var entry = _pathHelper.GetEntry(action.Path, scope, connection);
            var node = _updateItemHelper.Update(variable, variableName, action.Path) as IInternalNode;

            var path = action.Path.ToString();
            _scope.Variables[action.Variable] = new ScopeVariable(node, path);
        }
    }
}
