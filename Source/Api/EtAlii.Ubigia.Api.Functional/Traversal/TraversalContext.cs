// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Linq;

    internal class TraversalContext : ITraversalContext
    {
        private readonly IScriptProcessor _scriptProcessor;
        private readonly IScriptParser _scriptParser;

        public TraversalContext(
            IScriptProcessor scriptProcessor,
            IScriptParser scriptParser)
        {
            _scriptProcessor = scriptProcessor;
            _scriptParser = scriptParser;
        }

        public ScriptParseResult Parse(string text, ExecutionScope scope) => _scriptParser.Parse(text, scope);

        public IObservable<SequenceProcessingResult> Process(Script script, ExecutionScope scope) => _scriptProcessor.Process(script, scope);

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
