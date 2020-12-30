// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.Ubigia.Api.Functional.Traversal.Antlr;

    public partial class GtlVisitor
    {
        public override object VisitOperatorAdd(GtlParser.OperatorAddContext context) => new AddOperator();
        public override object VisitOperatorAssign(GtlParser.OperatorAssignContext context) => new AssignOperator();
        public override object VisitOperatorRemove(GtlParser.OperatorRemoveContext context) => new RemoveOperator();
    }
}
