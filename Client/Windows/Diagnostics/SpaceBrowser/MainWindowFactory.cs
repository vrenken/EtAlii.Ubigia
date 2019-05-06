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
            //viewModel.NewBlankDocumentCommands = CreateNewBlankDocumentCommands(container)
            window.DataContext = viewModel;
            return window;
        }

        private void RegisterContext(Container container, IProfilingDataConnection connection, IDiagnosticsConfiguration diagnostics)
        {
            // We start with the connection.
            container.Register<IDataConnection>(() => connection);
            container.Register(() => connection);

            // Function handling
            container.Register<ISpaceBrowserFunctionHandlersProvider, SpaceBrowserFunctionHandlersProvider>();
            container.Register<IViewFunctionHandler, ViewFunctionHandler>();

            var configuration = new GraphSLScriptContextConfiguration()
                .Use(connection)
                .UseLogicalDiagnostics(diagnostics)
                .UseFunctionalGraphSLDiagnostics(diagnostics)
                .Use(container.GetInstance<ISpaceBrowserFunctionHandlersProvider>())
                .UseDotNet47();
            
            // Then the fabric context.
            var fabricContext = new FabricContextFactory().CreateForProfiling(configuration);
            container.Register<IFabricContext>(() => fabricContext);
            container.Register(() => fabricContext);

            var logicalContext = new LogicalContextFactory().CreateForProfiling(configuration);
            container.Register<ILogicalContext>(() => logicalContext);
            container.Register(() => (IProfilingLogicalContext)logicalContext);


            container.Register<IGraphSLScriptContext>(() => new GraphSLScriptContextFactory().CreateForProfiling(configuration));
            container.Register(() => (IProfilingGraphSLScriptContext)container.GetInstance<IGraphSLScriptContext>());

            container.Register<IGraphQLQueryContext>(() =>
            {
                var queryContextConfiguration = new GraphQLQueryContextConfiguration()
                    .Use(configuration);
                return new GraphQLQueryContextFactory().CreateForProfiling(queryContextConfiguration);
            });
            container.Register(() => (IProfilingGraphQLQueryContext)container.GetInstance<IGraphQLQueryContext>());
            
            container.Register<ILinqQueryContext>(() =>
            {
                var queryContextConfiguration = new LinqQueryContextConfiguration()
                    .Use(configuration)
                    .UseFunctionalDiagnostics(diagnostics);
                return new LinqQueryContextFactory().CreateForProfiling(queryContextConfiguration);
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
            //container.Register<IProfilerFactory>(
            //    () => container.GetInstance<IDiagnosticsConfiguration>().CreateProfilerFactory())
            //container.Register<IProfiler>(() =>
            //[
            //    var factory = container.GetInstance<IProfilerFactory>()
            //    return container.GetInstance<IDiagnosticsConfiguration>().CreateProfiler(factory)
            //])
        }

        //private NewDocumentCommand[] CreateNewBlankDocumentCommands(Container container)
        //[
        //    return new[]
        //    [
        //        CreateNewDocumentCommand(container,
        //            new FunctionalGraphDocumentFactory(),
        //            "Functional graph",
        //            @"pack://siteoforigin:,,,/Images/Nodes.png",
        //            "Graph view [0]",
        //            "Create a document that shows a information stored in a space using a functional graph",
        //            "Useful for current state analysis",
        //            "Does not show temporal information"),
        //        CreateNewDocumentCommand(container,
        //            new LogicalGraphDocumentFactory(),
        //            "Logical graph",
        //            @"pack://siteoforigin:,,,/Images/Nodes.png",
        //            "Graph view [0]",
        //            "Create a document that shows a information stored in a space using a logical graph",
        //            "Useful for change analysis",
        //            "Shows temporal information"),
        //        CreateNewDocumentCommand(container,
        //            new TreeDocumentFactory(),
        //            "Hierarchical",
        //            @"pack://siteoforigin:,,,/Images/Tree.png",
        //            "Tree view [0]",
        //            "Create a document that shows information stored in a space hierarchically",
        //            "Useful for tree structure analysis",
        //            "Does not show temporal information"),
        //        CreateNewDocumentCommand(container,
        //            new SequentialDocumentFactory(),
        //            "Sequential",
        //            @"pack://siteoforigin:,,,/Images/View-Details.png",
        //            "Sequential view [0]",
        //            "Create a document to show information stored in a space sequentially",
        //            "Useful for order analysis",
        //            "Does not show temporal information"),
        //        CreateNewDocumentCommand(container,
        //            new TemporalDocumentFactory(),
        //            "Temporal",
        //            @"pack://siteoforigin:,,,/Images/Clock-01.png",
        //            "Temporal view [0]",
        //            "Create a document to show information stored in a space temporal",
        //            "Useful for temporal analysis",
        //            null),
        //        CreateNewDocumentCommand(container,
        //            new CodeDocumentFactory(),
        //            "Code",
        //            @"pack://siteoforigin:,,,/Images/File-Format-CSharp.png",
        //            "Code view [0]",
        //            "Create a document to interact with a space programmatically",
        //            "Useful for complex iterative or recursive activities",
        //            "Allows C# code to be tested"),
        //        CreateNewDocumentCommand(container,
        //            new ScriptDocumentFactory(),
        //            "Query",
        //            @"pack://siteoforigin:,,,/Images/File-Format-GraphQuery.png",
        //            "Query view [0]",
        //            "Create a document to invoke scripts on a space",
        //            "Allows execution scripts written in the GQL script language",
        //            "Useful for advanced space operations"),
        //        CreateNewDocumentCommand(container,
        //            new ProfilingDocumentFactory(),
        //            "Profiling",
        //            @"pack://siteoforigin:,,,/Images/Arrow.png",
        //            "Profiler view [0]",
        //            "Create a profiling document",
        //            "Shows profiling details of all API access to a space",
        //            "Useful for advanced query optimization"),
        //    ]
        //]
        //private NewDocumentCommand CreateNewDocumentCommand(
        //    Container container, 
        //    IDocumentFactory factory, 
        //    string header, 
        //    string icon, 
        //    string titleFormat, 
        //    string infoLine, 
        //    string infoTip1, 
        //    string infoTip2)
        //[
        //    var command = container.GetInstance<NewDocumentCommand>()
        //    command.DocumentFactory = factory
        //    command.Header = header
        //    command.Icon = icon
        //    command.TitleFormat = titleFormat
        //    command.InfoLine = infoLine
        //    command.InfoTip1 = infoTip1
        //    command.InfoTip2 = infoTip2
        //    return command
        //]
    }
}
