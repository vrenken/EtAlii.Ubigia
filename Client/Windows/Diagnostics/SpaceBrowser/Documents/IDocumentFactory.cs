namespace EtAlii.Servus.Client.Windows.Diagnostics
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Fabric;
    using EtAlii.Servus.Api.Functional;
    using EtAlii.Servus.Api.Logical;
    using EtAlii.Servus.Api.Transport;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Logging;

    public interface IDocumentFactory
    {
        IDocumentViewModel Create(
            IDataContext dataContext,
            ILogicalContext logicalContext,
            IFabricContext fabricContext,
            IDataConnection connection,
            IDiagnosticsConfiguration diagnostics, 
            ILogger logger, 
            ILogFactory logFactory, 
            IJournal journal);
    }
}
