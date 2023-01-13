// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia;

public interface IEditableEntry
{
    Identifier Id{ get; set; }

    Relation Parent { get; set; }
    IReadOnlyRelationsComponentCollection<ChildrenComponent> Children { get; }
    public void AddChild(in Identifier id);

    Relation Parent2 { get; set; }
    IReadOnlyRelationsComponentCollection<Children2Component> Children2 { get; }
    public void AddChild2(in Identifier id);

    Relation Indexed { get; set; }
    IReadOnlyRelationsComponentCollection<IndexesComponent> Indexes { get; }
    public void AddIndex(in Identifier id);

    Relation Previous { get; set; }
    Relation Next{ get; set; }

    Relation Downdate { get; set; }
    IReadOnlyRelationsComponentCollection<UpdatesComponent> Updates { get; }
    public void AddUpdate(in Identifier id);

    string Type { get; set; }
    string Tag { get; set; }
}
