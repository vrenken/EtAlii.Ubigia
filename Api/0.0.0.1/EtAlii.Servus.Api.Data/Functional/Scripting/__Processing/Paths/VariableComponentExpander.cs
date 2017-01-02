namespace EtAlii.Servus.Api.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class VariableComponentExpander : IPathComponentExpander
    {
        private readonly ScriptScope _scope;

        public VariableComponentExpander(ScriptScope scope)
        {
            _scope = scope;
        }

        public bool CanExpand(PathComponent pathComponent, int index)
        {
            return pathComponent is VariableComponent;
        }

        public void Expand(PathComponent pathComponent, int index, List<string> path, ref Identifier startIdentifier)
        {
            var variableName = ((VariableComponent)pathComponent).Name;
            ScopeVariable variable;
            if (_scope.Variables.TryGetValue(variableName, out variable))
            {
                if (variable.Value is IInternalNode)
                {
                    if (index == 0)
                    {
                        startIdentifier = ((IInternalNode)variable.Value).Entry.Id;
                    }
                    else
                    {
                        var message = String.Format("Unable to expand node variable component in path: {0} (component: {1})", path.ToString(), pathComponent.ToString());
                        throw new InvalidOperationException(message);
                    }
                }
                else if (variable.Value is Identifier)
                {
                    if (index == 0)
                    {
                        startIdentifier = (Identifier)variable.Value;
                    }
                    else
                    {
                        var message = String.Format("Unable to expand identifier variable component in path: {0} (component: {1})", path.ToString(), pathComponent.ToString());
                        throw new InvalidOperationException(message);
                    }
                }
                else if (variable.Value is string)
                {
                    var pathComponentsInVariable = ((string)variable.Value).Split(TerminalExpressions.SeparatorCharacter);
                    path.AddRange(pathComponentsInVariable);
                }
                else if (variable.Value is IEnumerable<string>)
                {
                    var pathComponentsInVariable = (IEnumerable<string>)variable.Value;
                    path.AddRange(pathComponentsInVariable);
                }
                else
                {
                    var message = String.Format("Unable to expand variable: {0} (Wrong type)", variableName);
                    throw new InvalidOperationException(message);
                }
            }
            else
            {
                var message = String.Format("Unable to use variable: {0} (Not set)", variableName);
                throw new InvalidOperationException(message);
            }
        }
    }
}