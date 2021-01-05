// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Collections.Generic;
    using Antlr4.Runtime.Tree;
    using EtAlii.Ubigia.Api.Functional.Traversal.Antlr;

    public static class ParseTreeHelper
    {
        public static (IParseTree before, IParseTree after, IParseTree first) GetPathSiblings(IParseTree current)
        {
            IParseTree previous = null, first = null;
            var partContext = GetParent<GtlParser.Path_partContext>(current);
            var parent = partContext.Parent;
            var childCount = parent.ChildCount;

            IParseTree MatchDepth(IParseTree childToDive) => childToDive?.GetChild(0);

            for (var i = 0; i < childCount; i++)
            {
                var child = parent.GetChild(i);

                first ??= child;

                if (MatchDepth(child) == current)
                {
                    var before = previous;
                    var after= parent.GetChild(i + 1);

                    return (MatchDepth(before), MatchDepth(after), MatchDepth(first));
                }

                previous = child;
            }

            return (null, null, null);
        }

        public static (IParseTree before, IParseTree after, IParseTree first) GetSequenceSiblings(IParseTree current)
        {
            IParseTree previous = null, first = null;
            var parent = GetParent<GtlParser.SequenceContext>(current);

            IParseTree MatchDepth(IParseTree childToDive) => childToDive?.GetChild(0);


            var children = new List<IParseTree>();

            var childCount = parent.ChildCount;
            for (var i = 0; i < childCount; i++)
            {
                var child = parent.GetChild(i);
                if (child is GtlParser.Operator_subject_pairContext operatorSubjectPairContext)
                {
                    children.Add(operatorSubjectPairContext.@operator());
                    children.Add(operatorSubjectPairContext.subject());
                }
                else if (child is GtlParser.Subject_operator_pairContext subjectOperatorPairContext)
                {
                    children.Add(subjectOperatorPairContext.subject());
                    children.Add(subjectOperatorPairContext.@operator());
                }
                else
                {
                    children.Add(child.GetChild(0));
                }
            }

            var allChildCount = children.Count;
            for (var i = 0; i < allChildCount; i++)
            {
                var child = children[i];

                first ??= child;

                if (MatchDepth(child) == current)
                {
                    var before = previous;
                    var after= i < children.Count - 1 ? children[i+ + 1] : null;

                    return (MatchDepth(before), MatchDepth(after), MatchDepth(first));
                }

                previous = child;
            }

            return (null, null, null);
        }

        private static TParent GetParent<TParent>(IParseTree current)
        {
            var parent = current.Parent;
            while (parent is not TParent && parent != null)
            {
                parent = parent.Parent;
            }

            return (TParent)parent;
        }
    }
}
