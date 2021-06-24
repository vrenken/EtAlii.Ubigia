// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.InMemory
{
    public interface IInMemoryItems
    {
        Item Find(string path);
        Item Find(string path, Folder folder);

        bool Exists(string path);
        void Move(string sourcePath, string targetPath);
        void Delete(string path);
    }
}
