namespace EtAlii.Ubigia.Api.Functional
{
    using Moppet.Lapa;

    internal class AllChildrenPathSubjectPartParser : IAllChildrenPathSubjectPartParser
    {
        public string Id { get; } = nameof(AllChildrenPathSubjectPart);

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;

        private const string RelationId = @"\\";
        private const string RelationDescription = @"ALL_CHILDREN_OF";

        public AllChildrenPathSubjectPartParser(
            INodeValidator nodeValidator,
            IPathRelationParserBuilder pathRelationParserBuilder)
        {
            _nodeValidator = nodeValidator;
    
            var relationParser = pathRelationParserBuilder.CreatePathRelationParser(RelationDescription, RelationId);
            Parser = new LpsParser(Id, true, relationParser);
        }

        public PathSubjectPart Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);
            return new AllChildrenPathSubjectPart();
        }


        public void Validate(PathSubjectPartParserArguments arguments)
        {
            if (arguments.Before is ChildrenPathSubjectPart || arguments.After is ChildrenPathSubjectPart ||
                arguments.Before is AllChildrenPathSubjectPart || arguments.After is AllChildrenPathSubjectPart)
            {
                throw new ScriptParserException("The all children path separator cannot be combined.");
            }
            if (arguments.After is ParentPathSubjectPart)
            {
                throw new ScriptParserException("The all children path separator cannot be followed by a parent path separator.");
            }
            if (arguments.After is AllParentsPathSubjectPart)
            {
                throw new ScriptParserException("The all children path separator cannot be followed by an all parents path separator.");
            }
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }

        public bool CanValidate(PathSubjectPart part)
        {
            return part is AllChildrenPathSubjectPart;
        }
    }
}
