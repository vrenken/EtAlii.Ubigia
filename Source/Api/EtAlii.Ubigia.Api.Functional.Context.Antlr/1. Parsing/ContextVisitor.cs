// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.Ubigia.Api.Functional.Context.Antlr;
    using EtAlii.Ubigia.Api.Functional.Traversal;

    public partial class ContextVisitor : ContextSchemaParserBaseVisitor<object>
    {
        private readonly IPathParser _pathParser;

        public ContextVisitor(IPathParser pathParser)
        {
            _pathParser = pathParser;
        }

        public override object VisitSchema(ContextSchemaParser.SchemaContext context)
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

        public override object VisitHeader_option_namespace(ContextSchemaParser.Header_option_namespaceContext context) => context.@namespace().GetText();
    }
}
