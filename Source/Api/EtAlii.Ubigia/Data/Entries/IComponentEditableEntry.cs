// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

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
        UpdatesComponentCollection UpdatesComponent { get; }

        TypeComponent TypeComponent { get; set; }

        TagComponent TagComponent { get; set; }
    }
}
