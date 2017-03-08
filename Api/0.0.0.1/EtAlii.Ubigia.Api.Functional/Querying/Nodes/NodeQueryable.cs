﻿namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api.Logical;

    public class NodeQueryable<T> : Queryable<T>
        where T: INode
    {
        public string StartPath { get; }
        public Root StartRoot { get; }
        public Identifier StartIdentifier { get; private set; }

        internal NodeQueryable(IQueryProvider queryProvider, string startPath)
            : base(queryProvider)
        {
            if (String.IsNullOrEmpty(startPath))
            {
                throw new ArgumentNullException("startPath");
            }
            if (String.IsNullOrWhiteSpace(startPath))
            {
                throw new ArgumentException("startPath");
            }

            StartPath = startPath;
        }

        internal NodeQueryable(IQueryProvider queryProvider, Root startRoot, string startPath)
            : this(queryProvider, startPath)
        {
            if (startRoot == null)
            {
                throw new ArgumentNullException("startRoot");
            }
            StartRoot = startRoot;
        }

        internal NodeQueryable(IQueryProvider queryProvider, Root startRoot)
            : base(queryProvider)
        {
            if (startRoot == null)
            {
                throw new ArgumentNullException("startRoot");
            }
            StartRoot = startRoot;
        }

        internal NodeQueryable(IQueryProvider queryProvider, Identifier startIdentifier)
            : base(queryProvider)
        {
            if (startIdentifier == Identifier.Empty)
            {
                throw new ArgumentNullException("startIdentifier");
            }
            StartIdentifier = startIdentifier;
        }
    }
}