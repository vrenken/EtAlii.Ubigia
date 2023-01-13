// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.Azure;

using System;
using System.IO;

public class AzurePathBuilder : IPathBuilder
{
    public string BaseFolder { get; }

    public AzurePathBuilder(IStorageOptions options)
    {
        var folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        BaseFolder = Path.Combine(folder, "EtAlii", "Ubigia", options.Name);
    }

    public string GetFolder(ContainerIdentifier container)
    {
        throw new NotImplementedException();
    }

    public string GetFileName(string fileId, ContainerIdentifier container)
    {
        throw new NotImplementedException();
    }

    public string GetFileNameWithoutExtension(string path)
    {
        throw new NotImplementedException();
    }

    public string Combine(string path1, string path2)
    {
        throw new NotImplementedException();
    }

    public string GetDirectoryName(string path)
    {
        throw new NotImplementedException();
    }
}
