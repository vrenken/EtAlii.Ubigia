namespace EtAlii.Servus.Client.Windows.Diagnostics
{
    using EtAlii.xTechnology.Workflow;
    using SimpleInjector;

    public class StructureScaffolding
    {
        public void Register(Container container)
        {
            container.Register<ICommandProcessor, TaskBasedCommandProcessor>(Lifestyle.Singleton);
            container.Register<IQueryProcessor, QueryProcessor>(Lifestyle.Singleton);
            container.Register<IUnitOfWorkProcessor, UnitOfWorkProcessor>(Lifestyle.Singleton);
            container.Register<MainDispatcherInvoker>(Lifestyle.Singleton);
        }
    }
}