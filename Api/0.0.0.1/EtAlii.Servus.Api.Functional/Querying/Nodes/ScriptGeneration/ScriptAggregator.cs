namespace EtAlii.Servus.Api.Functional
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using EtAlii.Servus.Api.Logical;
    using Remotion.Linq.Clauses;

    public class ScriptAggregator : IScriptAggregator
    {
        private readonly Dictionary<string,string> _variableAssignments;

        public ScriptAggregator()
        {
            _variableAssignments = new Dictionary<string, string>();
        }

        public void AddAddItem(string path)
        {
            var previousVariableName = _variableAssignments.Keys.Last();
            var addItem = String.Format("${0} += {1}", previousVariableName, path);

            var variableName = CreateVariableName(_variableAssignments.Count);
            _variableAssignments.Add(variableName, addItem);
        }

        public void AddVariableAssignment(NodeQueryable<INode> nodeQuery)
        {
            var variableAssignment = String.Empty;
            if (nodeQuery.StartRoot != null && nodeQuery.StartPath == null)
            {
                variableAssignment = String.Format("/{0}", nodeQuery.StartRoot);
            }
            else if (nodeQuery.StartRoot != null && nodeQuery.StartPath != null)
            {
                variableAssignment = String.Format("/{0}/{1}", nodeQuery.StartRoot, nodeQuery.StartPath.TrimStart('/'));
            }
            else if (nodeQuery.StartRoot == null && nodeQuery.StartPath != null)
            {
                variableAssignment = nodeQuery.StartPath;
            }
            else if (nodeQuery.StartIdentifier != Identifier.Empty)
            {
                variableAssignment = String.Format("/{0}", nodeQuery.StartIdentifier);
            }

            if(!_variableAssignments.ContainsValue(variableAssignment))
            {
                var variableName = CreateVariableName(_variableAssignments.Count);
                _variableAssignments.Add(variableName, variableAssignment);
            }
        }

        public void AddVariableAssignment(IQuerySource querySource)
        {
        }

        private string CreateVariableName(int count)
        {
            return String.Format("var{0}", count);
        }
        public string GetScript()
        {
            var sb = new StringBuilder();

            if (_variableAssignments.Count == 0)
            {
                throw new InvalidOperationException("A script must have at least one variable assignment.");
            }

            var variableName = String.Empty;
            foreach(var kvp in _variableAssignments)
            {
                variableName = kvp.Key;
                sb.AppendFormat("${0} <= {1}{2}", kvp.Key, kvp.Value, Environment.NewLine);
            }

            // The last variable should be assigned to the output channel.
            sb.AppendFormat("<= ${0}", variableName);

            return sb.ToString();
        }

        public void AddUpdateItem(Identifier identifier, string updateVariableName)
        {
            var selectItem = String.Format("/&{0} <= ${1}", identifier.ToDotSeparatedString(), updateVariableName);

            var variableName = CreateVariableName(_variableAssignments.Count);
            _variableAssignments.Add(variableName, selectItem);
        }

        public void AddGetItem(Identifier identifier)
        {
            var selectItem = String.Format("/&{0}", identifier.ToDotSeparatedString());

            var variableName = CreateVariableName(_variableAssignments.Count);
            _variableAssignments.Add(variableName, selectItem);
        }

        internal void Clear()
        {
            _variableAssignments.Clear();
        }
    }
}