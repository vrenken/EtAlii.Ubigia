namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Linq;
    using Moppet.Lapa;

    internal class LapaPathParser : IPathParser
    {
        private const string _id = "Script";

//        private static readonly string[] _separators = new[] [ "\n", "\r\n" ]

        private readonly INodeValidator _nodeValidator;
        private readonly INonRootedPathSubjectParser _nonRootedPathSubjectParser;
        private readonly IRootedPathSubjectParser _rootedPathSubjectParser;
        private readonly ITraversalValidator _traversalValidator;
        private readonly LpsParser _nonRootedParser;
        private readonly LpsParser _rootedParser;

        public LapaPathParser(
            INodeValidator nodeValidator,
            INonRootedPathSubjectParser nonRootedPathSubjectParser,
            IRootedPathSubjectParser rootedPathSubjectParser,
            ITraversalValidator traversalValidator)
        {
            _nodeValidator = nodeValidator;
            _nonRootedPathSubjectParser = nonRootedPathSubjectParser;
            _rootedPathSubjectParser = rootedPathSubjectParser;
            _traversalValidator = traversalValidator;

            _nonRootedParser = new LpsParser(_id, true, _nonRootedPathSubjectParser.Parser);
            _rootedParser = new LpsParser(_id, true, _rootedPathSubjectParser.Parser);
        }

        public Subject ParsePath(string text)
        {
            // TODO: This class should also be able to cope with rooted paths.
            var node = _nonRootedParser.Do(text);
            _nodeValidator.EnsureSuccess(node, _id, false);
            var childNode = node.Children.Single();

            if (!_nonRootedPathSubjectParser.CanParse(childNode))
            {
                throw new ScriptParserException($"Unable to parse path (text: {text ?? "NULL"})");
            }
            var pathSubject = _nonRootedPathSubjectParser.Parse(childNode);

            // There is a possibility that we receive a string constant that needs to be validated validation.
            _traversalValidator.Validate(pathSubject);

            return pathSubject;
        }

        public Subject ParseNonRootedPath(string text)
        {
            var node = _nonRootedParser.Do(text);
            _nodeValidator.EnsureSuccess(node, _id, false);
            var childNode = node.Children.Single();

            if (!_nonRootedPathSubjectParser.CanParse(childNode))
            {
                throw new ScriptParserException($"Unable to parse non-rooted path (text: {text ?? "NULL"})");
            }
            var pathSubject = _nonRootedPathSubjectParser.Parse(childNode);

            // There is a possibility that we receive a string constant that needs to be validated validation.
            _traversalValidator.Validate(pathSubject);

            return pathSubject;
        }

        public Subject ParseRootedPath(string text)
        {
            var node = _rootedParser.Do(text);
            _nodeValidator.EnsureSuccess(node, _id, false);
            var childNode = node.Children.Single();

            if (!_rootedPathSubjectParser.CanParse(childNode))
            {
                throw new ScriptParserException($"Unable to parse rooted path (text: {text ?? "NULL"})");
            }
            var pathSubject = _rootedPathSubjectParser.Parse(childNode);

            // There is a possibility that we receive a string constant that needs to be validated validation.
            _traversalValidator.Validate(pathSubject);

            return pathSubject;
        }
    }
}
