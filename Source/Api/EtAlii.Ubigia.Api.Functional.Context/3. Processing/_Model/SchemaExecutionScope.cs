namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System.Collections.Generic;
    using EtAlii.Ubigia.Api.Functional.Traversal;

    public class SchemaExecutionScope
    {
        public IScriptScope ScriptScope { get; }

        public SchemaExecutionScope()
        {
            ScriptScope = new ScriptScope();
        }

        public SchemaExecutionScope(Dictionary<string, ScopeVariable> variables)
        {
            ScriptScope = new ScriptScope(variables);
        }
    }
}
