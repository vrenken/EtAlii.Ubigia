// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Collections.Generic;
    using EtAlii.Ubigia.Api.Functional.Traversal.Antlr;

    public partial class GtlVisitor
    {
        public override object VisitSequence(GtlParser.SequenceContext context)
        {
            var sequenceParts = new List<SequencePart>();

            var leftOperandContext = context.operand(0);
            if (leftOperandContext != null)
            {
                var leftOperand = (SequencePart)Visit(leftOperandContext);
                sequenceParts.Add(leftOperand);
            }

            var operatorContext = context.OPERATOR();
            if (operatorContext != null)
            {
                var @operator = (SequencePart)Visit(operatorContext);
                sequenceParts.Add(@operator);
            }

            var rightOperandContext = context.operand(1);
            if (rightOperandContext != null)
            {
                var rightOperand = (SequencePart)Visit(rightOperandContext);
                sequenceParts.Add(rightOperand);
            }

            var commentContext = context.comment();
            if (commentContext != null)
            {
                var comment = (SequencePart)Visit(commentContext);
                sequenceParts.Add(comment);
            }

            return new Sequence(sequenceParts.ToArray());
        }

        public override object VisitComment(GtlParser.CommentContext context)
        {
            var text = context.GetText().Substring(CommentPrefixLength);
            return new Comment(text);
        }
    }
}
