// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.Ubigia.Api.Functional.Traversal.Antlr;

    public partial class GtlVisitor
    {
        public override object VisitOperator(GtlParser.OperatorContext context)
        {
            var result = base.VisitOperator(context);

            var (before, after, _) = ParseTreeHelper.GetSequenceSiblings(context);

            if (before is GtlParser.OperatorContext || after is GtlParser.OperatorContext)
            {
                throw new ScriptParserException("Two operators cannot be combined.");
            }
            if(before is GtlParser.CommentContext)
            {
                throw new ScriptParserException("A operator cannot used in combination with comments.");
            }

            return result;
        }

        public override object VisitOperator_add(GtlParser.Operator_addContext context) => new AddOperator();

        public override object VisitOperator_assign(GtlParser.Operator_assignContext context)
        {
            var (before, after, _) = ParseTreeHelper.GetSequenceSiblings(context);

            var beforeIsPath = before is GtlParser.Subject_non_rooted_pathContext || before is GtlParser.Subject_rooted_pathContext;
            var afterIsPath = after is GtlParser.Subject_non_rooted_pathContext || after is GtlParser.Subject_rooted_pathContext;
            if (beforeIsPath && afterIsPath)
            {
                throw new ScriptParserException("The assign operator cannot assign a path to another path.");
            }

            return new AssignOperator();
        }

        public override object VisitOperator_remove(GtlParser.Operator_removeContext context) => new RemoveOperator();
    }
}
