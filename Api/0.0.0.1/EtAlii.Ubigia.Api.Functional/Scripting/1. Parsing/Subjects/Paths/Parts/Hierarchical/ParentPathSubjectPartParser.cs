namespace EtAlii.Ubigia.Api.Functional
{
    using Moppet.Lapa;

    internal class ParentPathSubjectPartParser : IParentPathSubjectPartParser
    {
        public string Id { get; } = "ParentPathSubjectPart";

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;

        private const string RelationId = @"/";
        private const string RelationDescription = @"PARENT_OF";

        public ParentPathSubjectPartParser(
            INodeValidator nodeValidator,
            IPathRelationParserBuilder pathRelationParserBuilder)
        {
            _nodeValidator = nodeValidator;

            var relationParser = pathRelationParserBuilder.CreatePathRelationParser(RelationDescription, RelationId);
            Parser = new LpsParser(Id, true, relationParser);//.Debug("IsParentOfPathSubjectParser");
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
