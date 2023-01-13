// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence;

using System;
using System.Text.RegularExpressions;

internal class PropertiesStorage : IPropertiesStorage
{
    private readonly IPropertiesRetriever _propertiesRetriever;
    private readonly IPropertiesStorer _propertiesStorer;
    private const string NameRegex = @"[^A-Za-z0-9]+";

    public PropertiesStorage(
        IPropertiesRetriever propertiesRetriever,
        IPropertiesStorer propertiesStorer)
    {
        _propertiesRetriever = propertiesRetriever;
        _propertiesStorer = propertiesStorer;
    }

    public void Store(ContainerIdentifier container, PropertyDictionary properties, string name = "_Default")
    {
        try
        {
            name = Regex.Replace(name, NameRegex, "_", RegexOptions.Singleline);
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
            name = Regex.Replace(name, NameRegex, "_", RegexOptions.Singleline);
            return _propertiesRetriever.Retrieve(container, name);
        }
        catch (Exception e)
        {
            var message = $"Unable to retrieve properties from the specified container ({name})";
            throw new StorageException(message, e);
        }
    }
}
