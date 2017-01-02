namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using EtAlii.Ubigia.Client.Windows.Diagnostics.Views;
    using SimpleInjector;

    public class GraphScaffolding
    {
        public void Register(Container container)
        {
            container.Register<GraphButtonsViewModel>(Lifestyle.Singleton);
            container.Register<GraphContextMenuViewModel>(Lifestyle.Singleton);
            container.Register<GraphConfiguration>(Lifestyle.Singleton);

            container.Register<AddEntryToGraphCommandHandler>(Lifestyle.Singleton);
            container.Register<RetrieveEntryCommandHandler>(Lifestyle.Singleton);
            container.Register<ProcessEntryCommandHandler>(Lifestyle.Singleton);
            container.Register<DiscoverEntryCommandHandler>(Lifestyle.Singleton);
            container.Register<AddEntryRelationsToGraphCommandHandler>(Lifestyle.Singleton);
            container.Register<ApplyLayoutingToGraphCommandHandler>(Lifestyle.Singleton);
            container.Register<RemoveEntriesFromGraphCommandHandler>(Lifestyle.Singleton);
            container.Register<FindEntriesOnGraphQueryHandler>(Lifestyle.Singleton);
        }
    }
}