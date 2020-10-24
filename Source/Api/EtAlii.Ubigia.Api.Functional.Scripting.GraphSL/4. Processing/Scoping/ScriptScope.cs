﻿namespace EtAlii.Ubigia.Api.Functional.Scripting
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
    }
}
