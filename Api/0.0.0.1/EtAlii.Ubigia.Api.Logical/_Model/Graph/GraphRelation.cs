namespace EtAlii.Ubigia.Api.Logical
{
    using System.Diagnostics;

    [DebuggerDisplay("{" + nameof(_name) + "}")]
    public sealed class GraphRelation : GraphPathPart
    {
        private readonly string _name;

        private GraphRelation(string name)
        {
            _name = name;
        }

        public override string ToString()
        {
            return _name;
        }

        public static readonly GraphRelation AllChildren = new GraphRelation("ALLCHILDREN");
        public static readonly GraphRelation Children = new GraphRelation("CHILDREN");
        public static readonly GraphRelation AllParents = new GraphRelation("ALLPARENTS");
        public static readonly GraphRelation Parent = new GraphRelation("PARENT");
        public static readonly GraphRelation AllNext = new GraphRelation("ALLNEXT");
        public static readonly GraphRelation Next = new GraphRelation("NEXT");
        public static readonly GraphRelation AllPrevious = new GraphRelation("ALLPREVIOUS");
        public static readonly GraphRelation Previous = new GraphRelation("PREVIOUS");
        public static readonly GraphRelation AllUpdates = new GraphRelation("ALLUPDATES");
        public static readonly GraphRelation Updates = new GraphRelation("UPDATES");
        public static readonly GraphRelation AllDowndates = new GraphRelation("ALLDOWNDATES");
        public static readonly GraphRelation Downdate = new GraphRelation("DOWNDATE");
        public static readonly GraphRelation First = new GraphRelation("FIRST");
        public static readonly GraphRelation Last = new GraphRelation("LAST");
        public static readonly GraphRelation Original = new GraphRelation("ORIGINAL");
        public static readonly GraphRelation Final = new GraphRelation("FINAL");
    }
}