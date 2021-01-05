// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.Ubigia.Api.Functional.Traversal.Antlr;

    public partial class GtlVisitor
    {
        public override object VisitSubject_root(GtlParser.Subject_rootContext context)
        {
            var name = context.identifier().GetText();
            return new RootSubject(name);
        }

        public override object VisitSubject_root_definition(GtlParser.Subject_root_definitionContext context)
        {
            var type = context.GetText();
            return new RootDefinitionSubject(type);
        }
    }
}
