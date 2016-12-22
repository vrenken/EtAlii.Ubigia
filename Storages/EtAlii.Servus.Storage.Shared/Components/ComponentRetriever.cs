namespace EtAlii.Servus.Storage
{
    using System;
    using System.Collections.Generic;
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Fabric;

    internal class ComponentRetriever : IComponentRetriever
    {
        private readonly IPathBuilder _pathBuilder;
        private readonly IImmutableFolderManager _folderManager;
        private readonly IImmutableFileManager _fileManager;

        public ComponentRetriever(IPathBuilder pathBuilder, 
                                  IImmutableFolderManager folderManager, 
                                  IImmutableFileManager fileManager)
        {
            _pathBuilder = pathBuilder;
            _folderManager = folderManager;
            _fileManager = fileManager;
        }

        public IEnumerable<T> RetrieveAll<T>(ContainerIdentifier container)
            where T : CompositeComponent
        {
            var componentName = ComponentHelper.GetName<T>();

            container = ContainerIdentifier.Combine(container, componentName);

            var folder = _pathBuilder.GetFolder(container);

            var components = new List<T>();

            if (_folderManager.Exists(folder))
            {
                foreach (var fullFileName in _folderManager.EnumerateFiles(folder))
                {
                    var fileName = _pathBuilder.GetFileNameWithoutExtension(fullFileName);
                    var compositeComponentId = UInt64.Parse(fileName);

                    //const string format = "Retrieving {0} (Id:{2}) component from: {1}";
                    //_logger.Verbose(format, componentName, folder, compositeComponentId);

                    var component = _fileManager.LoadFromFile<T>(fullFileName);

                    ComponentHelper.SetId(component, compositeComponentId);
                    ComponentHelper.SetStored(component, true);
                    components.Add(component);
                }
            }

            return components;
        }

        public T Retrieve<T>(ContainerIdentifier container)
            where T : NonCompositeComponent
        {
            var componentName = ComponentHelper.GetName<T>();
            var folder = _pathBuilder.GetFolder(container);

            //var format = "Retrieving {0} component from: {1}";
            //_logger.Verbose(format, componentName, folder);

            var component = _folderManager.LoadFromFolder<T>(folder, componentName);

            // Why is this if statement here? I don't like it.
            if (component != null)
            {
                ComponentHelper.SetStored(component, true);
            }
            return component;
        }
    }
}
