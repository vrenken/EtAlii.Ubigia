// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    public interface IComponentEditableEntry
    {
        IdentifierComponent IdComponent { get; set; }

        ParentComponent ParentComponent { get; set; }
        IReadOnlyRelationsComponentCollection<ChildrenComponent> ChildrenComponent { get; }
        void AddChildren(Relation[] relations, bool markAsStored);

        Parent2Component Parent2Component { get; set; }
        IReadOnlyRelationsComponentCollection<Children2Component> Children2Component { get; }
        void AddChildren2(Relation[] relations, bool markAsStored);

        IndexedComponent IndexedComponent { get; set; }
        IReadOnlyRelationsComponentCollection<IndexesComponent> IndexesComponent { get; }
        void AddIndexes(Relation[] relations, bool markAsStored);

        PreviousComponent PreviousComponent { get; set; }
        NextComponent NextComponent { get; set; }

        DowndateComponent DowndateComponent { get; set; }
        IReadOnlyRelationsComponentCollection<UpdatesComponent> UpdatesComponent { get; }
        void AddUpdates(Relation[] relations, bool markAsStored);

        TypeComponent TypeComponent { get; set; }

        TagComponent TagComponent { get; set; }
    }
}
