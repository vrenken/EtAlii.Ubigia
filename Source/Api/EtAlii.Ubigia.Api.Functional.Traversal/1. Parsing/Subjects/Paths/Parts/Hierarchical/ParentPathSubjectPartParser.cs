namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using Moppet.Lapa;

    internal class ParentPathSubjectPartParser : IParentPathSubjectPartParser
    {
        public string Id { get; } = nameof(ParentPathSubjectPart);

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;

        private const string _relationId = @"/";
        private const string _relationDescription = @"PARENT_OF";

        public ParentPathSubjectPartParser(
            INodeValidator nodeValidator,
            IPathRelationParserBuilder pathRelationParserBuilder)
        {
            _nodeValidator = nodeValidator;

            var relationParser = pathRelationParserBuilder.CreatePathRelationParser(_relationDescription, _relationId);
            Parser = new LpsParser(Id, true, relationParser);//.Debug("IsParentOfPathSubjectParser")
        }

        public PathSubjectPart Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);
            return new ParentPathSubjectPart();
        }


        public void Validate(PathSubjectPartParserArguments arguments)
        {
            if (arguments.Before is ParentPathSubjectPart || arguments.After is ParentPathSubjectPart)
            {
                throw new ScriptParserException("Two parent path separators cannot be combined.");
            }
            if (arguments.After is ChildrenPathSubjectPart)
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
            return part is ParentPathSubjectPart;
        }
    }
}
