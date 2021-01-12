// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.Ubigia.Api.Functional.Context.Antlr;

    public partial class ContextVisitor
    {
        public override object VisitIdentifier(ContextSchemaParser.IdentifierContext context) => context?.GetText();

        public override object VisitString_quoted(ContextSchemaParser.String_quotedContext context)
        {
            var text = context?.STRING_QUOTED()?.GetText();
            return text?.Substring(1, text.Length - 2);
        }

        public override object VisitString_quoted_non_empty(ContextSchemaParser.String_quoted_non_emptyContext context)
        {
            var text = context?.STRING_QUOTED_NON_EMPTY()?.GetText();
            return text?.Substring(1, text.Length - 2);
        }
    }
}
