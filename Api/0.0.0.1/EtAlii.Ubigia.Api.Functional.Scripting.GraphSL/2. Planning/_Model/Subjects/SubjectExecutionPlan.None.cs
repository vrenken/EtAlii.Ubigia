﻿namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using System;
    using System.Reactive.Linq;
    using System.Threading.Tasks;

    public static class SubjectExecutionPlan
    {
        /// <summary>
        /// An empty SubjectExecutionPlan.
        /// </summary>
        public static ISubjectExecutionPlan Empty { get; } = new EmptySubjectExecutionPlan();

        private class EmptySubjectExecutionPlan : ISubjectExecutionPlan
        {
            public Type OutputType { get; }
            public Subject Subject { get; }

            public EmptySubjectExecutionPlan()
            {
                OutputType = GetType();
                Subject = new EmptySubject();
            }

            public Task<IObservable<object>> Execute(ExecutionScope scope)
            {
                return Task.FromResult(Observable.Empty<object>()); 
            }

            public override string ToString()
            {
                return "[Empty]";
            }
        }
    }
}