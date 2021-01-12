// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.Ubigia.Api.Functional.Context.Antlr;
    using EtAlii.Ubigia.Api.Functional.Traversal;

    public partial class ContextVisitor : ContextSchemaParserBaseVisitor<object>
    {
        private readonly IScriptParser _scriptParser;

        public ContextVisitor(IScriptParser scriptParser)
        {
            _scriptParser = scriptParser;
        }

        public override object VisitSchema(ContextSchemaParser.SchemaContext context)
        {
            var fragment = (StructureFragment)VisitStructure_fragment(context.structure_fragment());

            return new Schema(fragment);
        }
    }
}
