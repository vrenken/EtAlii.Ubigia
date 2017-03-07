namespace EtAlii.Ubigia.Api.Functional
{
    using Moppet.Lapa;

    internal class IsParentOfPathSubjectPartParser : IIsParentOfPathSubjectPartParser
    {
        public string Id => _id;
        private readonly string _id = "IsParentOfPathSubjectPart";

        public LpsParser Parser => _parser;
        private readonly LpsParser _parser;

        private readonly INodeValidator _nodeValidator;
        private readonly IPathRelationParserBuilder _pathRelationParserBuilder;

        private const string _relationId = @"/";
        private const string _relationDescription = @"IS_PARENT_OF";

        public IsParentOfPathSubjectPartParser(
            INodeValidator nodeValidator,
            IPathRelationParserBuilder pathRelationParserBuilder)
        {
            _nodeValidator = nodeValidator;
            _pathRelationParserBuilder = pathRelationParserBuilder;

            var relationParser = _pathRelationParserBuilder.CreatePathRelationParser(_relationDescription, _relationId);
            _parser = new LpsParser(Id, true, relationParser);//.Debug("IsParentOfPathSubjectParser");
        }

        public PathSubjectPart Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);
            return new IsParentOfPathSubjectPart();
        }


        public void Validate(PathSubjectPart before, PathSubjectPart part, int partIndex, PathSubjectPart after)
        {
            if (before is IsParentOfPathSubjectPart ||
               after is IsParentOfPathSubjectPart)
            {
                throw new ScriptParserException("Two parent path separators cannot be combined.");
            }
            if (after is IsChildOfPathSubjectPart)
            {
                throw new ScriptParserException("The parent path separator cannot be followed by a child path separator.");
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
