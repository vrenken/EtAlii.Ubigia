// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical;

using System.Threading.Tasks;

internal sealed class LogicalContext : ILogicalContext
{
    private bool _isDisposed;

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

    public async ValueTask DisposeAsync()
    {
        if (_isDisposed) return;

        await Options.FabricContext
            .DisposeAsync()
            .ConfigureAwait(false);

        _isDisposed = true;
    }
}
