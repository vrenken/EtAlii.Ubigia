// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Threading.Tasks;

    internal class RootDefinitionSubjectExecutionPlan : SubjectExecutionPlanBase
    {
        private readonly IRootDefinitionSubjectProcessor _processor;

        public RootDefinitionSubjectExecutionPlan(
            RootDefinitionSubject subject,
            IRootDefinitionSubjectProcessor processor)
            :base (subject)
        {
            _processor = processor;
        }

        protected override Type GetOutputType()
        {
            return typeof (RootDefinitionSubject);
        }

        protected override Task Execute(ExecutionScope scope, IObserver<object> output)
        {
            return _processor.Process(Subject, scope, output);
        }

        public override string ToString()
        {
            return Subject.ToString();
        }
    }
}
