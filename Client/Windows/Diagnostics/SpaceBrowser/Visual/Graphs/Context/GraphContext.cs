namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using EtAlii.xTechnology.Workflow;

    public class GraphContext : IGraphContext
    {
        public IAddEntryToGraphCommandHandler AddEntryToGraphCommandHandler { get; private set; }

        public IRetrieveEntryCommandHandler RetrieveEntryCommandHandler { get; private set; }

        public IProcessEntryCommandHandler ProcessEntryCommandHandler { get; private set; }

        public IDiscoverEntryCommandHandler DiscoverEntryCommandHandler { get; private set; }

        public IAddEntryRelationsToGraphCommandHandler AddEntryRelationsToGraphCommandHandler { get; private set; }

        public IApplyLayoutingToGraphCommandHandler ApplyLayoutingToGraphCommandHandler { get; private set; }

        public IRemoveEntriesFromGraphCommandHandler RemoveEntriesFromGraphCommandHandler { get; private set; }

        public IFindEntriesOnGraphQueryHandler FindEntriesOnGraphQueryHandler { get; private set; }

        public IFindEntryOnGraphQueryHandler FindEntryOnGraphQueryHandler { get; private set; }

        public ITraverseRelationsQueryHandler TraverseRelationsQueryHandler { get; private set; }

        public ICommandProcessor CommandProcessor { get; }

        public IUnitOfWorkProcessor UnitOfWorkProcessor { get; }

        public IQueryProcessor QueryProcessor { get; }

        /// <summary>
        /// The Configuration used to instantiate this Context.
        /// </summary>
        public IGraphConfiguration Configuration { get; }

        public GraphContext(
            ICommandProcessor commandProcessor, 
            IUnitOfWorkProcessor unitOfWorkProcessor, 
            IQueryProcessor queryProcessor, 
            IGraphConfiguration configuration)
        {
            CommandProcessor = commandProcessor;
            UnitOfWorkProcessor = unitOfWorkProcessor;
            QueryProcessor = queryProcessor;
            Configuration = configuration;
        }

        public void Initialize(
            IAddEntryToGraphCommandHandler addEntryToGraphCommandHandler, 
            IRetrieveEntryCommandHandler retrieveEntryCommandHandler, 
            IProcessEntryCommandHandler processEntryCommandHandler, 
            IDiscoverEntryCommandHandler discoverEntryCommandHandler, 
            IAddEntryRelationsToGraphCommandHandler addEntryRelationsToGraphCommandHandler, 
            IApplyLayoutingToGraphCommandHandler applyLayoutingToGraphCommandHandler, 
            IRemoveEntriesFromGraphCommandHandler removeEntriesFromGraphCommandHandler, 
            IFindEntriesOnGraphQueryHandler findEntriesOnGraphQueryHandler, 
            IFindEntryOnGraphQueryHandler findEntryOnGraphQueryHandler, 
            ITraverseRelationsQueryHandler traverseRelationsQueryHandler)
        {
            AddEntryToGraphCommandHandler = addEntryToGraphCommandHandler;
            RetrieveEntryCommandHandler = retrieveEntryCommandHandler;
            ProcessEntryCommandHandler = processEntryCommandHandler;
            DiscoverEntryCommandHandler = discoverEntryCommandHandler;
            AddEntryRelationsToGraphCommandHandler = addEntryRelationsToGraphCommandHandler;
            ApplyLayoutingToGraphCommandHandler = applyLayoutingToGraphCommandHandler;
            RemoveEntriesFromGraphCommandHandler = removeEntriesFromGraphCommandHandler;
            FindEntriesOnGraphQueryHandler = findEntriesOnGraphQueryHandler;
            FindEntryOnGraphQueryHandler = findEntryOnGraphQueryHandler;
            TraverseRelationsQueryHandler = traverseRelationsQueryHandler;
        }
    }
}