namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using EtAlii.xTechnology.Workflow;

    public class GraphContext : IGraphContext
    {
        public IAddEntryToGraphCommandHandler AddEntryToGraphCommandHandler => _addEntryToGraphCommandHandler;
        private IAddEntryToGraphCommandHandler _addEntryToGraphCommandHandler;
        public IRetrieveEntryCommandHandler RetrieveEntryCommandHandler => _retrieveEntryCommandHandler;
        private IRetrieveEntryCommandHandler _retrieveEntryCommandHandler;
        public IProcessEntryCommandHandler ProcessEntryCommandHandler => _processEntryCommandHandler;
        private IProcessEntryCommandHandler _processEntryCommandHandler;
        public IDiscoverEntryCommandHandler DiscoverEntryCommandHandler => _discoverEntryCommandHandler;
        private IDiscoverEntryCommandHandler _discoverEntryCommandHandler;
        public IAddEntryRelationsToGraphCommandHandler AddEntryRelationsToGraphCommandHandler => _addEntryRelationsToGraphCommandHandler;
        private IAddEntryRelationsToGraphCommandHandler _addEntryRelationsToGraphCommandHandler;
        public IApplyLayoutingToGraphCommandHandler ApplyLayoutingToGraphCommandHandler => _applyLayoutingToGraphCommandHandler;
        private IApplyLayoutingToGraphCommandHandler _applyLayoutingToGraphCommandHandler;
        public IRemoveEntriesFromGraphCommandHandler RemoveEntriesFromGraphCommandHandler => _removeEntriesFromGraphCommandHandler;
        private IRemoveEntriesFromGraphCommandHandler _removeEntriesFromGraphCommandHandler;
        public IFindEntriesOnGraphQueryHandler FindEntriesOnGraphQueryHandler => _findEntriesOnGraphQueryHandler;
        private IFindEntriesOnGraphQueryHandler _findEntriesOnGraphQueryHandler;
        public IFindEntryOnGraphQueryHandler FindEntryOnGraphQueryHandler => _findEntryOnGraphQueryHandler;
        private IFindEntryOnGraphQueryHandler _findEntryOnGraphQueryHandler;
        public ITraverseRelationsQueryHandler TraverseRelationsQueryHandler => _traverseRelationsQueryHandler;
        private ITraverseRelationsQueryHandler _traverseRelationsQueryHandler;

        public ICommandProcessor CommandProcessor => _commandProcessor;
        private readonly ICommandProcessor _commandProcessor;

        public IUnitOfWorkProcessor UnitOfWorkProcessor => _unitOfWorkProcessor;
        private readonly IUnitOfWorkProcessor _unitOfWorkProcessor;
        public IQueryProcessor QueryProcessor => _queryProcessor;
        private readonly IQueryProcessor _queryProcessor;

        public IGraphConfiguration Configuration => _configuration;
        private readonly IGraphConfiguration _configuration;

        public GraphContext(
            ICommandProcessor commandProcessor, 
            IUnitOfWorkProcessor unitOfWorkProcessor, 
            IQueryProcessor queryProcessor, 
            IGraphConfiguration configuration)
        {
            _commandProcessor = commandProcessor;
            _unitOfWorkProcessor = unitOfWorkProcessor;
            _queryProcessor = queryProcessor;
            _configuration = configuration;
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
            _addEntryToGraphCommandHandler = addEntryToGraphCommandHandler;
            _retrieveEntryCommandHandler = retrieveEntryCommandHandler;
            _processEntryCommandHandler = processEntryCommandHandler;
            _discoverEntryCommandHandler = discoverEntryCommandHandler;
            _addEntryRelationsToGraphCommandHandler = addEntryRelationsToGraphCommandHandler;
            _applyLayoutingToGraphCommandHandler = applyLayoutingToGraphCommandHandler;
            _removeEntriesFromGraphCommandHandler = removeEntriesFromGraphCommandHandler;
            _findEntriesOnGraphQueryHandler = findEntriesOnGraphQueryHandler;
            _findEntryOnGraphQueryHandler = findEntryOnGraphQueryHandler;
            _traverseRelationsQueryHandler = traverseRelationsQueryHandler;
        }
    }
}