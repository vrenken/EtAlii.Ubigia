namespace EtAlii.Ubigia.Api.Fabric
{
    using System;
    using EtAlii.Ubigia.Api.Transport;

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