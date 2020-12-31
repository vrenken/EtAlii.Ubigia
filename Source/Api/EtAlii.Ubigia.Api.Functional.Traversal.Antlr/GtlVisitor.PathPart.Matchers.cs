// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using EtAlii.Ubigia.Api.Functional.Traversal.Antlr;

    public partial class GtlVisitor
    {
        public override object VisitPath_part_matcher_tag_tag_only(GtlParser.Path_part_matcher_tag_tag_onlyContext context)
        {
            var tag = context.IDENTIFIER()?.GetText();
            return new TaggedPathSubjectPart(null, tag);
        }

        public override object VisitPath_part_matcher_tag_name_only(GtlParser.Path_part_matcher_tag_name_onlyContext context)
        {
            var name = context.IDENTIFIER()?.GetText();
            return new TaggedPathSubjectPart(name, null);
        }

        public override object VisitPath_part_matcher_tag_and_name(GtlParser.Path_part_matcher_tag_and_nameContext context)
        {
            var name = context.IDENTIFIER(0)?.GetText();
            var tag = context.IDENTIFIER(1)?.GetText();

            return new TaggedPathSubjectPart(name, tag);
        }

        public override object VisitPath_part_matcher_identifier(GtlParser.Path_part_matcher_identifierContext context)
        {
            var parts = context
                .GetText()
                .Substring(1).Split('.');
            var storage = Guid.Parse(parts[0]);
            var account = Guid.Parse(parts[1]);
            var space = Guid.Parse(parts[2]);

            var era = ulong.Parse(parts[3]);
            var period = ulong.Parse(parts[4]);
            var moment = ulong.Parse(parts[5]);

            var identifier = Identifier.Create(storage, account, space, era, period, moment);

            return new IdentifierPathSubjectPart(identifier);
        }

        public override object VisitPath_part_matcher_variable(GtlParser.Path_part_matcher_variableContext context)
        {
            var text = context.IDENTIFIER().GetText();
            return new VariablePathSubjectPart(text);
        }

        public override object VisitPath_part_matcher_constant_integer(GtlParser.Path_part_matcher_constant_integerContext context)
        {
            var text = context.INTEGER_LITERAL_UNSIGNED().GetText();
            return new ConstantPathSubjectPart(text);
        }

        public override object VisitPath_part_matcher_constant_identifier(GtlParser.Path_part_matcher_constant_identifierContext context)
        {
            var text = context.IDENTIFIER().GetText();
            return new ConstantPathSubjectPart(text);
        }

        public override object VisitPath_part_matcher_constant_quoted(GtlParser.Path_part_matcher_constant_quotedContext context)
        {
            var text = (string)VisitString_quoted(context.string_quoted());
            return new ConstantPathSubjectPart(text);
        }

        public override object VisitPath_part_matcher_constant_unquoted(GtlParser.Path_part_matcher_constant_unquotedContext context)
        {
            var text = context.STRING_UNQUOTED().GetText();
            return new ConstantPathSubjectPart(text);
        }
    }
}
