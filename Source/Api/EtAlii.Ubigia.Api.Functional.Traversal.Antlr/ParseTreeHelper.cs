// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using Antlr4.Runtime.Tree;
    using EtAlii.Ubigia.Api.Functional.Traversal.Antlr;

    public static class ParseTreeHelper
    {
        public static (IParseTree before, IParseTree after, IParseTree first) GetPathSiblings(IParseTree current)
        {
            var partContext = GetParent<GtlParser.Path_partContext>(current, out _);

            var(before, after, first) = GetSiblings(partContext, partContext.Parent);

            return (before?.GetChild(0), after?.GetChild(0), first?.GetChild(0));
        }

        public static (IParseTree before, IParseTree after, IParseTree first) GetSequenceSiblings(IParseTree current)
        {
            var parent = GetParent<GtlParser.SequenceContext>(current, out _);

            var(before, after, first) = GetSiblings(current, parent);

            return (before?.GetChild(0), after?.GetChild(0), first?.GetChild(0));
        }

        private static (IParseTree before, IParseTree after, IParseTree first) GetSiblings(IParseTree current, IParseTree parent)
        {
            IParseTree previous = null, first = null;
            var childCount = parent.ChildCount;
            for (var i = 0; i < childCount; i++)
            {
                var child = parent.GetChild(i);

                first ??= child;

                if (child == current)
                {
                    var before = previous;
                    var after
                        = parent.GetChild(i + 1);
                    return (before, after, first);
                }

                previous = child;
            }

            return (null, null, null);
        }

        private static TParent GetParent<TParent>(IParseTree current, out IParseTree parentOfChild)
        {
            var parent = current.Parent;
            parentOfChild = current;
            while (parent is not TParent && parent != null)
            {
                parentOfChild = parent;
                parent = parent.Parent;
            }

            return (TParent)parent;
        }
    }
}
