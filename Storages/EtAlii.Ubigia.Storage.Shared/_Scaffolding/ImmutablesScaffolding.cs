namespace EtAlii.Ubigia.Storage
{
    using EtAlii.xTechnology.MicroContainer;

    public class ImmutablesScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<IImmutableFolderManager>(container.GetInstance<IFolderManager>);
            container.Register<IImmutableFileManager>(container.GetInstance<IFileManager>);
        }
    }
}
