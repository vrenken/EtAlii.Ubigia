// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Linq;

    internal class ScriptExecutionPlanner : IScriptExecutionPlanner
    {
        private readonly ISequenceExecutionPlanner _sequenceExecutionPlanner;

        public ScriptExecutionPlanner(ISequenceExecutionPlanner sequenceExecutionPlanner)
        {
            _sequenceExecutionPlanner = sequenceExecutionPlanner;
        }

        public ISequenceExecutionPlan[] Plan(Script script)
        {
            return script.Sequences
                .Select(_sequenceExecutionPlanner.Plan)
                //.Where(plan => plan != SequenceExecutionPlan.Empty) // // We are only interested in filled execution plans.
                .ToArray();
        }
    }
}
