namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api.Logical;

    public class NodeQueryable<T> : Queryable<T>
        where T: INode
    {
        public string StartPath { get; }
        public Root StartRoot { get; }
        public Identifier StartIdentifier { get; }

        internal NodeQueryable(IQueryProvider queryProvider, string startPath)
            : base(queryProvider)
        {
            if (String.IsNullOrWhiteSpace(startPath))
            {
                throw new ArgumentException(nameof(startPath));
            }

            StartPath = startPath;
        }

        internal NodeQueryable(IQueryProvider queryProvider, Root startRoot, string startPath)
            : this(queryProvider, startPath)
        {
            StartRoot = startRoot ?? throw new ArgumentNullException(nameof(startRoot));
        }

        internal NodeQueryable(IQueryProvider queryProvider, Root startRoot)
            : base(queryProvider)
        {
            StartRoot = startRoot ?? throw new ArgumentNullException(nameof(startRoot));
        }

        internal NodeQueryable(IQueryProvider queryProvider, Identifier startIdentifier)
            : base(queryProvider)
        {
            if (startIdentifier == Identifier.Empty)
            {
                throw new ArgumentNullException(nameof(startIdentifier));
            }
            StartIdentifier = startIdentifier;
        }
    }
}