// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api.Logical;

    internal class TraversalContext : ITraversalContext
    {
        private readonly ParserOptions _parserOptions;
        private readonly IFunctionHandlersProvider _functionHandlersProvider;
        private readonly IRootHandlerMappersProvider _rootHandlerMappersProvider;
        private readonly IScriptProcessorFactory _scriptProcessorFactory;
        private readonly IScriptParserFactory _scriptParserFactory;
        private readonly ILogicalContext _logicalContext;

        public Func<TraversalProcessorOptions> ProcessorOptionsProvider { get; }

        public TraversalContext(
            ParserOptions parserOptions,
            Func<TraversalProcessorOptions> traversalProcessorOptionsProvider,
            IFunctionHandlersProvider functionHandlersProvider,
            IRootHandlerMappersProvider rootHandlerMappersProvider,
            IScriptProcessorFactory scriptProcessorFactory,
            IScriptParserFactory scriptParserFactory,
            ILogicalContext logicalContext)
        {
            ProcessorOptionsProvider = traversalProcessorOptionsProvider;
            _parserOptions = parserOptions;
            _functionHandlersProvider = functionHandlersProvider;
            _rootHandlerMappersProvider = rootHandlerMappersProvider;
            _scriptProcessorFactory = scriptProcessorFactory;
            _scriptParserFactory = scriptParserFactory;
            _logicalContext = logicalContext;
        }

        public ScriptParseResult Parse(string text)
        {
            var parser = _scriptParserFactory.Create(_parserOptions);
            return parser.Parse(text);
        }

        public IObservable<SequenceProcessingResult> Process(Script script, IScriptScope scope)
        {
            var options = ProcessorOptionsProvider()
                .Use(_logicalContext)
                .Use(_functionHandlersProvider)
                .Use(_rootHandlerMappersProvider)
                .Use(scope);
            var processor = _scriptProcessorFactory.Create(options);
            return processor.Process(script);
        }

        public IObservable<SequenceProcessingResult> Process(string[] text, IScriptScope scope)
        {
            var scriptParseResult = Parse(string.Join(Environment.NewLine, text));

            if (scriptParseResult.Errors.Any())
            {
                var firstError = scriptParseResult.Errors.First();
                throw new ScriptParserException(firstError.Message, firstError.Exception);
            }

            return Process(scriptParseResult.Script, scope);
        }


        public IObservable<SequenceProcessingResult> Process(string text, params object[] args)
        {
            text = string.Format(text, args);
            return Process(text);
        }

        public IObservable<SequenceProcessingResult> Process(string[] text)
        {
            return Process(string.Join(Environment.NewLine, text));
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
