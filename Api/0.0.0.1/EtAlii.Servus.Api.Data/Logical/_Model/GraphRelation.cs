﻿namespace EtAlii.Servus.Api.Data
{
    public class GraphRelation : GraphPathPart
    {
        public string Name { get; set; }

        private GraphRelation(string name)
        {
            Name = name;
        }

        public static readonly GraphRelation Child = new GraphRelation("CHILD");
        public static readonly GraphRelation Parent = new GraphRelation("PARENT");
        public static readonly GraphRelation Next = new GraphRelation("NEXT");
        public static readonly GraphRelation Previous = new GraphRelation("PREVIOUS");
        public static readonly GraphRelation Update = new GraphRelation("UPDATE");
        public static readonly GraphRelation Downdate = new GraphRelation("DOWNDATE");
        public static readonly GraphRelation First = new GraphRelation("FIRST");
        public static readonly GraphRelation Last = new GraphRelation("LAST");
        public static readonly GraphRelation Original = new GraphRelation("ORIGINAL");
        public static readonly GraphRelation Final = new GraphRelation("FINAL");
    }
}