// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence;

public interface IPathBuilder
{
    string BaseFolder { get; }
    string GetFolder(ContainerIdentifier container);
    string GetFileName(string fileId, ContainerIdentifier container);

    string GetFileNameWithoutExtension(string path);
    string Combine(string path1, string path2);
    string GetDirectoryName(string path);
}
