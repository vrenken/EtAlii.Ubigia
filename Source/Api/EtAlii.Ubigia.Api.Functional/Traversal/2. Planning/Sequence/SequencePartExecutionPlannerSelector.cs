// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;

    internal class SequencePartExecutionPlannerSelector :  ISequencePartExecutionPlannerSelector
    {
        private readonly IOperatorExecutionPlannerSelector _operatorExecutionPlannerSelector;
        private readonly ISubjectExecutionPlannerSelector _subjectExecutionPlannerSelector;
        private readonly ICommentExecutionPlanner _commentExecutionPlanner;

        public SequencePartExecutionPlannerSelector(
            IOperatorExecutionPlannerSelector operatorExecutionPlannerSelector,
            ISubjectExecutionPlannerSelector subjectExecutionPlannerSelector,
            ICommentExecutionPlanner commentExecutionPlanner)
        {
            _operatorExecutionPlannerSelector = operatorExecutionPlannerSelector;
            _subjectExecutionPlannerSelector = subjectExecutionPlannerSelector;
            _commentExecutionPlanner = commentExecutionPlanner;
        }

        public IExecutionPlanner Select(object item)
        {
            return item switch
            {
                Operator @operator => _operatorExecutionPlannerSelector.Select(@operator),
                Subject subject => _subjectExecutionPlannerSelector.Select(subject),
                Comment => _commentExecutionPlanner,
                _ => throw new InvalidOperationException($"Unable to select option for criteria: {(item != null ? item.ToString() : "[NULL]")}")
            };
        }
    }
}
