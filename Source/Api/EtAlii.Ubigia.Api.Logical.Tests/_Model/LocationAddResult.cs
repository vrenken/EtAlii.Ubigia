// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Tests
{
    public class LocationAddResult
    {

        public readonly string Path;
        public readonly IEditableEntry Entry;

        public LocationAddResult(string path, IEditableEntry entry)
        {
            Path = path;
            Entry = entry;
        }
    }
}
