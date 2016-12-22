namespace EtAlii.Servus.Api.Diagnostics.Profiling
{
    using System;

    public static partial class ProfilingAspects
    {
        public static readonly FunctionalProfilers Functional = new FunctionalProfilers();
    }

    public class FunctionalProfilers
    {
        public ProfilingAspect[] All => _all;
        private readonly ProfilingAspect[] _all;

        
        public ProfilingAspect ScriptSet => _scriptSet;
        private readonly ProfilingAspect _scriptSet = new ProfilingAspect(ProfilingLayer.Functional, "Script set");

        public ProfilingAspect Context => _context;
        private readonly ProfilingAspect _context = new ProfilingAspect(ProfilingLayer.Functional, "Context");

        public ProfilingAspect ScriptProcessor => _scriptProcessor;
        private readonly ProfilingAspect _scriptProcessor = new ProfilingAspect(ProfilingLayer.Functional, "Script processor");

        public ProfilingAspect ScriptSequenceProcessor => _scriptSequencesProcessor;
        private readonly ProfilingAspect _scriptSequencesProcessor = new ProfilingAspect(ProfilingLayer.Functional, "Sequence processor");

        public ProfilingAspect ScriptProcessorSubject => _scriptProcessorSubject;
        private readonly ProfilingAspect _scriptProcessorSubject = new ProfilingAspect(ProfilingLayer.Functional, "Subject procesor");

        public ProfilingAspect ScriptProcessorPathSubject => _scriptProcessorPathSubject;
        private readonly ProfilingAspect _scriptProcessorPathSubject = new ProfilingAspect(ProfilingLayer.Functional, "Path subject processor");

        public ProfilingAspect ScriptProcessorPathSubjectConversion => _scriptProcessorPathSubjectConversion;
        private readonly ProfilingAspect _scriptProcessorPathSubjectConversion = new ProfilingAspect(ProfilingLayer.Functional, "Path conversion");

        public ProfilingAspect ScriptProcessorEntryConversion => _scriptProcessorEntryConversion;
        private readonly ProfilingAspect _scriptProcessorEntryConversion = new ProfilingAspect(ProfilingLayer.Functional, "Entry conversion");

        public ProfilingAspect ScriptParser => _scriptParser;
        private readonly ProfilingAspect _scriptParser = new ProfilingAspect(ProfilingLayer.Functional, "Script parser");

        public ProfilingAspect ScriptSequenceParser => _scriptSequenceParser;
        private readonly ProfilingAspect _scriptSequenceParser = new ProfilingAspect(ProfilingLayer.Functional, "Sequence parser");

        public ProfilingAspect ScriptPathSubjectParser => _scriptPathSubjectParser;
        private readonly ProfilingAspect _scriptPathSubjectParser = new ProfilingAspect(ProfilingLayer.Functional, "Path subject parser");

        
        public FunctionalProfilers()
        {
            _all = new ProfilingAspect[]
            {
                _context,
                _scriptSet,
                _scriptProcessor,
                _scriptSequencesProcessor,
                _scriptProcessorSubject,
                _scriptProcessorPathSubject,
                _scriptProcessorPathSubjectConversion,
                _scriptProcessorEntryConversion,
                _scriptParser,
                _scriptSequenceParser,
                _scriptPathSubjectParser
            };
        }

    }
}