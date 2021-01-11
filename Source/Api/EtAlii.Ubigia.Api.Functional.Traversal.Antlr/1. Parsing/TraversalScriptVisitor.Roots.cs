// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.Ubigia.Api.Functional.Traversal.Antlr;

    public partial class TraversalScriptVisitor
    {
        public override object VisitSubject_root(TraversalScriptParser.Subject_rootContext context)
        {
            var name = context.identifier().GetText();
            return new RootSubject(name);
        }

        public override object VisitSubject_root_definition(TraversalScriptParser.Subject_root_definitionContext context)
        {
            var type = context.GetText();
            return new RootDefinitionSubject(type);
        }
    }
}
