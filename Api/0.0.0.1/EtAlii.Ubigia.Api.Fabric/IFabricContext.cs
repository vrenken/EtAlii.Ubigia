namespace EtAlii.Ubigia.Api.Fabric
{
    using System;
    using EtAlii.Ubigia.Api.Transport;

    public interface IFabricContext : IDisposable
    {
        /// <summary>
        /// The Configuration used to instantiate this Context.
        /// </summary>
        IFabricContextConfiguration Configuration { get; }

        IDataConnection Connection { get; }
        IRootContext Roots { get; }
        IEntryContext Entries { get; }
        IContentContext Content { get; }
        IPropertiesContext Properties { get; }
    }
}