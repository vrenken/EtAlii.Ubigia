namespace EtAlii.Ubigia.Api.Functional
{
    using Moppet.Lapa;

    internal class ChildrenPathSubjectPartParser : IChildrenPathSubjectPartParser
    {
        public string Id { get; } = "ChildrenPathSubjectPart";

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;

        private const string RelationId = @"\";
        private const string RelationDescription = @"CHILDREN_OF";

        public ChildrenPathSubjectPartParser(
            INodeValidator nodeValidator,
            IPathRelationParserBuilder pathRelationParserBuilder)
        {
            _nodeValidator = nodeValidator;

            var relationParser = pathRelationParserBuilder.CreatePathRelationParser(RelationDescription, RelationId);
            Parser = new LpsParser(Id, true, relationParser);//.Debug("IsChildOfOfPathSubjectParser");
        }

        public PathSubjectPart Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);
            return new ChildrenPathSubjectPart();
        }


        public void Validate(PathSubjectPart before, PathSubjectPart part, int partIndex, PathSubjectPart after)
        {
            if (before is ChildrenPathSubjectPart ||
               after is ChildrenPathSubjectPart)
            {
                throw new ScriptParserException("Two child path separators cannot be combined.");
            }
            if (after is ParentPathSubjectPart)
            {
                throw new ScriptParserException("The child path separator cannot be followed by a parent path separator.");
            }
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }

        public bool CanValidate(PathSubjectPart part)
        {
            return part is ChildrenPathSubjectPart;
        }
    }
}
