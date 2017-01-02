namespace EtAlii.Servus.Api.Fabric
{
    using System;
    using EtAlii.Servus.Api.Transport;

    public interface IFabricContext : IDisposable
    {
        IFabricContextConfiguration Configuration { get; }

        IDataConnection Connection { get; }
        IRootContext Roots { get; }
        IEntryContext Entries { get; }
        IContentContext Content { get; }
        IPropertyContext Properties { get; }
    }
}