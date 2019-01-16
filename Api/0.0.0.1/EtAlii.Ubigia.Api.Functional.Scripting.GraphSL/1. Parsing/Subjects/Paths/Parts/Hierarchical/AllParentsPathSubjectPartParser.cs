namespace EtAlii.Ubigia.Api.Functional
{
    using Moppet.Lapa;

    internal class AllParentsPathSubjectPartParser : IAllParentsPathSubjectPartParser
    {
        public string Id { get; } = nameof(AllParentsPathSubjectPart);

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;

        private const string RelationId = @"//";
        private const string RelationDescription = @"ALL_PARENTS_OF";

        public AllParentsPathSubjectPartParser(
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
            return new AllParentsPathSubjectPart();
        }


        public void Validate(PathSubjectPart before, PathSubjectPart part, int partIndex, PathSubjectPart after)
        {
            if (before is ParentPathSubjectPart || after is ParentPathSubjectPart ||
                before is AllParentsPathSubjectPart || after is AllParentsPathSubjectPart)
            {
                throw new ScriptParserException("The all parents path separator cannot be combined.");
            }
            if (after is ChildrenPathSubjectPart)
            {
                throw new ScriptParserException("The all parents path separator cannot be followed by a child path separator.");
            }
            if (after is AllChildrenPathSubjectPart)
            {
                throw new ScriptParserException("The all parents path separator cannot be followed by an all child path separator.");
            }
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }

        public bool CanValidate(PathSubjectPart part)
        {
            return part is AllParentsPathSubjectPart;
        }
    }
}
