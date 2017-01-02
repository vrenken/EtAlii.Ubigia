namespace EtAlii.Ubigia.Api.Functional
{
    using Moppet.Lapa;

    internal class IsChildOfPathSubjectPartParser : IIsChildOfPathSubjectPartParser
    {
        public string Id { get { return _id; } }
        private readonly string _id = "IsChildOfPathSubjectPart";

        public LpsParser Parser { get { return _parser; } }
        private readonly LpsParser _parser;

        private readonly INodeValidator _nodeValidator;
        private readonly IPathRelationParserBuilder _pathRelationParserBuilder;

        private const string RelationId = @"\";
        private const string RelationDescription = @"IS_CHILD_OF";

        public IsChildOfPathSubjectPartParser(
            INodeValidator nodeValidator,
            IPathRelationParserBuilder pathRelationParserBuilder)
        {
            _nodeValidator = nodeValidator;
            _pathRelationParserBuilder = pathRelationParserBuilder;

            var relationParser = _pathRelationParserBuilder.CreatePathRelationParser(RelationDescription, RelationId);
            _parser = new LpsParser(Id, true, relationParser);//.Debug("IsChildOfOfPathSubjectParser");
        }

        public PathSubjectPart Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);
            return new IsChildOfPathSubjectPart();
        }


        public void Validate(PathSubjectPart before, PathSubjectPart part, int partIndex, PathSubjectPart after)
        {
            if (before is IsChildOfPathSubjectPart ||
               after is IsChildOfPathSubjectPart)
            {
                throw new ScriptParserException("Two child path separators cannot be combined.");
            }
            if (before is IsParentOfPathSubjectPart)
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
            return part is IsChildOfPathSubjectPart;
        }
    }
}
