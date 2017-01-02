namespace EtAlii.Ubigia.Storage
{
    using System;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Fabric;

    internal class CompositeComponentStorer : ICompositeComponentStorer
    {
        private readonly IPathBuilder _pathBuilder;
        private readonly INextCompositeComponentIdAlgorithm _nextCompositeComponentIdAlgorithm;
        private readonly IImmutableFolderManager _folderManager;

        public CompositeComponentStorer(
            IImmutableFolderManager folderManager, 
            IPathBuilder pathBuilder,
            INextCompositeComponentIdAlgorithm nextCompositeComponentIdAlgorithm)
        {
            _folderManager = folderManager;
            _pathBuilder = pathBuilder;
            _nextCompositeComponentIdAlgorithm = nextCompositeComponentIdAlgorithm;
        }


        public void Store(ContainerIdentifier containerId, CompositeComponent component)
        {
            var componentName = ComponentHelper.GetName(component);

            containerId = ContainerIdentifier.Combine(containerId, componentName);

            if (component.Id != 0)
            {
                throw new InvalidOperationException("Unable to store composite component: Id already assigned");
            }

            var compositeComponentId = _nextCompositeComponentIdAlgorithm.Create(containerId);

            var folder = _pathBuilder.GetFolder(containerId);

            //var format = "Storing Composite component (Id:{2}) component to: {1}";
            //_logger.Verbose(format, componentName, folder, compositeComponentId);

            _folderManager.Create(folder);

            ComponentHelper.SetStored(component, false);
            _folderManager.SaveToFolder(component, compositeComponentId.ToString(), folder);
            ComponentHelper.SetStored(component, true);

            ComponentHelper.SetId(component, compositeComponentId);
        }
    }
}
