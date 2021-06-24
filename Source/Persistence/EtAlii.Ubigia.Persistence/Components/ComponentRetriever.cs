// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

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

        public async IAsyncEnumerable<T> RetrieveAll<T>(ContainerIdentifier container)
            where T : CompositeComponent
        {
            var componentName = ComponentHelper.GetName<T>();

            container = ContainerIdentifier.Combine(container, componentName);

            var folder = _pathBuilder.GetFolder(container);

            if (_folderManager.Exists(folder))
            {
                foreach (var fullFileName in _folderManager.EnumerateFiles(folder))
                {
                    var fileName = _pathBuilder.GetFileNameWithoutExtension(fullFileName);
                    var compositeComponentId = ulong.Parse(fileName);

                    var component = await _fileManager.LoadFromFile<T>(fullFileName).ConfigureAwait(false);

                    ComponentHelper.SetId(component, compositeComponentId);
                    ComponentHelper.SetStored(component, true);
                    yield return component;
                }
            }
        }

        public async Task<T> Retrieve<T>(ContainerIdentifier container)
            where T : NonCompositeComponent
        {
            var componentName = ComponentHelper.GetName<T>();
            var folder = _pathBuilder.GetFolder(container);

            //var format = "Retrieving [0] component from: [1]"
            //_logger.Verbose[format, componentName, folder]

            var component = await _folderManager.LoadFromFolder<T>(folder, componentName).ConfigureAwait(false);

            // Why is this if statement here? I don't like it.
            if (component != null)
            {
                ComponentHelper.SetStored(component, true);
            }
            return component;
        }
    }
}
