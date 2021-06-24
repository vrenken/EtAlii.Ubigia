// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System.Collections.ObjectModel;

    internal sealed class ExecutionPlanResultSink
    {
        public ObservableCollection<Structure> Items { get; } = new();

        public ExecutionPlanResultSink[] Children { get; }

        public ExecutionPlanResultSink Parent { get; private set; }

        public Fragment Source { get; }

        public ExecutionPlanResultSink(
            Fragment source,
            ExecutionPlanResultSink[] children)
        {
            Source = source;
            Children = children;
            foreach (var child in children)
            {
                child.Parent = this;
            }
        }

        public override string ToString()
        {
            return Source.ToString();
        }
    }
}
