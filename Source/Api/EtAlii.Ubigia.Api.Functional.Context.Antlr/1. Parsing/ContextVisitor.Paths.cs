// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.Ubigia.Api.Functional.Context.Antlr;

    public partial class ContextVisitor
    {
        public override object VisitNon_rooted_path(ContextSchemaParser.Non_rooted_pathContext context) => _pathParser.ParseNonRootedPath(context.GetText());
        public override object VisitRooted_path(ContextSchemaParser.Rooted_pathContext context) => _pathParser.ParseRootedPath(context.GetText());
    }
}
