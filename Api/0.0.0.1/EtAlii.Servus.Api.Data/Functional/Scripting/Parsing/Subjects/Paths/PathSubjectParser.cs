namespace EtAlii.Servus.Api.Data
{
    using System.Collections.Generic;
    using EtAlii.Servus.Api.Data.Model;
    using System.Linq;
    using Moppet.Lapa;

    internal class PathSubjectParser : ISubjectParser
    {
        public const string Id = "PathSubject";
        public LpsParser Parser { get { return _parser; } }
        private readonly LpsParser _parser;
        private readonly IParserHelper _parserHelper;
        private readonly PathSubjectPartParser _pathSubjectPartParser;

        public PathSubjectParser(
            IParserHelper parserHelper,
            PathSubjectPartParser pathSubjectPartParser)
        {
            _parserHelper = parserHelper;
            _pathSubjectPartParser = pathSubjectPartParser;
            _parser = new LpsParser(Id, true, _pathSubjectPartParser.Parser.OneOrMore());
        }

        public Subject Parse(LpNode node)
        {
            _parserHelper.EnsureSuccess2(node, Id);
            var childNodes = node.Children ?? new LpNode[] { };
            var parts = childNodes.Select(childNode => _pathSubjectPartParser.Parse(childNode)).ToArray();

            var pathSubject = new PathSubject(parts);
            return pathSubject;
        }


        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }

        public void Validate(SequencePart before, Subject subject, int subjectIndex, SequencePart after)
        {
            var parts = ((PathSubject)subject).Parts;

            for (int i = 0; i < parts.Length; i++)
            {
                var beforePathPart = i > 0 ? parts[i - 1] : null;
                var afterPathPart = i < parts.Length - 1 ? parts[i + 1] : null;
                var part = parts[i];
                _pathSubjectPartParser.Validate(beforePathPart, part, i, afterPathPart);
            }

            if(subjectIndex == 0 && ((PathSubject)subject).Parts.FirstOrDefault() is ConstantPathSubjectPart)
            {
                throw new ScriptParserException("A relative path part cannot be used as first subject.");
            }
        }

        public bool CanValidate(Subject subject)
        {
            return subject is PathSubject;
        }
    }
}
