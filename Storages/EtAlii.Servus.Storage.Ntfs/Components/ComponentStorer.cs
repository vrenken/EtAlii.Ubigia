namespace EtAlii.Servus.Storage
{
    using EtAlii.Servus.Client.Model;
    using EtAlii.xTechnology.Logging;
    using System;
    using System.Collections.Generic;

    public class ComponentStorer : IComponentStorer
    {
        private readonly ILogger _logger;
        private readonly IPathBuilder _pathBuilder;
        private readonly IFolderManager _folderManager;

        public ComponentStorer(ILogger logger, IFolderManager folderManager, IPathBuilder pathBuilder)
        {
            _logger = logger;
            _folderManager = folderManager;
            _pathBuilder = pathBuilder;
        }


        public void StoreAll<T>(ContainerIdentifier container, IEnumerable<T> components)
               where T : EntryComponent
        {
            foreach (var component in components)
            {
                Store(container, component);
            }
        }

        public void Store<T>(ContainerIdentifier container, T component)
            where T : EntryComponent
        {
            try
            {
                var componentName = EntryComponent.GetName(component);
                var compositeComponentId = (ulong)0;

                if (component is CompositeComponent)
                {
                    container = ContainerIdentifier.Combine(container, componentName);

                    var compositeComponent = component as CompositeComponent;
                    if (compositeComponent.Id != 0)
                    {
                        throw new InvalidOperationException("Unable to store composite component: Id already assigned");
                    }
                    //CompositeComponent.SetId(compositeComponent);
                    compositeComponentId = (ulong)System.DateTime.UtcNow.Ticks;
                }

                var folder = _pathBuilder.GetFolder(container);

                var format = compositeComponentId == 0 ? "Storing {0} component in: {1}" : "Storing Composite component (Id:{2}) component to: {1}";
                _logger.Verbose(format, componentName, folder, compositeComponentId);

                LongPathHelper.Create(folder);

                EntryComponentHelper.SetStored(component, false);
                _folderManager.SaveToFolder(component, compositeComponentId == 0 ? componentName : compositeComponentId.ToString(), folder);
                EntryComponentHelper.SetStored(component, true);

                if (component is CompositeComponent)
                {
                    var compositeComponent = component as CompositeComponent;
                    CompositeComponent.SetId(compositeComponent, compositeComponentId);
                }
            }
            catch (Exception e)
            {
                throw new StorageException("Unable to store components in the specified container", e);
            }
        }
    }
}
