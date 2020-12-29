// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Linq;
    using EtAlii.Ubigia.Api.Functional.Traversal.Antlr;

    public partial class GtlVisitor : GtlParserBaseVisitor<object>
    {
        private const int CommentPrefixLength = 2;

        public override object VisitScript(GtlParser.ScriptContext context)
        {
            var sequences = context.children
                .Select(sequenceContext => Visit(sequenceContext) as Sequence)
                .ToArray();

            return new Script(sequences);
        }
    }
}
