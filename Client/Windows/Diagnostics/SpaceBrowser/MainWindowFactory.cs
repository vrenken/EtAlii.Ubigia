namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Api.Fabric.Diagnostics;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Functional.Diagnostics;
    using EtAlii.Ubigia.Api.Functional.NET47;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Logical.Diagnostics;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.Diagnostics;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Logging;
    using EtAlii.xTechnology.MicroContainer;

    public class MainWindowFactory
    {
        public IMainWindow Create(IProfilingDataConnection connection, IDiagnosticsConfiguration diagnostics)
        {
            var container = new Container();

            container.Register<IMainDispatcherInvoker, MainDispatcherInvoker>();

            container.Register<IDocumentsProvider, DocumentsProvider>();
            container.Register<IMainWindowViewModel, MainWindowViewModel>();
            container.Register<IMainWindow, MainWindow>();

            RegisterDiagnostics(container, diagnostics);
            RegisterContext(container, connection, diagnostics);

            container.Register<IRootsViewModel, RootsViewModel>();
            container.Register<IJournalViewModel, JournalViewModel>();

            container.Register<IDocumentContext, DocumentContext>();

            container.Register<IFunctionalGraphDocumentFactory, FunctionalGraphDocumentFactory>();
            container.Register<INewFunctionalGraphDocumentCommand, NewFunctionalGraphDocumentCommand>();

            container.Register<ILogicalGraphDocumentFactory, LogicalGraphDocumentFactory>();
            container.Register<INewLogicalGraphDocumentCommand, NewLogicalGraphDocumentCommand>();

            container.Register<ITreeDocumentFactory, TreeDocumentFactory>();
            container.Register<INewTreeDocumentCommand, NewTreeDocumentCommand>();

            container.Register<ISequentialDocumentFactory, SequentialDocumentFactory>();
            container.Register<INewSequentialDocumentCommand, NewSequentialDocumentCommand>();

            container.Register<ITemporalDocumentFactory, TemporalDocumentFactory>();
            container.Register<INewTemporalDocumentCommand, NewTemporalDocumentCommand>();

            container.Register<IGraphScriptLanguageDocumentFactory, GraphScriptLanguageDocumentFactory>();
            container.Register<INewGraphScriptLanguageDocumentCommand, NewGraphScriptLanguageDocumentCommand>();

            container.Register<IGraphQueryLanguageDocumentFactory, GraphQueryLanguageDocumentFactory>();
            container.Register<INewGraphQueryLanguageDocumentCommand, NewGraphQueryLanguageDocumentCommand>();

            container.Register<ICodeDocumentFactory, CodeDocumentFactory>();
            container.Register<INewCodeDocumentCommand, NewCodeDocumentCommand>();

            container.Register<IProfilingDocumentFactory, ProfilingDocumentFactory>();
            container.Register<INewProfilingDocumentCommand, NewProfilingDocumentCommand>();

            container.Register<IGraphContextFactory, GraphContextFactory>();

            var window = container.GetInstance<IMainWindow>();
            var viewModel = container.GetInstance<IMainWindowViewModel>();
            window.DataContext = viewModel;
            return window;
        }

        private void RegisterContext(Container container, IProfilingDataConnection connection, IDiagnosticsConfiguration diagnostics)
        {
            // We start with the connection.
            container.Register<IDataConnection>(() => connection);
            container.Register(() => connection);

            // Then the fabric context.
            var fabricContextConfiguration = new FabricContextConfiguration()
                .Use(connection);
            var fabricContext = new FabricContextFactory().CreateForProfiling(fabricContextConfiguration);
            container.Register<IFabricContext>(() => fabricContext);
            container.Register(() => fabricContext);

            // The logical context.
            container.Register<ILogicalContext>(() =>
            {
                var logicalContextConfiguration = new LogicalContextConfiguration()
                    .Use(fabricContext)
                    .Use(diagnostics);
                return new LogicalContextFactory().CreateForProfiling(logicalContextConfiguration);
            });
            container.Register(() => (IProfilingLogicalContext)container.GetInstance<ILogicalContext>());

            // Function handling
            container.Register<ISpaceBrowserFunctionHandlersProvider, SpaceBrowserFunctionHandlersProvider>();
            container.Register<IViewFunctionHandler, ViewFunctionHandler>();

            container.Register<IGraphSLScriptContext>(() =>
            {
                var logicalContext = container.GetInstance<ILogicalContext>();
                var configuration = new GraphSLScriptContextConfiguration()
                    .Use(logicalContext)
                    .Use(diagnostics)
                    .Use(container.GetInstance<ISpaceBrowserFunctionHandlersProvider>())
                    .UseNET47();
                return new GraphSLScriptContextFactory().CreateForProfiling(configuration);
            });
            container.Register(() => (IProfilingGraphSLScriptContext)container.GetInstance<IGraphSLScriptContext>());

            container.Register<IGraphQLQueryContext>(() =>
            {
                var logicalContext = container.GetInstance<ILogicalContext>();
                var configuration = new GraphQLQueryContextConfiguration()
                    .Use(logicalContext)
                    .Use(diagnostics);
                return new GraphQLQueryContextFactory().CreateForProfiling(configuration);
            });
            container.Register(() => (IProfilingGraphQLQueryContext)container.GetInstance<IGraphQLQueryContext>());
            
            container.Register<ILinqQueryContext>(() =>
            {
                var logicalContext = container.GetInstance<ILogicalContext>();
                var configuration = new LinqQueryContextConfiguration()
                    .Use(logicalContext)
                    .Use(diagnostics);
                return new LinqQueryContextFactory().CreateForProfiling(configuration);
            });
            container.Register(() => (IProfilingLinqQueryContext)container.GetInstance<ILinqQueryContext>());

        }

        private void RegisterDiagnostics(Container container, IDiagnosticsConfiguration diagnostics)
        {
            container.Register(() => diagnostics);
            container.Register(() => container.GetInstance<IDiagnosticsConfiguration>().CreateLogFactory());
            container.Register(() =>
            {
                var factory = container.GetInstance<ILogFactory>();
                return container.GetInstance<IDiagnosticsConfiguration>().CreateLogger(factory);
            });
        }
    }
}
