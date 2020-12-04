﻿namespace EtAlii.Ubigia.Api.Logical
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

        // ReSharper disable InconsistentNaming
        public static readonly GraphRelation AllChildren = new("ALLCHILDREN");
        public static readonly GraphRelation Children = new("CHILDREN");
        public static readonly GraphRelation AllParents = new("ALLPARENTS");
        public static readonly GraphRelation Parent = new("PARENT");
        public static readonly GraphRelation AllNext = new("ALLNEXT");
        public static readonly GraphRelation Next = new("NEXT");
        public static readonly GraphRelation AllPrevious = new("ALLPREVIOUS");
        public static readonly GraphRelation Previous = new("PREVIOUS");
        public static readonly GraphRelation AllUpdates = new("ALLUPDATES");
        public static readonly GraphRelation Updates = new("UPDATES");
        public static readonly GraphRelation AllDowndates = new("ALLDOWNDATES");
        public static readonly GraphRelation Downdate = new("DOWNDATE");
        public static readonly GraphRelation First = new("FIRST");
        public static readonly GraphRelation Last = new("LAST");
        public static readonly GraphRelation Original = new("ORIGINAL");
        public static readonly GraphRelation Final = new("FINAL");
        // ReSharper restore InconsistentNaming
    }
}