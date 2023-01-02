// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    /// <summary>
    /// The identity of the node to be added. Could be a variable.
    /// </summary>
    public record NodeIdentity
    {
        /// <summary>
        /// The name of the node to be added.
        /// </summary>
        public string Name { get; init; }

        /// <summary>
        /// True if the real name is stored in a ExecutionScope variable.
        /// </summary>
        public bool IsVariable { get; init; }

        public override string ToString()
        {
            return $"{(IsVariable ? "$" : "")}{Name}";
        }
    }
}
