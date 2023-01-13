// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric;

using System.Threading.Tasks;
using EtAlii.Ubigia.Api.Transport;

public sealed class FabricContext : IFabricContext
{
    private bool _disposed;

    /// <inheritdoc/>
    public FabricOptions Options { get; }

    /// <inheritdoc/>
    public IDataConnection Connection { get; }

    /// <inheritdoc/>
    public IRootContext Roots { get; }

    /// <inheritdoc/>
    public IEntryContext Entries { get; }

    /// <inheritdoc/>
    public IContentContext Content { get; }

    /// <inheritdoc/>
    public IPropertiesContext Properties { get; }

    public FabricContext(
        FabricOptions options,
        IEntryContext entries,
        IRootContext roots,
        IContentContext content,
        IDataConnection connection,
        IPropertiesContext properties)
    {
        Options = options;
        Entries = entries;
        Roots = roots;
        Content = content;
        Connection = connection;
        Properties = properties;
    }

    public async ValueTask DisposeAsync()
    {
        if (_disposed) return;

        // Free other state (managed objects).
        if (Connection.IsConnected)
        {
            await Connection.Close().ConfigureAwait(false);
        }

        _disposed = true;
    }
}
