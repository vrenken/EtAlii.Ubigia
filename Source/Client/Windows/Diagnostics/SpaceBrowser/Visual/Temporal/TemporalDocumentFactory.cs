﻿namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using EtAlii.xTechnology.MicroContainer;

    public class TemporalDocumentFactory : ITemporalDocumentFactory
    {
        public IDocumentViewModel Create(IDocumentContext documentContext)
        {
            var container = new Container();

            new DiagnosticsScaffolding().Register(container, documentContext.Diagnostics);
            new StructureScaffolding().Register(container);

            container.Register(() => documentContext.FabricContext);
            container.Register(() => documentContext.ScriptContext);
            container.Register(() =>
            {
                var dvmp = container.GetInstance<IDocumentViewModelProvider>();
                return documentContext.GraphContextFactory.Create(documentContext.Journal, documentContext.FabricContext, dvmp);
            });

            container.Register<IDocumentViewModelProvider, DocumentViewModelProvider>();
            container.Register(() => documentContext.Journal);
            container.Register<ITemporalDocumentViewModel, TemporalDocumentViewModel>();
            container.Register<IGraphButtonsViewModel, GraphButtonsViewModel>();
            container.Register<IGraphContextMenuViewModel, GraphContextMenuViewModel>();

            var documentViewModel = container.GetInstance<ITemporalDocumentViewModel>();
            var documentViewModelProvider = container.GetInstance<IDocumentViewModelProvider>();
            documentViewModelProvider.SetInstance(documentViewModel);

            return documentViewModel;
        }
    }
}