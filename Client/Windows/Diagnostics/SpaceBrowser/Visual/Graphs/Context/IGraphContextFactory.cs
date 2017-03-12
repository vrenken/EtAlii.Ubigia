namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.xTechnology.Logging;

    public interface IGraphContextFactory
    {
        IGraphContext Create(
            ILogger logger,
            IJournalViewModel journal,
            IFabricContext fabricContext,
            IDocumentViewModelProvider documentViewModelProvider);
    }
}