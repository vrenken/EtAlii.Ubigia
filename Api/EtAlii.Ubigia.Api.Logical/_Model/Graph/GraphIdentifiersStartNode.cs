namespace EtAlii.Ubigia.Api.Logical
{
    using System.Collections.Generic;
    using System.Linq;

    public class GraphIdentifiersStartNode : GraphPathPart
    {
        public readonly IEnumerable<Identifier> Identifiers;

        public GraphIdentifiersStartNode(IEnumerable<Identifier> identifiers)
        {
            Identifiers = identifiers;
        }

        public GraphIdentifiersStartNode(Identifier identifier)
        {
            Identifiers = new[] { identifier };
        }

        public override string ToString()
        {
            return Identifiers.Count() == 1 ? Identifiers.Single().ToTimeString() : "Multiple starts";
        }
    }
}