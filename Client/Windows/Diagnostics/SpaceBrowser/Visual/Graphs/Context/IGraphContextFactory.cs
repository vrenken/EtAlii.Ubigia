namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.xTechnology.Diagnostics;

    public interface IGraphContextFactory
    {
        IGraphContext Create(
            ILogger logger,
            IJournalViewModel journal,
            IFabricContext fabricContext,
            IDocumentViewModelProvider documentViewModelProvider);
    }
}