// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Antlr
{
    using EtAlii.Ubigia.Api.Functional.Traversal;

    public partial class UbigiaVisitor
    {
        public override object VisitOperator_add(UbigiaParser.Operator_addContext context) => new AddOperator();

        public override object VisitOperator_assign(UbigiaParser.Operator_assignContext context) => new AssignOperator();

        public override object VisitOperator_remove(UbigiaParser.Operator_removeContext context) => new RemoveOperator();
    }
}
