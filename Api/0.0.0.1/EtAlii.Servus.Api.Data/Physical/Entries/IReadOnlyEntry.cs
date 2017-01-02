using System.Collections.Generic;
namespace EtAlii.Servus.Api
{

    public interface IReadOnlyEntry
    {
        Identifier Id{ get; }

        Relation Parent { get; }
        IEnumerable<Relation> Children { get; }

        Relation Parent2 { get; }
        IEnumerable<Relation> Children2 { get; }

        Relation Previous { get; }
        Relation Next{ get; }

        Relation Downdate { get; }
        IEnumerable<Relation> Updates { get; }

        Relation Indexed { get; }
        IEnumerable<Relation> Indexes { get; }

        string Type { get; }
    }
}
