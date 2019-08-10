namespace EtAlii.Ubigia.Api.Logical
{
    using System;

    public interface ILogicalContext : IDisposable
    {
        /// <summary>
        /// The Configuration used to instantiate this Context.
        /// </summary>
        ILogicalContextConfiguration Configuration { get; }

        /// <summary>
        /// The Nodes property provides logical access to graph nodes.
        /// </summary>
        ILogicalNodeSet Nodes { get; }
        ILogicalRootSet Roots { get; }
        IContentManager Content { get; }
        IPropertiesManager Properties { get; }
    }
}