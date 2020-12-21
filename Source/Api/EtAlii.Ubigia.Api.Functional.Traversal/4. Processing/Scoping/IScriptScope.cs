namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Collections.Generic;

    public interface IScriptScope
    {
        /// <summary>
        /// The recent value of the variables used in the script.
        /// </summary>
        Dictionary<string, ScopeVariable> Variables { get; }
    }
}
