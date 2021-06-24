// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api.Logical;

    internal class TraversalContext : ITraversalContext
    {
        private readonly IFunctionHandlersProvider _functionHandlersProvider;
        private readonly IRootHandlerMappersProvider _rootHandlerMappersProvider;
        private readonly IScriptProcessorFactory _scriptProcessorFactory;
        private readonly IScriptParserFactory _scriptParserFactory;
        private readonly ILogicalContext _logicalContext;

        public Func<TraversalParserConfiguration> ParserConfigurationProvider { get; }
        public Func<TraversalProcessorConfiguration> ProcessorConfigurationProvider { get; }

        public TraversalContext(
            Func<TraversalParserConfiguration> traversalParserConfigurationProvider,
            Func<TraversalProcessorConfiguration> traversalProcessorConfigurationProvider,
            IFunctionHandlersProvider functionHandlersProvider,
            IRootHandlerMappersProvider rootHandlerMappersProvider,
            IScriptProcessorFactory scriptProcessorFactory,
            IScriptParserFactory scriptParserFactory,
            ILogicalContext logicalContext)
        {
            ParserConfigurationProvider = traversalParserConfigurationProvider;
            ProcessorConfigurationProvider = traversalProcessorConfigurationProvider;
            _functionHandlersProvider = functionHandlersProvider;
            _rootHandlerMappersProvider = rootHandlerMappersProvider;
            _scriptProcessorFactory = scriptProcessorFactory;
            _scriptParserFactory = scriptParserFactory;
            _logicalContext = logicalContext;
        }

        public ScriptParseResult Parse(string text)
        {
            var configuration = ParserConfigurationProvider();
            var parser = _scriptParserFactory.Create(configuration);
            return parser.Parse(text);
        }

        public IObservable<SequenceProcessingResult> Process(Script script, IScriptScope scope)
        {
            var configuration = ProcessorConfigurationProvider()
                .Use(_logicalContext)
                .Use(_functionHandlersProvider)
                .Use(_rootHandlerMappersProvider)
                .Use(scope);
            var processor = _scriptProcessorFactory.Create(configuration);
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
