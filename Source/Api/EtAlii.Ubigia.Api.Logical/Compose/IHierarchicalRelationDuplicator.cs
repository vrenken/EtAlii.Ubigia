// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    public interface IHierarchicalRelationDuplicator
    {
        void Duplicate(IReadOnlyEntry source, IEditableEntry target);
        void Duplicate(IReadOnlyEntry source, IEditableEntry target, in Identifier relationToExclude);
    }
}
