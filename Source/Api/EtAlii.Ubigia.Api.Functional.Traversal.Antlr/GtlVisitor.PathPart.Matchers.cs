// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using EtAlii.Ubigia.Api.Functional.Traversal.Antlr;

    public partial class GtlVisitor
    {
        public override object VisitPath_part_matcher_identifier(GtlParser.Path_part_matcher_identifierContext context)
        {
            var storage = Guid.Parse(context.GUID(0).GetText());
            var account = Guid.Parse(context.GUID(1).GetText());
            var space = Guid.Parse(context.GUID(2).GetText());

            var era = ulong.Parse(context.INTEGER_LITERAL_UNSIGNED(0).GetText());
            var period = ulong.Parse(context.INTEGER_LITERAL_UNSIGNED(1).GetText());
            var moment = ulong.Parse(context.INTEGER_LITERAL_UNSIGNED(2).GetText());

            var identifier = Identifier.Create(storage, account, space, era, period, moment);

            return new IdentifierPathSubjectPart(identifier);
        }

        public override object VisitPath_part_matcher_variable(GtlParser.Path_part_matcher_variableContext context)
        {
            var text = context.IDENTITY().GetText();
            return new VariablePathSubjectPart(text);
        }

        public override object VisitPath_part_matcher_constant(GtlParser.Path_part_matcher_constantContext context)
        {
            var identity = context.IDENTITY();
            if (identity != null) return new ConstantPathSubjectPart(identity.GetText());


            var stringQuoted = context.STRING_QUOTED();
            if (stringQuoted != null) return new ConstantPathSubjectPart(stringQuoted.GetText());

            var pathSubjectPart = base.VisitPath_part_matcher_constant(context);
            if (pathSubjectPart is not PathSubjectPart)
            {
                throw new ScriptParserException($"The constant path subject part could not be understood: {context.GetText()}" );
            }

            return pathSubjectPart;
        }
    }
}
