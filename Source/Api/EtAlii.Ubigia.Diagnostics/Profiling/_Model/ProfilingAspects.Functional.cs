// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Diagnostics.Profiling
{
    public partial class ProfilingAspects
    {
        public static FunctionalProfilers Functional { get; }
    }

    public class FunctionalProfilers
    {
        public ProfilingAspect[] All { get; }


        public ProfilingAspect ScriptSet { get; } = new(ProfilingLayer.Functional, "Script set");

        public ProfilingAspect Context { get; } = new(ProfilingLayer.Functional, "Context");

        public ProfilingAspect ScriptProcessor { get; } = new(ProfilingLayer.Functional, "Script processor");

        public ProfilingAspect ScriptSequenceProcessor { get; } = new(ProfilingLayer.Functional, "Sequence processor");

        public ProfilingAspect ScriptProcessorSubject { get; } = new(ProfilingLayer.Functional, "Subject processor");

        public ProfilingAspect ScriptProcessorPathSubject { get; } = new(ProfilingLayer.Functional, "Path subject processor");

        public ProfilingAspect ScriptProcessorPathSubjectConversion { get; } = new(ProfilingLayer.Functional, "Path conversion");

        public ProfilingAspect ScriptProcessorEntryConversion { get; } = new(ProfilingLayer.Functional, "Entry conversion");

        public ProfilingAspect ScriptParser { get; } = new(ProfilingLayer.Functional, "Script parser");

        public ProfilingAspect ScriptSequenceParser { get; } = new(ProfilingLayer.Functional, "Sequence parser");

        public ProfilingAspect ScriptPathSubjectParser { get; } = new(ProfilingLayer.Functional, "Path subject parser");


        public FunctionalProfilers()
        {
            All = new[]
            {
                Context,
                ScriptSet,
                ScriptProcessor,
                ScriptSequenceProcessor,
                ScriptProcessorSubject,
                ScriptProcessorPathSubject,
                ScriptProcessorPathSubjectConversion,
                ScriptProcessorEntryConversion,
                ScriptParser,
                ScriptSequenceParser,
                ScriptPathSubjectParser
            };
        }

    }
}
