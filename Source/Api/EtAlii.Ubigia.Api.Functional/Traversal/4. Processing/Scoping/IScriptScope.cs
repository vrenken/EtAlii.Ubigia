// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

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
