namespace EtAlii.Ubigia.Windows.Tools.MediaImport
{
    using EtAlii.xTechnology.MicroContainer;

    public class TaskbarIconScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<ITaskbarIconViewModel, TaskbarIconViewModel>();
            container.Register<ITaskbarIcon, TaskbarIcon>();
            container.RegisterInitializer<ITaskbarIcon>(taskbarIcon => taskbarIcon.DataContext = container.GetInstance<ITaskbarIconViewModel>());
        }
    }
}
