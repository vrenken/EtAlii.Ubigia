// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Collections.Generic;
    using EtAlii.Ubigia.Api.Functional.Traversal.Antlr;

    public class GtlVisitor : GtlParserBaseVisitor<object>
    {
        public readonly List<GtlLine> Lines = new();

        public override object VisitLine(GtlParser.LineContext context)
        {
            var name = context.name();
            var opinion = context.opinion();

            var line = new GtlLine(name.GetText(), opinion.GetText().Trim('"'));
            Lines.Add(line);

            return line;
        }

        public override object VisitScript(GtlParser.ScriptContext context)
        {
            var parts = Array.Empty<SequencePart>();
            var sequence = new Sequence(parts);
            return new Script(sequence);
        }
    }
}
