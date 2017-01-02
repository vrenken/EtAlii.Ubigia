namespace EtAlii.Servus.Api.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    internal class UpdateItemHandler : ActionHandlerBase<UpdateItem>
    {
        private readonly UpdateItemHelper _updateItemHelper;
        private readonly ScriptScope _scope;

        public UpdateItemHandler(
            UpdateItemHelper updateItemHelper, 
            ScriptScope scope)
        {
            _updateItemHelper = updateItemHelper;
            _scope = scope;
        }

        public override void Handle(UpdateItem action)
        {
            string variableName = action.UpdateVariable;
            ScopeVariable variable;
            if (!_scope.Variables.TryGetValue(variableName, out variable))
            {
                var message = String.Format("Variable '{0}' not set.", variableName);
                throw new InvalidOperationException(message);
            }

            var result = _updateItemHelper.Update(variable, variableName, action.Path);

            _scope.Output(result);
        }

    }
}
