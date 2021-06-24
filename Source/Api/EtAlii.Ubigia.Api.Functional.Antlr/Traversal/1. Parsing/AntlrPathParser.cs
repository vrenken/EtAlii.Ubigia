// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Antlr.Traversal
{
    using System;
    using Antlr4.Runtime;
    using EtAlii.Ubigia.Api.Functional.Antlr;
    using EtAlii.Ubigia.Api.Functional.Traversal;

    /// <summary>
    /// The interface that abstracts away any GTL specific parser implementation.
    /// </summary>
    internal class AntlrPathParser : IPathParser
    {
        private readonly ITraversalValidator _traversalValidator;

        public AntlrPathParser(ITraversalValidator traversalValidator)
        {
            _traversalValidator = traversalValidator;
        }

        public Subject ParsePath(string text) => ParseNonRootedPath(text);

        public Subject ParseNonRootedPath(string text)
        {
            Subject pathSubject;
            try
            {
                var inputStream = new AntlrInputStream(text);
                var gtlLexer = new UbigiaLexer(inputStream);
                var commonTokenStream = new CommonTokenStream(gtlLexer);
                var parser = new UbigiaParser(commonTokenStream);
                var errorListener = new ScriptErrorListener();
                parser.RemoveErrorListeners();
                parser.AddErrorListener(errorListener);
                var context = parser.subject_non_rooted_path();

                if (parser.NumberOfSyntaxErrors != 0)
                {
                    var error = errorListener.ToErrorString();
                    throw new ScriptParserException(error);
                }
                if (context.exception != null)
                {
                    throw new ScriptParserException(context.exception.Message, context.exception);
                }

                var visitor = new UbigiaVisitor();
                pathSubject = visitor.VisitSubject_non_rooted_path(context) as Subject;

                _traversalValidator.Validate(pathSubject);
            }
            catch (Exception e)
            {
                throw new ScriptParserException(e.Message, e);
            }

            return pathSubject;
        }

        public Subject ParseRootedPath(string text)
        {
            Subject pathSubject;
            try
            {
                var inputStream = new AntlrInputStream(text);
                var gtlLexer = new UbigiaLexer(inputStream);
                var commonTokenStream = new CommonTokenStream(gtlLexer);
                var parser = new UbigiaParser(commonTokenStream);
                var errorListener = new ScriptErrorListener();
                parser.RemoveErrorListeners();
                parser.AddErrorListener(errorListener);
                var context = parser.subject_rooted_path();

                if (parser.NumberOfSyntaxErrors != 0)
                {
                    var error = errorListener.ToErrorString();
                    throw new ScriptParserException(error);
                }
                if (context.exception != null)
                {
                    throw new ScriptParserException(context.exception.Message, context.exception);
                }

                var visitor = new UbigiaVisitor();
                pathSubject = visitor.VisitSubject_rooted_path(context) as Subject;

                _traversalValidator.Validate(pathSubject);
            }
            catch (Exception e)
            {
                throw new ScriptParserException(e.Message, e);
            }

            return pathSubject;
        }
    }
}
