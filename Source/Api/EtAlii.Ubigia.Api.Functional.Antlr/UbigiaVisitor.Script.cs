// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Antlr
{
    using System.Linq;
    using EtAlii.Ubigia.Api.Functional.Traversal;

    public partial class UbigiaVisitor : UbigiaParserBaseVisitor<object>
    {
        private const int CommentPrefixLength = 2;

        public override object VisitScript(UbigiaParser.ScriptContext context)
        {
            var sequences = context.sequence()
                .Select(sequenceContext => Visit(sequenceContext) as Sequence)
                .Where(sequence => sequence != null)
                .ToArray();

            return new Script(sequences);
        }
    }
}
