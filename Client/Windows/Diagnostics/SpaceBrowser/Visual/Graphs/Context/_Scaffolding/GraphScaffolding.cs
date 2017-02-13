namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using EtAlii.xTechnology.Workflow;
    using EtAlii.xTechnology.MicroContainer;

    public class GraphScaffolding
    {
        public void Register(Container container)
        {
            container.Register<IGraphConfiguration, GraphConfiguration>();

            container.Register<IGraphContext, GraphContext>();

            container.Register<IMainDispatcherInvoker, MainDispatcherInvoker>();

            container.Register<ICommandProcessor, TaskBasedCommandProcessor>();
            container.Register<IQueryProcessor, QueryProcessor>();
            container.Register<IUnitOfWorkProcessor, UnitOfWorkProcessor>();

            container.Register<IAddEntryToGraphCommandHandler, AddEntryToGraphCommandHandler>();
            container.Register<IRetrieveEntryCommandHandler, RetrieveEntryCommandHandler>();
            container.Register<IProcessEntryCommandHandler, ProcessEntryCommandHandler>();
            container.Register<IDiscoverEntryCommandHandler, DiscoverEntryCommandHandler>();
            container.Register<IAddEntryRelationsToGraphCommandHandler, AddEntryRelationsToGraphCommandHandler>();
            container.Register<IApplyLayoutingToGraphCommandHandler, ApplyLayoutingToGraphCommandHandler>();
            container.Register<IRemoveEntriesFromGraphCommandHandler, RemoveEntriesFromGraphCommandHandler>();
            container.Register<IFindEntriesOnGraphQueryHandler, FindEntriesOnGraphQueryHandler>();
            container.Register<IFindEntryOnGraphQueryHandler, FindEntryOnGraphQueryHandler>();
            container.Register<ITraverseRelationsQueryHandler, TraverseRelationsQueryHandler>();
        }

        public void Initialize(Container container)
        {
            var graphContext = container.GetInstance<IGraphContext>();

            var addEntryToGraphCommandHandler = container.GetInstance<IAddEntryToGraphCommandHandler>();
            var retrieveEntryCommandHandler = container.GetInstance<IRetrieveEntryCommandHandler>();
            var processEntryCommandHandler = container.GetInstance<IProcessEntryCommandHandler>();
            var discoverEntryCommandHandler = container.GetInstance<IDiscoverEntryCommandHandler>();
            var addEntryRelationsToGraphCommandHandler = container.GetInstance<IAddEntryRelationsToGraphCommandHandler>();
            var applyLayoutingToGraphCommandHandler = container.GetInstance<IApplyLayoutingToGraphCommandHandler>();
            var removeEntriesFromGraphCommandHandler = container.GetInstance<IRemoveEntriesFromGraphCommandHandler>();
            var findEntriesOnGraphQueryHandler = container.GetInstance<IFindEntriesOnGraphQueryHandler>();
            var findEntryOnGraphQueryHandler = container.GetInstance<IFindEntryOnGraphQueryHandler>();
            var traverseRelationsQueryHandler = container.GetInstance<ITraverseRelationsQueryHandler>();

            graphContext.Initialize(addEntryToGraphCommandHandler, retrieveEntryCommandHandler, processEntryCommandHandler, discoverEntryCommandHandler, addEntryRelationsToGraphCommandHandler, applyLayoutingToGraphCommandHandler, removeEntriesFromGraphCommandHandler, findEntriesOnGraphQueryHandler, findEntryOnGraphQueryHandler, traverseRelationsQueryHandler);
        }
    }
}