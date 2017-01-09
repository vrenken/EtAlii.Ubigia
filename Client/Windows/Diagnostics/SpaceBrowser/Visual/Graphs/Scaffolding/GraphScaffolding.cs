namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using EtAlii.Ubigia.Client.Windows.Diagnostics.Views;
    using SimpleInjector;

    public class GraphScaffolding
    {
        public void Register(Container container)
        {
            container.Register<IGraphButtonsViewModel, GraphButtonsViewModel>(Lifestyle.Singleton);
            container.Register<IGraphContextMenuViewModel, GraphContextMenuViewModel>(Lifestyle.Singleton);
            container.Register<IGraphConfiguration, GraphConfiguration>(Lifestyle.Singleton);

            container.Register<IAddEntryToGraphCommandHandler, AddEntryToGraphCommandHandler>(Lifestyle.Singleton);
            container.Register<IRetrieveEntryCommandHandler, RetrieveEntryCommandHandler>(Lifestyle.Singleton);
            container.Register<IProcessEntryCommandHandler, ProcessEntryCommandHandler>(Lifestyle.Singleton);
            container.Register<IDiscoverEntryCommandHandler, DiscoverEntryCommandHandler>(Lifestyle.Singleton);
            container.Register<IAddEntryRelationsToGraphCommandHandler, AddEntryRelationsToGraphCommandHandler>(Lifestyle.Singleton);
            container.Register<IApplyLayoutingToGraphCommandHandler, ApplyLayoutingToGraphCommandHandler>(Lifestyle.Singleton);
            container.Register<IRemoveEntriesFromGraphCommandHandler, RemoveEntriesFromGraphCommandHandler>(Lifestyle.Singleton);
            container.Register<IFindEntriesOnGraphQueryHandler, FindEntriesOnGraphQueryHandler>(Lifestyle.Singleton);
        }
    }
}