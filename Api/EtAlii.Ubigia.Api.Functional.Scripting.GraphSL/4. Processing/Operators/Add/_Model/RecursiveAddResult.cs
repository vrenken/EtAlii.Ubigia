﻿namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    internal class RecursiveAddResult
    {
        public readonly Identifier ParentId;
        public readonly IEditableEntry NewEntry;

        public RecursiveAddResult(Identifier parentId, IEditableEntry newEntry)
        {
            ParentId = parentId;
            NewEntry = newEntry;
        }
    }
}