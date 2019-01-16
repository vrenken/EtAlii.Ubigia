namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using EtAlii.xTechnology.Workflow;

    public interface IGraphContext
    {
        IGraphConfiguration Configuration { get; }

        ICommandProcessor CommandProcessor { get; }
        IUnitOfWorkProcessor UnitOfWorkProcessor { get; }
        IQueryProcessor QueryProcessor { get; }

        IAddEntryToGraphCommandHandler AddEntryToGraphCommandHandler { get; }
        IRetrieveEntryCommandHandler RetrieveEntryCommandHandler { get; }
        IProcessEntryCommandHandler ProcessEntryCommandHandler { get; }
        IDiscoverEntryCommandHandler DiscoverEntryCommandHandler { get; }
        IAddEntryRelationsToGraphCommandHandler AddEntryRelationsToGraphCommandHandler { get; }
        IApplyLayoutingToGraphCommandHandler ApplyLayoutingToGraphCommandHandler { get; }
        IRemoveEntriesFromGraphCommandHandler RemoveEntriesFromGraphCommandHandler { get; }
        IFindEntriesOnGraphQueryHandler FindEntriesOnGraphQueryHandler { get; }
        IFindEntryOnGraphQueryHandler FindEntryOnGraphQueryHandler { get; }
        ITraverseRelationsQueryHandler TraverseRelationsQueryHandler { get; }

        void Initialize(
            IAddEntryToGraphCommandHandler addEntryToGraphCommandHandler,
            IRetrieveEntryCommandHandler retrieveEntryCommandHandler,
            IProcessEntryCommandHandler processEntryCommandHandler,
            IDiscoverEntryCommandHandler discoverEntryCommandHandler,
            IAddEntryRelationsToGraphCommandHandler addEntryRelationsToGraphCommandHandler,
            IApplyLayoutingToGraphCommandHandler applyLayoutingToGraphCommandHandler,
            IRemoveEntriesFromGraphCommandHandler removeEntriesFromGraphCommandHandler,
            IFindEntriesOnGraphQueryHandler findEntriesOnGraphQueryHandler,
            IFindEntryOnGraphQueryHandler findEntryOnGraphQueryHandler,
            ITraverseRelationsQueryHandler traverseRelationsQueryHandler);

    }
}