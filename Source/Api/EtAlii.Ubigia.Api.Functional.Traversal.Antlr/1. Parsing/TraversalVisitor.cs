// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Linq;
    using EtAlii.Ubigia.Api.Functional.Traversal.Antlr;

    public partial class TraversalVisitor : TraversalScriptParserBaseVisitor<object>
    {
        private const int CommentPrefixLength = 2;

        public override object VisitScript(TraversalScriptParser.ScriptContext context)
        {
            var sequences = context.sequence()
                .Select(sequenceContext => Visit(sequenceContext) as Sequence)
                .Where(sequence => sequence != null)
                .ToArray();

            return new Script(sequences);
        }
    }
}