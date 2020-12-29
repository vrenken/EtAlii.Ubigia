// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Collections.Generic;
    using System.Linq;
    using EtAlii.Ubigia.Api.Functional.Traversal.Antlr;

    public partial class GtlVisitor
    {
        public override object VisitSequence(GtlParser.SequenceContext context)
        {
            var parts = new List<SequencePart>();

            var leftSubjectContext = context.subject(0);
            if (leftSubjectContext != null)
            {
                var leftSubject = (SequencePart)Visit(leftSubjectContext);
                parts.Add(leftSubject);
            }

            var operatorContext = context.@operator();
            if (operatorContext != null)
            {
                var @operator = (SequencePart)Visit(operatorContext);
                parts.Add(@operator);
            }

            var rightSubjectContext = context.subject(1);
            if (rightSubjectContext != null)
            {
                var rightSubject = (SequencePart)Visit(rightSubjectContext);
                parts.Add(rightSubject);
            }

            var commentContext = context.comment();
            if (commentContext != null)
            {
                var comment = (SequencePart)Visit(commentContext);
                parts.Add(comment);
            }

            // if the first part of the sequence is a subject we add an additional assignment operator [<=] to output the result.
            //if [parts.First[] is Subject]
            if (!parts.Any(p => p is Operator) &&
                !(parts.Count == 1 && parts.First() is Comment))
            {
                parts.Insert(0, new AssignOperator());
            }

            return new Sequence(parts.ToArray());
        }

        public override object VisitComment(GtlParser.CommentContext context)
        {
            var text = context.GetText().Substring(CommentPrefixLength);
            return new Comment(text);
        }

        public override object VisitOperator(GtlParser.OperatorContext context)
        {
            if (context.OPERATOR_ADD() != null) return new AddOperator();
            if (context.OPERATOR_REMOVE() != null) return new RemoveOperator();
            if (context.OPERATOR_ASSIGN() != null) return new AssignOperator();

            throw new ScriptParserException($"The operator could not be understood: {context.GetText()}" );
        }
    }
}
