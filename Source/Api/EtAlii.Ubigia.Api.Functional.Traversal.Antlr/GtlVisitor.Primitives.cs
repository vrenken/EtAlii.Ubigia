// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Globalization;
    using EtAlii.Ubigia.Api.Functional.Traversal.Antlr;

    public partial class GtlVisitor
    {
        // We use CultureInfo.InvariantCulture to ensure the . is always used as separator.
        public override object VisitString_quoted(GtlParser.String_quotedContext context) => context.STRING_QUOTED().GetText().Trim('"', '\'');

        public override object VisitBoolean_literal(GtlParser.Boolean_literalContext context) => bool.Parse(context.BOOLEAN_LITERAL().GetText());

        public override object VisitFloat_literal(GtlParser.Float_literalContext context) => float.Parse(context.FLOAT_LITERAL().GetText(), CultureInfo.InvariantCulture);

        public override object VisitFloat_literal_unsigned(GtlParser.Float_literal_unsignedContext context) => float.Parse(context.FLOAT_LITERAL_UNSIGNED().GetText(), CultureInfo.InvariantCulture);

        public override object VisitInteger_literal(GtlParser.Integer_literalContext context) => int.Parse(context.INTEGER_LITERAL().GetText(), CultureInfo.InvariantCulture);

        public override object VisitInteger_literal_unsigned(GtlParser.Integer_literal_unsignedContext context) => int.Parse(context.INTEGER_LITERAL_UNSIGNED().GetText(), CultureInfo.InvariantCulture);

        public override object VisitIdentifier(GtlParser.IdentifierContext context) => context.IDENTIFIER()?.GetText();

        public override object VisitDatetime(GtlParser.DatetimeContext context) => DateTime.Parse(context.GetText(), CultureInfo.InvariantCulture);
    }
}
