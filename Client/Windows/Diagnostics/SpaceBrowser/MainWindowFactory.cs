namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using System;
    using EtAlii.Ubigia.Api.Diagnostics.Profiling;
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Api.Fabric.Diagnostics;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Functional.Diagnostics;
    using EtAlii.Ubigia.Api.Functional.Win32;
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

            container.Register<IScriptDocumentFactory, ScriptDocumentFactory>();
            container.Register<INewScriptDocumentCommand, NewScriptDocumentCommand>();

            container.Register<ICodeDocumentFactory, CodeDocumentFactory>();
            container.Register<INewCodeDocumentCommand, NewCodeDocumentCommand>();

            container.Register<IProfilingDocumentFactory, ProfilingDocumentFactory>();
            container.Register<INewProfilingDocumentCommand, NewProfilingDocumentCommand>();

            container.Register<IGraphContextFactory, GraphContextFactory>();

            var window = container.GetInstance<IMainWindow>();
            var viewModel = container.GetInstance<IMainWindowViewModel>();
            //viewModel.NewBlankDocumentCommands = CreateNewBlankDocumentCommands(container);
            window.DataContext = viewModel;
            return window;
        }

        private void RegisterContext(Container container, IProfilingDataConnection connection, IDiagnosticsConfiguration diagnostics)
        {
            // We start with the connection.
            container.Register<IDataConnection>(() => connection);
            container.Register<IProfilingDataConnection>(() => connection);

            // Then the fabric context.
            var fabricContextConfiguration = new FabricContextConfiguration()
                .Use(connection);
            var fabricContext = new FabricContextFactory().CreateForProfiling(fabricContextConfiguration);
            container.Register<IFabricContext>(() => fabricContext);
            container.Register<IProfilingFabricContext>(() => fabricContext);

            // The logical context.
            container.Register<ILogicalContext>(() =>
            {
                var logicalContextConfiguration = new LogicalContextConfiguration()
                    .Use(fabricContext)
                    .Use(diagnostics);
                return new LogicalContextFactory().CreateForProfiling(logicalContextConfiguration);
            });
            container.Register<IProfilingLogicalContext>(() => (IProfilingLogicalContext)container.GetInstance<ILogicalContext>());

            // Function handling
            container.Register<ISpaceBrowserFunctionHandlersProvider, SpaceBrowserFunctionHandlersProvider>();
            container.Register<IViewFunctionHandler, ViewFunctionHandler>();

            container.Register<IDataContext>(() =>
            {
                // And finally, the functional context.
                var dataContextConfiguration = new DataContextConfiguration()
                                    .Use(diagnostics)
                                    .Use(container.GetInstance<ILogicalContext>())
                                    .Use(container.GetInstance<ISpaceBrowserFunctionHandlersProvider>())
                                    .UseWin32();
                return new DataContextFactory().CreateForProfiling(dataContextConfiguration);
            });
            container.Register<IProfilingDataContext>(() => (IProfilingDataContext)container.GetInstance<IDataContext>());
        }

        private void RegisterDiagnostics(Container container, IDiagnosticsConfiguration diagnostics)
        {
            container.Register<IDiagnosticsConfiguration>(() => diagnostics);
            container.Register<ILogFactory>(() => container.GetInstance<IDiagnosticsConfiguration>().CreateLogFactory());
            container.Register<ILogger>(() =>
            {
                var factory = container.GetInstance<ILogFactory>();
                return container.GetInstance<IDiagnosticsConfiguration>().CreateLogger(factory);
            });
            //container.Register<IProfilerFactory>(
            //    () => container.GetInstance<IDiagnosticsConfiguration>().CreateProfilerFactory());
            //container.Register<IProfiler>(() =>
            //{
            //    var factory = container.GetInstance<IProfilerFactory>();
            //    return container.GetInstance<IDiagnosticsConfiguration>().CreateProfiler(factory);
            //});
        }

        //private NewDocumentCommand[] CreateNewBlankDocumentCommands(Container container)
        //{
        //    return new[]
        //    {
        //        CreateNewDocumentCommand(container,
        //            new FunctionalGraphDocumentFactory(),
        //            "Functional graph",
        //            @"\Images\Icons\Nodes.png",
        //            "Graph view {0}",
        //            "Create a document that shows a information stored in a space using a functional graph",
        //            "Usefull for current state analysis",
        //            "Does not show temporal information"),
        //        CreateNewDocumentCommand(container,
        //            new LogicalGraphDocumentFactory(),
        //            "Logical graph",
        //            @"\Images\Icons\Nodes.png",
        //            "Graph view {0}",
        //            "Create a document that shows a information stored in a space using a logical graph",
        //            "Usefull for change analysis",
        //            "Shows temporal information"),
        //        CreateNewDocumentCommand(container,
        //            new TreeDocumentFactory(),
        //            "Hierarchical",
        //            @"\Images\Icons\Tree.png",
        //            "Tree view {0}",
        //            "Create a document that shows information stored in a space hierarchically",
        //            "Usefull for tree structure analysis",
        //            "Does not show temporal information"),
        //        CreateNewDocumentCommand(container,
        //            new SequentialDocumentFactory(),
        //            "Sequential",
        //            @"\Images\Icons\View-Details.png",
        //            "Sequential view {0}",
        //            "Create a document to show information stored in a space sequentially",
        //            "Usefull for order analysis",
        //            "Does not show temporal information"),
        //        CreateNewDocumentCommand(container,
        //            new TemporalDocumentFactory(),
        //            "Temporal",
        //            @"\Images\Icons\Clock-01.png",
        //            "Temporal view {0}",
        //            "Create a document to show information stored in a space temporal",
        //            "Usefull for temporal analysis",
        //            null),
        //        CreateNewDocumentCommand(container,
        //            new CodeDocumentFactory(),
        //            "Code",
        //            @"\Images\Icons\File-Format-CSharp.png",
        //            "Code view {0}",
        //            "Create a document to interact with a space programmatically",
        //            "Usefull for complex iterative or recursive activities",
        //            "Allows C# code to be tested"),
        //        CreateNewDocumentCommand(container,
        //            new ScriptDocumentFactory(),
        //            "Query",
        //            @"\Images\Icons\File-Format-GraphQuery.png",
        //            "Query view {0}",
        //            "Create a document to invoke scripts on a space",
        //            "Allows execution scripts written in the GQL script language",
        //            "Usefull for advanced space operations"),
        //        CreateNewDocumentCommand(container,
        //            new ProfilingDocumentFactory(),
        //            "Profiling",
        //            @"\Images\Icons\Arrow.png",
        //            "Profiler view {0}",
        //            "Create a profiling document",
        //            "Shows profiling details of all API access to a space",
        //            "Usefull for advanced query optimization"),
        //    };
        //}

        //private NewDocumentCommand CreateNewDocumentCommand(
        //    Container container, 
        //    IDocumentFactory factory, 
        //    string header, 
        //    string icon, 
        //    string titleFormat, 
        //    string infoLine, 
        //    string infoTip1, 
        //    string infoTip2)
        //{
        //    var command = container.GetInstance<NewDocumentCommand>(); 
        //    command.DocumentFactory = factory;
        //    command.Header = header;
        //    command.Icon = icon;
        //    command.TitleFormat = titleFormat;
        //    command.InfoLine = infoLine;
        //    command.InfoTip1 = infoTip1;
        //    command.InfoTip2 = infoTip2;
        //    return command;
        //}

    }
}
