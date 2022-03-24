// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System;

    internal sealed class LogicalContext : ILogicalContext
    {
        /// <inheritdoc/>
        public LogicalOptions Options { get; }

        /// <inheritdoc/>
        public ILogicalNodeSet Nodes { get; }

        /// <inheritdoc/>
        public ILogicalRootSet Roots { get; }

        /// <inheritdoc/>
        public IContentManager Content { get; }

        /// <inheritdoc/>
        public IPropertiesManager Properties { get; }

        public LogicalContext(
            LogicalOptions options,
            ILogicalNodeSet nodes,
            ILogicalRootSet roots,
            IContentManager content,
            IPropertiesManager properties)
        {
            Options = options;
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

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Dispose any relevant resources.
                // Maybe the connection should be disposed here?
            }

        }

        ~LogicalContext()
        {
            Dispose(false);
        }
    }
}
