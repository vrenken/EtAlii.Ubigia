// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    public interface IComponentEditableEntry
    {
        IdentifierComponent IdComponent { get; set; }

        ParentComponent ParentComponent { get; set; }
        ChildrenComponentCollection ChildrenComponent { get; }

        Parent2Component Parent2Component { get; set; }
        Children2ComponentCollection Children2Component { get; }

        IndexesComponentCollection IndexesComponent { get; }
        IndexedComponent IndexedComponent { get; set; }

        PreviousComponent PreviousComponent { get; set; }
        NextComponent NextComponent { get; set; }

        DowndateComponent DowndateComponent { get; set; }

        IReadOnlyRelationsComponentCollection<UpdatesComponent> UpdatesComponent { get; }
        void AddUpdates(Relation[] relations, bool markAsStored);

        TypeComponent TypeComponent { get; set; }

        TagComponent TagComponent { get; set; }
    }
}
