// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.Standard;

using System;
using System.IO;

public class StandardPathBuilder : IPathBuilder
{
    public string BaseFolder { get; private set; }

    private readonly IStorageOptions _options;
    private readonly IStorageSerializer _serializer;
    private readonly char _separatorChar;
    private readonly string _separatorString;

    public StandardPathBuilder(
        IStorageOptions options,
        IStorageSerializer serializer)
    {
        _separatorChar = Path.DirectorySeparatorChar;
        _separatorString = new string(_separatorChar, 1);

        _options = options;
        _serializer = serializer;
    }

    public void Initialize(string baseFolder)
    {
        var folder = Environment.ExpandEnvironmentVariables(baseFolder);
        BaseFolder = string.Join(_separatorString, folder, _options.Name);
    }

    public string GetFolder(ContainerIdentifier container)
    {
        var relativePath = string.Join(_separatorString, container.Paths);
        return Path.Combine(BaseFolder, relativePath);
    }

    public string GetFileName(string fileId, ContainerIdentifier container)
    {
        var folder = GetFolder(container);
        var fileName = string.Format(_serializer.FileNameFormat, fileId);
        return string.Join(_separatorString, folder, fileName);
    }

    public string GetFileNameWithoutExtension(string path)
    {
        return Path.GetFileNameWithoutExtension(path);
    }

    public string Combine(string path1, string path2)
    {
        return string.Join(_separatorString, path1, path2);
    }

    public string GetDirectoryName(string path)
    {
        var lastIndex = path.LastIndexOf(_separatorChar);
        return lastIndex == -1 ? string.Empty : path.Substring(0, lastIndex);
    }
}
