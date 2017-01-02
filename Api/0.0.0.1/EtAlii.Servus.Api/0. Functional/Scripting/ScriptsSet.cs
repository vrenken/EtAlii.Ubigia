namespace EtAlii.Servus.Api.Functional
{
    using System;
    using System.Linq;
    using EtAlii.Servus.Api.Logical;

    internal class ScriptsSet : IScriptsSet
    {
        private readonly IFunctionHandlersProvider _functionHandlersProvider;
        private readonly IScriptProcessorFactory _scriptProcessorFactory;
        private readonly IScriptParserFactory _scriptParserFactory;
        private readonly ILogicalContext _logicalContext;

        protected internal ScriptsSet(IFunctionHandlersProvider functionHandlersProvider,
            IScriptProcessorFactory scriptProcessorFactory, 
            IScriptParserFactory scriptParserFactory, 
            ILogicalContext logicalContext)
        {
            _functionHandlersProvider = functionHandlersProvider;
            _scriptProcessorFactory = scriptProcessorFactory;
            _scriptParserFactory = scriptParserFactory;
            _logicalContext = logicalContext;
        }

        public ScriptParseResult Parse(string text)
        {
            var scriptParserConfiguration = new ScriptParserConfiguration()
                .Use(_logicalContext.Configuration)
                //.Use(_diagnostics)
                .Use(_functionHandlersProvider);
            var parser = _scriptParserFactory.Create(scriptParserConfiguration);
            return parser.Parse(text);
        }

        public IObservable<SequenceProcessingResult> Process(Script script, IScriptScope scope)
        {
            var configuration = new ScriptProcessorConfiguration()
                .UseCaching(_logicalContext.Configuration.CachingEnabled)
                .Use(scope)
                .Use(_logicalContext);
            var processor = _scriptProcessorFactory.Create(configuration);
            return processor.Process(script);
        }

        public IObservable<SequenceProcessingResult> Process(string[] text, IScriptScope scope)
        {
            var scriptParseResult = Parse(String.Join(Environment.NewLine, text));

            if (scriptParseResult.Errors.Any())
            {
                var firstError = scriptParseResult.Errors.First();
                throw new ScriptParserException(firstError.Message, firstError.Exception);
            }

            return Process(scriptParseResult.Script, scope);
        }


        public IObservable<SequenceProcessingResult> Process(string text, params object[] args)
        {
            text = String.Format(text, args);
            return Process(text);
        }

        public IObservable<SequenceProcessingResult> Process(string[] text)
        {
            return Process(String.Join(Environment.NewLine, text));
        }

        public IObservable<SequenceProcessingResult> Process(string text)
        {
            var scriptParseResult = Parse(text);

            if (scriptParseResult.Errors.Any())
            {
                var firstError = scriptParseResult.Errors.First();
                throw new ScriptParserException(firstError.Message, firstError.Exception);
            }

            var scope = new ScriptScope();

            return Process(scriptParseResult.Script, scope);
        }
    }
}