namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using System;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Diagnostics.Profiling;
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Logging;
    using SimpleInjector;

    public class MainWindowFactory
    {
        public IMainWindow Create(IProfilingDataConnection connection, IDiagnosticsConfiguration diagnostics)
        {
            var container = new Container();
            container.ResolveUnregisteredType += (sender, args) => { throw new InvalidOperationException("Unregistered type found: " + args.UnregisteredServiceType.Name); };

            container.Register<IMainDispatcherInvoker, MainDispatcherInvoker>(Lifestyle.Singleton);

            container.Register<IMainWindowViewModel, MainWindowViewModel>(Lifestyle.Singleton);
            container.Register<IMainWindow, MainWindow>(Lifestyle.Singleton);

            RegisterDiagnostics(container, diagnostics);
            RegisterContext(container, connection, diagnostics);

            container.Register<IRootsViewModel, RootsViewModel>(Lifestyle.Singleton);
            container.Register<IJournalViewModel, JournalViewModel>(Lifestyle.Singleton);

            container.Register<IFunctionalGraphDocumentFactory, FunctionalGraphDocumentFactory>(Lifestyle.Singleton);
            container.Register<INewFunctionalGraphDocumentCommand, NewFunctionalGraphDocumentCommand>(Lifestyle.Singleton);

            container.Register<ILogicalGraphDocumentFactory, LogicalGraphDocumentFactory>(Lifestyle.Singleton);
            container.Register<INewLogicalGraphDocumentCommand, NewLogicalGraphDocumentCommand>(Lifestyle.Singleton);

            container.Register<ITreeDocumentFactory, TreeDocumentFactory>(Lifestyle.Singleton);
            container.Register<INewTreeDocumentCommand, NewTreeDocumentCommand>(Lifestyle.Singleton);

            container.Register<ISequentialDocumentFactory, SequentialDocumentFactory>(Lifestyle.Singleton);
            container.Register<INewSequentialDocumentCommand, NewSequentialDocumentCommand>(Lifestyle.Singleton);

            container.Register<ITemporalDocumentFactory, TemporalDocumentFactory>(Lifestyle.Singleton);
            container.Register<INewTemporalDocumentCommand, NewTemporalDocumentCommand>(Lifestyle.Singleton);

            container.Register<IScriptDocumentFactory, ScriptDocumentFactory>(Lifestyle.Singleton);
            container.Register<INewScriptDocumentCommand, NewScriptDocumentCommand>(Lifestyle.Singleton);

            container.Register<ICodeDocumentFactory, CodeDocumentFactory>(Lifestyle.Singleton);
            container.Register<INewCodeDocumentCommand, NewCodeDocumentCommand>(Lifestyle.Singleton);

            container.Register<IProfilingDocumentFactory, ProfilingDocumentFactory>(Lifestyle.Singleton);
            container.Register<INewProfilingDocumentCommand, NewProfilingDocumentCommand>(Lifestyle.Singleton);

            var window = container.GetInstance<IMainWindow>();
            var viewModel = container.GetInstance<IMainWindowViewModel>();
            //viewModel.NewBlankDocumentCommands = CreateNewBlankDocumentCommands(container);
            window.DataContext = viewModel;
            return window;
        }

        private void RegisterContext(Container container, IProfilingDataConnection connection, IDiagnosticsConfiguration diagnostics)
        {
            // We start with the connection.
            container.Register<IDataConnection>(() => connection, Lifestyle.Singleton);
            container.Register<IProfilingDataConnection>(() => connection, Lifestyle.Singleton);

            // Then the fabric context.
            var fabricContextConfiguration = new FabricContextConfiguration()
                .Use(connection);
            var fabricContext = new FabricContextFactory().CreateForProfiling(fabricContextConfiguration);
            container.Register<IFabricContext>(() => fabricContext, Lifestyle.Singleton);
            container.Register<IProfilingFabricContext>(() => fabricContext, Lifestyle.Singleton);

            // The logical context.
            var logicalContextConfiguration = new LogicalContextConfiguration()
                .Use(fabricContext)
                .Use(diagnostics);
            var logicalContext = new LogicalContextFactory().CreateForProfiling(logicalContextConfiguration);
            container.Register<ILogicalContext>(() => logicalContext, Lifestyle.Singleton);
            container.Register<IProfilingLogicalContext>(() => logicalContext, Lifestyle.Singleton);

            // And finally, the functional context.
            var dataContextConfiguration = new DataContextConfiguration()
                                .Use(diagnostics)
                                .Use(logicalContext)
                                .UseWin32();
            var dataContext = new DataContextFactory().CreateForProfiling(dataContextConfiguration);
            container.Register<IDataContext>(() => dataContext, Lifestyle.Singleton);
            container.Register<IProfilingDataContext>(() => dataContext, Lifestyle.Singleton);
        }

        private void RegisterDiagnostics(Container container, IDiagnosticsConfiguration diagnostics)
        {
            container.Register<IDiagnosticsConfiguration>(() => diagnostics, Lifestyle.Singleton);
            container.Register<ILogFactory>(() => container.GetInstance<IDiagnosticsConfiguration>().CreateLogFactory(), Lifestyle.Singleton);
            container.Register<ILogger>(() =>
            {
                var factory = container.GetInstance<ILogFactory>();
                return container.GetInstance<IDiagnosticsConfiguration>().CreateLogger(factory);
            }, Lifestyle.Singleton);
            //container.Register<IProfilerFactory>(
            //    () => container.GetInstance<IDiagnosticsConfiguration>().CreateProfilerFactory(), Lifestyle.Singleton);
            //container.Register<IProfiler>(() =>
            //{
            //    var factory = container.GetInstance<IProfilerFactory>();
            //    return container.GetInstance<IDiagnosticsConfiguration>().CreateProfiler(factory);
            //}, Lifestyle.Singleton);
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
