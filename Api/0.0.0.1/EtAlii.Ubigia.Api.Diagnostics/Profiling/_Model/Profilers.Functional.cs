namespace EtAlii.Ubigia.Api.Diagnostics.Profiling
{
    public static partial class ProfilingAspects
    {
        public static readonly FunctionalProfilers Functional = new FunctionalProfilers();
    }

    public class FunctionalProfilers
    {
        public ProfilingAspect[] All { get; }


        public ProfilingAspect ScriptSet { get; } = new ProfilingAspect(ProfilingLayer.Functional, "Script set");

        public ProfilingAspect Context { get; } = new ProfilingAspect(ProfilingLayer.Functional, "Context");

        public ProfilingAspect ScriptProcessor { get; } = new ProfilingAspect(ProfilingLayer.Functional, "Script processor");

        public ProfilingAspect ScriptSequenceProcessor { get; } = new ProfilingAspect(ProfilingLayer.Functional, "Sequence processor");

        public ProfilingAspect ScriptProcessorSubject { get; } = new ProfilingAspect(ProfilingLayer.Functional, "Subject procesor");

        public ProfilingAspect ScriptProcessorPathSubject { get; } = new ProfilingAspect(ProfilingLayer.Functional, "Path subject processor");

        public ProfilingAspect ScriptProcessorPathSubjectConversion { get; } = new ProfilingAspect(ProfilingLayer.Functional, "Path conversion");

        public ProfilingAspect ScriptProcessorEntryConversion { get; } = new ProfilingAspect(ProfilingLayer.Functional, "Entry conversion");

        public ProfilingAspect ScriptParser { get; } = new ProfilingAspect(ProfilingLayer.Functional, "Script parser");

        public ProfilingAspect ScriptSequenceParser { get; } = new ProfilingAspect(ProfilingLayer.Functional, "Sequence parser");

        public ProfilingAspect ScriptPathSubjectParser { get; } = new ProfilingAspect(ProfilingLayer.Functional, "Path subject parser");


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