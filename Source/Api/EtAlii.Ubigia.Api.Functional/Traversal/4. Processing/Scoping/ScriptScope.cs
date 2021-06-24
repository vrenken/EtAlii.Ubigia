// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Collections.Generic;

    /// <summary>
    /// This class defines a scope in which a script can execute.
    /// It can be used to find variables or entries used by the script.
    /// </summary>
    public class ScriptScope : IScriptScope
    {
        /// <summary>
        /// The recent value of the variables used in the script.
        /// </summary>
        public Dictionary<string, ScopeVariable> Variables { get; }

        /// <summary>
        /// Create a new ScriptScope instance.
        /// Assign a Action to the output parameter to retrieve and process the results of the script.
        /// </summary>
        public ScriptScope()
        {
            Variables = new Dictionary<string, ScopeVariable>();
        }

        /// <summary>
        /// Create a new ScriptScope instance using the variables provided by the dictionary.
        /// </summary>
        public ScriptScope(Dictionary<string, ScopeVariable> variables)
        {
            Variables = variables;
        }
    }
}
