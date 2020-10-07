namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Api.Functional.Querying;
    using EtAlii.Ubigia.Api.Functional.Scripting;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.xTechnology.Diagnostics;

    public interface IDocumentContext
    {
        IGraphSLScriptContext ScriptContext { get; }
        IGraphQLQueryContext QueryContext { get; }
        ILinqQueryContext LinqContext { get; }
        ILogicalContext LogicalContext { get; }
        IFabricContext FabricContext { get; }
        IDataConnection Connection { get; }
        IDiagnosticsConfiguration Diagnostics { get; } 
        ILogger Logger { get; } 
        ILogFactory LogFactory { get; } 
        IJournalViewModel Journal { get; }
        IGraphContextFactory GraphContextFactory { get; }
    }
}
