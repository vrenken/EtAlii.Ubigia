namespace EtAlii.Servus.Api.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Variable2ComponentExpander : IPath2ComponentExpander
    {
        private readonly ScriptScope _scope;

        private const char SeparatorCharacter = '/';

        public Variable2ComponentExpander(ScriptScope scope)
        {
            _scope = scope;
        }

        public bool CanExpand(PathSubjectPart part, int index)
        {
            return part is VariablePathSubjectPart;
        }

        public void Expand(PathSubjectPart part, int index, List<PathSubjectPart> path)
        {
            var variableName = ((VariablePathSubjectPart)part).Name;
            ScopeVariable variable;
            if (_scope.Variables.TryGetValue(variableName, out variable))
            {
                if (variable.Value is IInternalNode)
                {
                    if (index != 0)
                    {
                        var message = String.Format("Unable to expand node variable part in path: {0} (part: {1})", path.ToString(), part.ToString());
                        throw new InvalidOperationException(message);
                    }
                }
                else if (variable.Value is Identifier)
                {
                    if (index != 0)
                    {
                        var message = String.Format("Unable to expand identifier variable part in path: {0} (part: {1})", path.ToString(), part.ToString());
                        throw new InvalidOperationException(message);
                    }
                }
                else if (variable.Value is string)
                {
                    var pathPartsInVariable = ((string)variable.Value).Split(SeparatorCharacter).Select(s => new ConstantPathSubjectPart(s));
                    path.AddRange(pathPartsInVariable);
                }
                else if (variable.Value is IEnumerable<string>)
                {
                    var pathPartsInVariable = ((IEnumerable<string>)variable.Value).Select(s => new ConstantPathSubjectPart(s));
                    path.AddRange(pathPartsInVariable);
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