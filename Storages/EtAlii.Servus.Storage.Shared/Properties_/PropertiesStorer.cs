﻿namespace EtAlii.Servus.Storage
{
    using System.Text.RegularExpressions;
    using EtAlii.Servus.Api;

    public class PropertiesStorer : IPropertiesStorer
    {
        private readonly IPathBuilder _pathBuilder;
        private readonly IFileManager _fileManager;

        public PropertiesStorer(
            IPathBuilder pathBuilder, 
            IFileManager fileManager)
        {
            _pathBuilder = pathBuilder;
            _fileManager = fileManager;
        }

        public void Store(ContainerIdentifier container, PropertyDictionary properties, string name)
        {
            container = ContainerIdentifier.Combine(container, "Properties");

            var fileName = _pathBuilder.GetFileName(name, container);
            PropertiesHelper.SetStored(properties, false);
            _fileManager.SaveToFile(fileName, properties);
            PropertiesHelper.SetStored(properties, true);
        }
    }
}
