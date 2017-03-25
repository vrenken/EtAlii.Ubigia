namespace EtAlii.Ubigia.Storage
{
    using System;
    using System.Text.RegularExpressions;
    using EtAlii.Ubigia.Api;

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

        public void Store(ContainerIdentifier container, PropertyDictionary properties, string name = "Default")
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

        public PropertyDictionary Retrieve(ContainerIdentifier container, string name)
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
