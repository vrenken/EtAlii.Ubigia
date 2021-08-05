// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.Ubigia.Api.Logical;
    using Microsoft.Extensions.Configuration;

    public interface ITraversalProcessorOptions : IExtensible
    {
        /// <summary>
        /// The client configuration root that will be used to configure the traversal processor.
        /// </summary>
        IConfiguration ConfigurationRoot { get; }

        public IScriptScope ScriptScope { get; }

        ILogicalContext LogicalContext { get; }

        bool CachingEnabled { get; }
    }
}
