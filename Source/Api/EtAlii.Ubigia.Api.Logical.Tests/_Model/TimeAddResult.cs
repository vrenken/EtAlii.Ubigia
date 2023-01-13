// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Tests;

public class TimeAddResult
{

    public readonly string Path;
    public readonly IEditableEntry Entry;

    public TimeAddResult(string path, IEditableEntry entry)
    {
        Path = path;
        Entry = entry;
    }
}
