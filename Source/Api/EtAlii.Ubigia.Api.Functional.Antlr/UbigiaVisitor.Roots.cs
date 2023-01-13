// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Antlr;

using EtAlii.Ubigia.Api.Functional.Traversal;

public partial class UbigiaVisitor
{
    public override object VisitSubject_root(UbigiaParser.Subject_rootContext context)
    {
        var name = context.identifier().GetText();
        return new RootSubject(name);
    }

    public override object VisitSubject_root_definition(UbigiaParser.Subject_root_definitionContext context)
    {
        var type = context.GetText();
        return new RootDefinitionSubject(new RootType(type));
    }
}
