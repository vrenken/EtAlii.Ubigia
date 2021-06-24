// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System;

    internal class LogicalContext : ILogicalContext
    {
        /// <inheritdoc/>
        public ILogicalContextConfiguration Configuration { get; }

        /// <inheritdoc/>
        public ILogicalNodeSet Nodes { get; }

        /// <inheritdoc/>
        public ILogicalRootSet Roots { get; }

        /// <inheritdoc/>
        public IContentManager Content { get; }

        /// <inheritdoc/>
        public IPropertiesManager Properties { get; }

        public LogicalContext(
            ILogicalContextConfiguration configuration,
            ILogicalNodeSet nodes,
            ILogicalRootSet roots,
            IContentManager content,
            IPropertiesManager properties)
        {
            Configuration = configuration;
            Nodes = nodes;
            Roots = roots;
            Content = content;
            Properties = properties;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Dispose any relevant resources.
            }

        }

        ~LogicalContext()
        {
            Dispose(false);
        }
    }
}
