// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence;

public class PropertiesRetriever : IPropertiesRetriever
{
    private readonly IPathBuilder _pathBuilder;
    private readonly IFileManager _fileManager;

    public PropertiesRetriever(
        IPathBuilder pathBuilder,
        IFileManager fileManager)
    {
        _pathBuilder = pathBuilder;
        _fileManager = fileManager;
    }

    public PropertyDictionary Retrieve(ContainerIdentifier container, string name)
    {
        PropertyDictionary properties = null;
        container = ContainerIdentifier.Combine(container, "Properties");

        var fileName = _pathBuilder.GetFileName(name, container);
        if (_fileManager.Exists(fileName))
        {
            properties = _fileManager.LoadFromFile(fileName);
            PropertiesHelper.SetStored(properties, true);
        }

        return properties;
    }
}
