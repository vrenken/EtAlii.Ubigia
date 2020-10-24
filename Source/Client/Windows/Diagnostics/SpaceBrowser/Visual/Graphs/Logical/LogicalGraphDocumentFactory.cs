﻿namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using EtAlii.xTechnology.MicroContainer;

    public class LogicalGraphDocumentFactory : ILogicalGraphDocumentFactory
    {
        public IDocumentViewModel Create(IDocumentContext documentContext)
        {
            var container = new Container();

            new DiagnosticsScaffolding().Register(container, documentContext.Diagnostics);
            new StructureScaffolding().Register(container);

            container.Register<IDocumentViewModelProvider, DocumentViewModelProvider>();
            container.Register(() => documentContext.Journal);
            container.Register<ILogicalGraphDocumentViewModel, LogicalGraphDocumentViewModel>();
            container.Register<IGraphButtonsViewModel, GraphButtonsViewModel>();
            container.Register<IGraphContextMenuViewModel, GraphContextMenuViewModel>();

            container.Register(() => documentContext.FabricContext);
            container.Register(() => documentContext.ScriptContext);
            container.Register(() =>
            {
                var dvmp = container.GetInstance<IDocumentViewModelProvider>();
                return documentContext.GraphContextFactory.Create(documentContext.Journal, documentContext.FabricContext, dvmp);
            });

            var documentViewModel = container.GetInstance<ILogicalGraphDocumentViewModel>();
            var documentViewModelService = container.GetInstance<IDocumentViewModelProvider>();
            documentViewModelService.SetInstance(documentViewModel);

            return documentViewModel;
        }
    }
}