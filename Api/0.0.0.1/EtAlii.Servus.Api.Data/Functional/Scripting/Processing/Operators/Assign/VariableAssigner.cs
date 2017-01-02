namespace EtAlii.Servus.Api.Data
{
    using System.Collections;
    using EtAlii.Servus.Api.Data.Model;
    using System;
    using System.Linq;
    using System.Collections.Generic;

    internal class VariableAssigner : IAssigner
    {
        private readonly IEnumerable<Func<object, string, ProcessParameters<Operator, SequencePart>, bool>> _variableOptions;
        private readonly ProcessingContext _context;

        public VariableAssigner(ProcessingContext context)
        {
            _context = context;
            _variableOptions = new Func<object, string, ProcessParameters<Operator, SequencePart>, bool>[]
            {
                (o,n,p) => TryOutputToVariable<INode>(o,n,p),
                (o,n,p) => TryOutputToVariable<IReadOnlyEntry>(o,n,p),
                (o,n,p) => TryOutputToVariable<Identifier>(o,n,p),
                (o,n,p) => OutputToVariable(o,n,p),
            };
        }

        public object Assign(ProcessParameters<Operator, SequencePart> parameters)
        {
            var variableSubject = parameters.LeftPart as VariableSubject;
            _variableOptions.First(f => f(parameters.RightResult, variableSubject.Name, parameters) == true);

            ScopeVariable variable = null;
            _context.Scope.Variables.TryGetValue(variableSubject.Name, out variable);
            return variable != null ? variable.Value : null;
        }

        private bool TryOutputToVariable<T>(object items, string variableName, ProcessParameters<Operator, SequencePart> parameters)
        {
            var success = false;
            if (items is IEnumerable<T>)
            {
                var enumerableItems = items as IEnumerable<T>;
                if (!HasMultiple(enumerableItems))
                {
                    SetVariable(enumerableItems.FirstOrDefault(), variableName, parameters);
                    success = true;
                }
                else
                {
                    SetVariable(enumerableItems, variableName, parameters);
                    success = true;
                }
            }
            return success;
        }

        private bool OutputToVariable(object items, string variableName, ProcessParameters<Operator, SequencePart> parameters)
        {
            SetVariable(items, variableName, parameters);
            return true;
        }

        private void SetVariable(object o, string variableName, ProcessParameters<Operator, SequencePart> parameters)
        {
            if (o != null)
            {
                var source = DetermineSource(parameters);
                var type = DetermineType(parameters);
                var variable = new ScopeVariable(o, source, type);
                _context.Scope.Variables[variableName] = variable;
            }
            else
            {
                _context.Scope.Variables.Remove(variableName);
            }
        }


        private string DetermineSource(ProcessParameters<Operator, SequencePart> parameters)
        {
            return parameters.RightPart != null ? parameters.RightPart.ToString() : "Unknown";
        }
        private Type DetermineType(ProcessParameters<Operator, SequencePart> parameters)
        {
            return parameters.RightResult != null ? parameters.RightResult.GetType() : null;
        }

        // TODO: Refactor to extension method.
        private bool HasMultiple<T>(IEnumerable<T> items)
        {
            var count = 0;
            foreach (var item in items)
            {
                count++;
                if (count > 1)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
