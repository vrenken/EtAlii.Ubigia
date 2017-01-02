namespace EtAlii.Servus.Api.Data
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// This class defines a scope in which a script can execute.
    /// It can be used to find variables or entries used by the script.
    /// </summary>
    public class ScriptScope
    {
        /// <summary>
        /// The recent value of the variables used in the script.
        /// </summary>
        public Dictionary<string, ScopeVariable> Variables { get { return _variables; } }
        private readonly Dictionary<string, ScopeVariable> _variables;

        /// <summary>
        /// The set of entries used by the script, accessible by identifier.
        /// </summary>
        public Dictionary<Identifier, Entry> Entries { get { return _entries; } }
        private readonly Dictionary<Identifier, Entry> _entries;

        private readonly Action<object> _output;

        /// <summary>
        /// Create a new ScriptScope instance. 
        /// Assign a Action to the output parameter to retrieve and process the results of the script.
        /// </summary>
        /// <param name="output"></param>
        public ScriptScope(Action<object> output = null)
        {
            _variables = new Dictionary<string, ScopeVariable>();
            _entries = new Dictionary<Identifier, Entry>();
            _output = output ?? new Action<object>(o => { }); 
        }

        internal void Output(object objectToOutput)
        {
            _output(objectToOutput);
        }
    }
}
