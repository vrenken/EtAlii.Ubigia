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

        public ScriptParseResult Parse(string text)
        {
            var parser = _scriptParserFactory.Create(_options);
            return parser.Parse(text);
        }

        public IObservable<SequenceProcessingResult> Process(Script script, IScriptScope scope)
        {
            var options = _options.CreateScope(scope);
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
