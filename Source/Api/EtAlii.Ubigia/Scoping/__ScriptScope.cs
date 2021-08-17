// // Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia
//
// namespace EtAlii.Ubigia.Api.Functional
// {
//     using System.Collections.Generic;
//
//     /// <summary>
//     /// This class defines a scope in which a script can execute.
//     /// It can be used to find variables or entries used by the script.
//     /// </summary>
//     public class ExecutionScope : ExecutionScope
//     {
//         /// <summary>
//         /// The recent value of the variables used in the script.
//         /// </summary>
//         public Dictionary<string, ScopeVariable> Variables { get; }
//
//         /// <summary>
//         /// Create a new ExecutionScope instance.
//         /// Assign a Action to the output parameter to retrieve and process the results of the script.
//         /// </summary>
//         public ExecutionScope()
//         {
//             Variables = new Dictionary<string, ScopeVariable>();
//         }
//
//         /// <summary>
//         /// Create a new ExecutionScope instance using the variables provided by the dictionary.
//         /// </summary>
//         public ExecutionScope(Dictionary<string, ScopeVariable> variables)
//         {
//             Variables = variables;
//         }
//     }
// }
