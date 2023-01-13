// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Antlr;

using EtAlii.Ubigia.Api.Functional.Context;

public partial class UbigiaVisitor
{
    public override object VisitSchema(UbigiaParser.SchemaContext context)
    {
        var text = context.GetText();
        text = text
            .Substring(0, text.Length - 5) // Remove the <EOF> at the end.
            .Replace("\r\n","\n")
            .TrimEnd('\n');
        var fragmentContext = context.structure_fragment();

        var namespaceContext = context.header_option_namespace();

        var @namespace = namespaceContext != null
            ? (string)VisitHeader_option_namespace(namespaceContext)
            : null;
        return fragmentContext != null
            ? new Schema((StructureFragment)VisitStructure_fragment(fragmentContext), @namespace, null, text)
            : null;
    }

    public override object VisitHeader_option_namespace(UbigiaParser.Header_option_namespaceContext context) => context.@namespace().GetText();
}
