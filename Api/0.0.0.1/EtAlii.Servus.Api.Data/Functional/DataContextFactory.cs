namespace EtAlii.Servus.Api
{
    using EtAlii.Servus.Api.Data;
    using EtAlii.xTechnology.MicroContainer;
    using Remotion.Linq.Parsing.Structure;

    public class DataContextFactory
    {
        public DataContext Create(IDataConnection connection, IDiagnosticsConfiguration diagnostics = null)
        {
            var container = new Container();

            container.Register<DataContextConfiguration>(Lifestyle.Singleton);

            container.Register<IDataConnection>(() => connection, Lifestyle.Singleton);
            container.Register<IScriptParser>(() => new ScriptParserFactory().Create(diagnostics), Lifestyle.Singleton);
            container.Register<IScriptProcessor>(() => new ScriptProcessorFactory().Create(diagnostics), Lifestyle.Singleton);

            container.Register<IIndexSet, IndexSet>(Lifestyle.Singleton);

            container.Register<IRootSet, RootSet>(Lifestyle.Singleton);
            container.Register<RootQueryProviderFactory>(Lifestyle.Singleton);
            container.Register<RootQueryExecutor>(Lifestyle.Transient);

            container.Register<INodeSet, NodeSet>(Lifestyle.Singleton);
            container.Register<NodeQueryProviderFactory>(Lifestyle.Singleton);
            container.Register<NodeQueryExecutor>(Lifestyle.Transient);
            container.Register<NodeQueryModelVisitor>(Lifestyle.Transient);
            container.Register<ScriptAggregator>(Lifestyle.Transient);

            container.Register<NodeReloadCommand>(Lifestyle.Singleton);
            container.Register<NodeSaveCommand>(Lifestyle.Singleton);
            container.Register<NodeQuery>(Lifestyle.Singleton);
            container.Register<IChangeTracker, ChangeTracker>(Lifestyle.Singleton);

            container.Register<DataContext>(Lifestyle.Singleton);
            container.Register<IQueryParser, QueryParser>(QueryParser.CreateDefault);

            container.Register<ContentManagerFactory>(Lifestyle.Singleton);
            container.Register<IContentManager>(() => container.GetInstance<ContentManagerFactory>().Create(connection),Lifestyle.Singleton);

            return container.GetInstance<DataContext>();
        }
    }
}
