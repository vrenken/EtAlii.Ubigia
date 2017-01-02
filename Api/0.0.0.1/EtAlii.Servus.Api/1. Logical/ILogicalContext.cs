namespace EtAlii.Servus.Api.Logical
{
    using System;

    public interface ILogicalContext : IDisposable
    {
        ILogicalContextConfiguration Configuration { get; }

        ILogicalNodeSet Nodes { get; }
        ILogicalRootSet Roots { get; }
        IContentManager Content { get; }
        IPropertiesManager Properties { get; }
    }
}