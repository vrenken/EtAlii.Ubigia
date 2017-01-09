namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using System;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Logging;
    using SimpleInjector;

    public class CodeDocumentFactory : ICodeDocumentFactory
    {
        public IDocumentViewModel Create(
            IDataContext dataContext,
            ILogicalContext logicalContext,
            IFabricContext fabricContext,
            IDataConnection connection,
            IDiagnosticsConfiguration diagnostics,
            ILogger logger,
            ILogFactory logFactory,
            IJournal journal)
        {
            var container = new Container();
            container.ResolveUnregisteredType += (sender, args) => { throw new InvalidOperationException("Unregistered type found: " + args.UnregisteredServiceType.Name); };

            new DiagnosticsScaffolding().Register(container, diagnostics, logger, logFactory);
            new StructureScaffolding().Register(container);

            container.Register<IFabricContext>(() => fabricContext, Lifestyle.Singleton);
            container.Register<IDataContext>(() => dataContext, Lifestyle.Singleton);

            container.Register<CodeViewModel>(Lifestyle.Singleton);
            container.Register<IJournal>(() => journal, Lifestyle.Singleton);

            container.Register<CodeButtonsViewModel>(Lifestyle.Singleton);

            container.Register<CompileCodeUnitOfworkHandler>(Lifestyle.Singleton);
            container.Register<ExecuteCodeUnitOfworkHandler>(Lifestyle.Singleton);
            container.Register<CodeCompiler>(Lifestyle.Singleton);
            container.Register<CodeCompilerResultsParser>(Lifestyle.Singleton);

            container.Register<TextTemplateQueryHandler>(Lifestyle.Singleton);

            return container.GetInstance<CodeViewModel>();
        }
    }
}
