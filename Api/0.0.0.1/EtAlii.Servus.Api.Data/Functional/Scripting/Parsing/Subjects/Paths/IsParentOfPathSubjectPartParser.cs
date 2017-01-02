namespace EtAlii.Servus.Api.Data
{
    using EtAlii.Servus.Api.Data.Model;
    using Moppet.Lapa;
    using System.Collections.Generic;

    internal class IsParentOfPathSubjectPartParser : IPathSubjectPartParser
    {
        public const string Id = "IsParentOfPathSubjectPart";
        private const string RelationId = @"/";
        private const string RelationDescription = @"IS_PARENT_OF";

        public LpsParser Parser { get { return _parser; } }
        private readonly LpsParser _parser;
        private readonly IParserHelper _parserHelper;
        private readonly IPathRelationParserBuilder _pathRelationParserBuilder;

        public IsParentOfPathSubjectPartParser(
            IParserHelper parserHelper,
            IPathRelationParserBuilder pathRelationParserBuilder)
        {
            _parserHelper = parserHelper;
            _pathRelationParserBuilder = pathRelationParserBuilder;

            var relationParser = _pathRelationParserBuilder.CreatePathRelationParser(RelationDescription, RelationId);
            _parser = new LpsParser(Id, true, relationParser);
        }

        public PathSubjectPart Parse(LpNode node)
        {
            _parserHelper.EnsureSuccess2(node, Id);
            return new IsParentOfPathSubjectPart();
        }


        public void Validate(PathSubjectPart before, PathSubjectPart part, int partIndex, PathSubjectPart after)
        {
            if(before is IsParentOfPathSubjectPart ||
               after is IsParentOfPathSubjectPart)
            {
                throw new ScriptParserException("Two parent path separators cannot be combined.");
            }
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }

        public bool CanValidate(PathSubjectPart part)
        {
            return part is IsParentOfPathSubjectPart;
        }
    }
}
