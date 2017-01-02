namespace EtAlii.Servus.Api.Data
{
    using System;
    using EtAlii.Servus.Api;

    internal class VariableAddItemHandler : ActionHandlerBase<VariableAddItem>
    {
        private readonly IAddItemHelper _addItemHelper;
        private readonly IPathHelper _pathHelper;
        private readonly ScriptScope _scope;

        public VariableAddItemHandler(
            IAddItemHelper addItemHelper,
            IPathHelper pathHelper, 
            ScriptScope scope)
        {
            _addItemHelper = addItemHelper;
            _pathHelper = pathHelper;
            _scope = scope;
        }

        public override void Handle(VariableAddItem action)
        {
            var entry = _pathHelper.GetEntry(action.Path);
            var node = _addItemHelper.AddNewEntry(action, entry);

            var path = String.Join("/", action.Path.ToString(), action.ItemPath.ToString());
            _scope.Variables[action.Variable] = new ScopeVariable(node, path);
        }
    }
}
