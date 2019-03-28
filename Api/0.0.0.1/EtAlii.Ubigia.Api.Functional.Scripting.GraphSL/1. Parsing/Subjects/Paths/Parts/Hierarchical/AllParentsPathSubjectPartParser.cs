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


        public void Validate(PathSubjectPartParserArguments arguments)
        {
            if (arguments.Before is ParentPathSubjectPart || arguments.After is ParentPathSubjectPart ||
                arguments.Before is AllParentsPathSubjectPart || arguments.After is AllParentsPathSubjectPart)
            {
                throw new ScriptParserException("The all parents path separator cannot be combined.");
            }
            if (arguments.After is ChildrenPathSubjectPart)
            {
                throw new ScriptParserException("The all parents path separator cannot be followed by a child path separator.");
            }
            if (arguments.After is AllChildrenPathSubjectPart)
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
