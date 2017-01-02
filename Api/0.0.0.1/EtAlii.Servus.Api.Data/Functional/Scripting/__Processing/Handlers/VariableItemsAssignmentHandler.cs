namespace EtAlii.Servus.Api.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class VariableItemsAssignmentHandler : ActionHandlerBase<VariableItemsAssignment>
    {
        private readonly IPathHelper _pathHelper;
        private readonly ScriptScope _scope;

        public VariableItemsAssignmentHandler(
            IPathHelper pathHelper, 
            ScriptScope scope)
        {
            _pathHelper = pathHelper;
            _scope = scope;
        }

        public override void Handle(VariableItemsAssignment action)
        {
            var nodes = _pathHelper.GetChildren(action.Path);
            if (nodes.Any())
            {
                var path = String.Format("/{0}/", action.Path.ToString());
                _scope.Variables[action.Variable] = new ScopeVariable(nodes, path);
            }
        }
    }
}
