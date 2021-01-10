// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.Ubigia.Api.Functional.Traversal.Antlr;

    public partial class TraversalScriptVisitor
    {
        public override object VisitOperator_add(TraversalScriptParser.Operator_addContext context) => new AddOperator();

        public override object VisitOperator_assign(TraversalScriptParser.Operator_assignContext context) => new AssignOperator();

        public override object VisitOperator_remove(TraversalScriptParser.Operator_removeContext context) => new RemoveOperator();
    }
}
