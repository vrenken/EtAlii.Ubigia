// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.Ubigia.Api.Functional.Traversal.Antlr;

    public partial class GtlVisitor
    {
        public override object VisitString_quoted(GtlParser.String_quotedContext context)
        {
            return context.STRING_QUOTED().GetText().Trim('"', '\'');
        }
    }
}
