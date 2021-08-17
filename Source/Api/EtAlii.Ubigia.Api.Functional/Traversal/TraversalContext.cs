// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Linq;

    internal class TraversalContext : ITraversalContext
    {
        private readonly FunctionalOptions _options;
        private readonly IScriptProcessorFactory _scriptProcessorFactory;
        private readonly IScriptParserFactory _scriptParserFactory;

        public TraversalContext(
            FunctionalOptions options,
            IScriptProcessorFactory scriptProcessorFactory,
            IScriptParserFactory scriptParserFactory)
        {
            _options = options;
            _scriptProcessorFactory = scriptProcessorFactory;
            _scriptParserFactory = scriptParserFactory;
        }

        public ScriptParseResult Parse(string text, ExecutionScope scope)
        {
            var parser = _scriptParserFactory.Create(_options);
            return parser.Parse(text, scope);
        }

        public IObservable<SequenceProcessingResult> Process(Script script, ExecutionScope scope)
        {
            var processor = _scriptProcessorFactory.Create(_options);
            return processor.Process(script, scope);
        }

        public IObservable<SequenceProcessingResult> Process(string[] text, ExecutionScope scope)
        {
            var scriptParseResult = Parse(string.Join(Environment.NewLine, text), scope);

            if (scriptParseResult.Errors.Any())
            {
                var firstError = scriptParseResult.Errors.First();
                throw new ScriptParserException(firstError.Message, firstError.Exception);
            }

            return Process(scriptParseResult.Script, scope);
        }


        public IObservable<SequenceProcessingResult> Process(string text, ExecutionScope scope, params object[] args)
        {
            text = string.Format(text, args);
            return Process(text, scope);
        }

        public IObservable<SequenceProcessingResult> Process(string text, ExecutionScope scope)
        {
            var scriptParseResult = Parse(text, scope);

            if (scriptParseResult.Errors.Any())
            {
                var firstError = scriptParseResult.Errors.First();
                throw new ScriptParserException(firstError.Message, firstError.Exception);
            }

            return Process(scriptParseResult.Script, scope);
        }
    }
}
