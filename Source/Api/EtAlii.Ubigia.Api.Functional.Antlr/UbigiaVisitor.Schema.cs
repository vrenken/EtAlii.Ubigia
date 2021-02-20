// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Antlr
{
    using EtAlii.Ubigia.Api.Functional.Context;

    public partial class UbigiaVisitor
    {
        public override object VisitSchema(UbigiaParser.SchemaContext context)
        {
            var fragmentContext = context.structure_fragment();

            var namespaceContext = context.header_option_namespace();

            var @namespace = namespaceContext != null
                ? (string)VisitHeader_option_namespace(namespaceContext)
                : null;
            return fragmentContext != null
                ? new Schema((StructureFragment)VisitStructure_fragment(fragmentContext), @namespace, null)
                : null;
        }

        public override object VisitHeader_option_namespace(UbigiaParser.Header_option_namespaceContext context) => context.@namespace().GetText();
    }
}
