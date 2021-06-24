// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Antlr
{
    using System.Collections.Generic;
    using System.Linq;
    using EtAlii.Ubigia.Api.Functional.Traversal;

    public partial class UbigiaVisitor
    {
        public override object VisitSequence_pattern_1(UbigiaParser.Sequence_pattern_1Context context)
        {
            var parts = new List<SequencePart>();

            foreach (var subjectOperatorPair in context.subject_operator_pair())
            {
                var subject = (SequencePart)VisitSubject(subjectOperatorPair.subject());
                parts.Add(subject);
                var @operator = (SequencePart)VisitOperator(subjectOperatorPair.@operator());
                parts.Add(@operator);
            }

            var optionalSubjectContext = context.subject_optional();
            if (optionalSubjectContext != null)
            {
                var rightSubject = (SequencePart)Visit(optionalSubjectContext);
                parts.Add(rightSubject);
            }

            var commentContext = context.comment();
            if (commentContext != null)
            {
                var comment = (SequencePart)Visit(commentContext);
                parts.Add(comment);
            }

            // if the first part of the sequence is a subject we add an additional assignment operator [<=] to output the result.
            // if [parts.First[] is Subject]
            if (!parts.Any(p => p is Operator) &&
                !(parts.Count == 1 && parts.First() is Comment))
            {
                parts.Insert(0, new AssignOperator());
            }

            return new Sequence(parts.ToArray());
        }
        public override object VisitSequence_pattern_2(UbigiaParser.Sequence_pattern_2Context context)
        {
            var parts = new List<SequencePart>();

            foreach (var operatorSubjectPair in context.operator_subject_pair())
            {
                var @operator = (SequencePart)VisitOperator(operatorSubjectPair.@operator());
                parts.Add(@operator);
                var subject = (SequencePart)VisitSubject(operatorSubjectPair.subject());
                parts.Add(subject);
            }

            var optionalOperator = context.operator_optional();
            if (optionalOperator != null)
            {
                var @operator = (SequencePart)Visit(optionalOperator);
                parts.Add(@operator);
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
        public override object VisitSequence_pattern_3(UbigiaParser.Sequence_pattern_3Context context)
        {
            var parts = new List<SequencePart>();

            var subject = context.subject();
            if (subject != null)
            {
                var rightSubject = (SequencePart)Visit(subject);
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

        public override object VisitSequence_pattern_4(UbigiaParser.Sequence_pattern_4Context context)
        {
            var parts = new List<SequencePart>();

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

        public override object VisitComment(UbigiaParser.CommentContext context)
        {
            var text = context.GetText().Substring(CommentPrefixLength);
            return new Comment(text);
        }
    }
}
