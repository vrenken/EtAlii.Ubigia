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

            var leftSubjectContext = context.subject(0);
            if (leftSubjectContext != null)
            {
                var leftSubject = (SequencePart)Visit(leftSubjectContext);
                sequenceParts.Add(leftSubject);
            }

            var operatorContext = context.OPERATOR();
            if (operatorContext != null)
            {
                var @operator = (SequencePart)Visit(operatorContext);
                sequenceParts.Add(@operator);
            }

            var rightSubjectContext = context.subject(1);
            if (rightSubjectContext != null)
            {
                var rightSubject = (SequencePart)Visit(rightSubjectContext);
                sequenceParts.Add(rightSubject);
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
