namespace EtAlii.Servus.Storage
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Fabric;

    internal class ComponentStorer : IComponentStorer
    {
        private readonly IPathBuilder _pathBuilder;
        private readonly IImmutableFolderManager _folderManager;

        public ComponentStorer(IImmutableFolderManager folderManager, 
                               IPathBuilder pathBuilder)
        {
            _folderManager = folderManager;
            _pathBuilder = pathBuilder;
        }

        public void Store<T>(ContainerIdentifier containerId, T component)
            where T : class, IComponent
        {
            var componentName = ComponentHelper.GetName(component);

            var folder = _pathBuilder.GetFolder(containerId);

            //var format = "Storing {0} component in: {1}";
            //_logger.Verbose(format, componentName, folder);

            _folderManager.Create(folder);

            ComponentHelper.SetStored(component, false);
            _folderManager.SaveToFolder(component, componentName, folder);
            ComponentHelper.SetStored(component, true);
        }
    }
}
