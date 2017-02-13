namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using EtAlii.xTechnology.Workflow;
    using SimpleInjector;

    public class GraphScaffolding
    {
        public void Register(Container container)
        {
            container.Register<IGraphConfiguration, GraphConfiguration>(Lifestyle.Singleton);

            container.Register<IGraphContext, GraphContext>(Lifestyle.Singleton);

            container.Register<IMainDispatcherInvoker, MainDispatcherInvoker>(Lifestyle.Singleton);

            container.Register<ICommandProcessor, TaskBasedCommandProcessor>(Lifestyle.Singleton);
            container.Register<IQueryProcessor, QueryProcessor>(Lifestyle.Singleton);
            container.Register<IUnitOfWorkProcessor, UnitOfWorkProcessor>(Lifestyle.Singleton);

            container.Register<IAddEntryToGraphCommandHandler, AddEntryToGraphCommandHandler>(Lifestyle.Singleton);
            container.Register<IRetrieveEntryCommandHandler, RetrieveEntryCommandHandler>(Lifestyle.Singleton);
            container.Register<IProcessEntryCommandHandler, ProcessEntryCommandHandler>(Lifestyle.Singleton);
            container.Register<IDiscoverEntryCommandHandler, DiscoverEntryCommandHandler>(Lifestyle.Singleton);
            container.Register<IAddEntryRelationsToGraphCommandHandler, AddEntryRelationsToGraphCommandHandler>(Lifestyle.Singleton);
            container.Register<IApplyLayoutingToGraphCommandHandler, ApplyLayoutingToGraphCommandHandler>(Lifestyle.Singleton);
            container.Register<IRemoveEntriesFromGraphCommandHandler, RemoveEntriesFromGraphCommandHandler>(Lifestyle.Singleton);
            container.Register<IFindEntriesOnGraphQueryHandler, FindEntriesOnGraphQueryHandler>(Lifestyle.Singleton);
            container.Register<IFindEntryOnGraphQueryHandler, FindEntryOnGraphQueryHandler>(Lifestyle.Singleton);
            container.Register<ITraverseRelationsQueryHandler, TraverseRelationsQueryHandler>(Lifestyle.Singleton);
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