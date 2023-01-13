// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

internal class RecursiveRemoveResult
{
    //private readonly Identifier _parentId
    public readonly IEditableEntry NewEntry;

    public RecursiveRemoveResult(
        //Identifier parentId,
        IEditableEntry newEntry)
    {
        //_parentId = parentId
        NewEntry = newEntry;
    }
}
