// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence
{
    using System;
    using System.Text.RegularExpressions;

    internal class PropertiesStorage : IPropertiesStorage
    {
        private readonly IPropertiesRetriever _propertiesRetriever;
        private readonly IPropertiesStorer _propertiesStorer;
        private readonly Regex _nameRegex;

        public PropertiesStorage(
            IPropertiesRetriever propertiesRetriever,
            IPropertiesStorer propertiesStorer)
        {
            _propertiesRetriever = propertiesRetriever;
            _propertiesStorer = propertiesStorer;
            _nameRegex = new Regex(@"[^A-Za-z0-9]+", RegexOptions.Singleline);
        }

        public void Store(ContainerIdentifier container, PropertyDictionary properties, string name = "_Default")
        {
            try
            {
                name = _nameRegex.Replace(name, "_");
                _propertiesStorer.Store(container, properties, name);
            }
            catch (Exception e)
            {
                var message = $"Unable to store properties in the specified container ({name})";
                throw new StorageException(message, e);
            }
        }

        public PropertyDictionary Retrieve(ContainerIdentifier container, string name = "_Default")
        {
            try
            {
                name = _nameRegex.Replace(name, "_");
                return _propertiesRetriever.Retrieve(container, name);
            }
            catch (Exception e)
            {
                var message = $"Unable to retrieve properties from the specified container ({name})";
                throw new StorageException(message, e);
            }
        }
    }
}
