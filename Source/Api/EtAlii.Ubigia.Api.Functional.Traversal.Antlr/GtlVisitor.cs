// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Linq;
    using Antlr4.Runtime.Tree;
    using EtAlii.Ubigia.Api.Functional.Traversal.Antlr;

    public partial class GtlVisitor : GtlParserBaseVisitor<object>
    {
        private const int CommentPrefixLength = 2;

        public override object VisitScript(GtlParser.ScriptContext context)
        {
            var sequences = context.sequence()
                .Select(sequenceContext => VisitSequence(sequenceContext) as Sequence)
                .Where(sequence => sequence != null)
                .ToArray();

            return new Script(sequences);
        }

        private (IParseTree before, IParseTree after) GetSiblings(IParseTree current, IParseTree parent)
        {
            IParseTree previous = null;
            var childCount = parent.ChildCount;
            for (var i = 0; i < childCount; i++)
            {
                var child = parent.GetChild(i);
                if (child == current)
                {
                    var before = previous;
                    var after
                        = parent.GetChild(i + 1);
                    return (before, after);
                }

                previous = child;
            }

            return (null, null);
        }

        private (IParseTree before, IParseTree after) GetSiblings(IParseTree current) => GetSiblings(current, current.Parent);

        private (IParseTree before, IParseTree after) GetSiblings<TParent>(IParseTree current)
            where TParent: IParseTree
        {
            var parent = GetParent<TParent>(current, out var childOfParent);
            return GetSiblings(childOfParent, parent);
        }

        private IParseTree GetParent<TParent>(IParseTree current, out IParseTree parentOfChild)
        {
            var parent = current.Parent;
            parentOfChild = current;
            while (parent is not TParent && parent != null)
            {
                parentOfChild = parent;
                parent = parent.Parent;
            }

            return parent;
        }
    }
}
