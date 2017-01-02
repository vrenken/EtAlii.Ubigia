namespace EtAlii.Servus.Storage
{
    using EtAlii.Servus.Client.Model;
    using EtAlii.xTechnology.Logging;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class ComponentRetriever : IComponentRetriever
    {
        private readonly ILogger _logger;
        private readonly IPathBuilder _pathBuilder;
        private readonly IFolderManager _folderManager;
        private readonly IFileManager _fileManager;

        public ComponentRetriever(ILogger logger, IPathBuilder pathBuilder, IFolderManager folderManager, IFileManager fileManager)
        {
            _logger = logger;
            _pathBuilder = pathBuilder;
            _folderManager = folderManager;
            _fileManager = fileManager;
        }

        public IEnumerable<T> RetrieveAll<T>(ContainerIdentifier container)
            where T : CompositeComponent
        {
            try
            {
                var componentName = ContentComponent.GetName<T>();

                container = ContainerIdentifier.Combine(container, componentName);

                var folder = _pathBuilder.GetFolder(container);

                var components = new List<T>();

                if (_folderManager.Exists(folder))
                {
                    foreach (var fullFileName in _folderManager.EnumerateFiles(folder))
                    {
                        var fileName = Path.GetFileNameWithoutExtension(fullFileName);
                        var compositeComponentId = UInt64.Parse(fileName);

                        var format = "Retrieving {0} (Id:{2}) component from: {1}";
                        _logger.Verbose(format, componentName, folder, compositeComponentId);

                        var component = _fileManager.LoadFromFile<T>(fullFileName);

                        CompositeComponent.SetId(component, compositeComponentId);
                        EntryComponentHelper.SetStored(component, true);
                        components.Add(component);
                    }
                }

                return components;
            }
            catch (Exception e)
            {
                throw new StorageException("Unable to retrieve components from the specified container", e);
            }
        }

        public T Retrieve<T>(ContainerIdentifier container)
            where T : NonCompositeComponent
        {
            try
            {
                var componentName = ContentComponent.GetName<T>();
                var folder = _pathBuilder.GetFolder(container);

                var format = "Retrieving {0} component from: {1}";
                _logger.Verbose(format, componentName, folder);


                var component = _folderManager.LoadFromFolder<T>(folder, componentName);

                // Why is this if statement here? I don't like it.
                if (component != null)
                {
                    EntryComponentHelper.SetStored(component, true);
                }
                return component;
            }
            catch (Exception e)
            {
                throw new StorageException("Unable to retrieve components from the specified container", e);
            }
        }
    }
}
