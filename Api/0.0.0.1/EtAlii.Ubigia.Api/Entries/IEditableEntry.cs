namespace EtAlii.Ubigia.Api
{
    public interface IEditableEntry
    {
        Identifier Id{ get; set; }

        Relation Parent { get; set; }
        ChildrenComponentCollection Children { get; }

        Relation Parent2 { get; set; }
        Children2ComponentCollection Children2 { get; }

        IndexesComponentCollection Indexes { get; }
        Relation Indexed { get; set; }

        Relation Previous { get; set; }
        Relation Next{ get; set; }

        Relation Downdate { get; set; }
        UpdatesComponentCollection Updates { get; }

        string Type { get; set; }
    }
}
