// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.Ubigia.Api.Functional.Traversal.Antlr;

    public partial class GtlVisitor
    {

        public override object VisitOperandOnlySequence(GtlParser.OperandOnlySequenceContext context)
        {
            var operandContext = context.operand();
            var subject = VisitOperand(operandContext) as Subject;
            return new Sequence(new SequencePart[]{ subject });
        }

        public override object VisitOperatorOperandSequence(GtlParser.OperatorOperandSequenceContext context)
        {
            var text = context.GetText().Substring(CommentPrefixLength);
            var part = new Comment(text);
            return new Sequence(new SequencePart[]{ part });
        }

        public override object VisitOperandOperatorOperandSequence(GtlParser.OperandOperatorOperandSequenceContext context)
        {
            var text = context.GetText().Substring(2);
            var part = new Comment(text);
            return new Sequence(new SequencePart[]{ part });
        }

        public override object VisitCommentSequence(GtlParser.CommentSequenceContext context)
        {
            var text = context.GetText().Substring(CommentPrefixLength);
            var part = new Comment(text);
            return new Sequence(new SequencePart[]{ part });
        }
    }
}
