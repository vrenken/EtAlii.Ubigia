namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using Moppet.Lapa;

    internal class ChildrenPathSubjectPartParser : IChildrenPathSubjectPartParser
    {
        public string Id { get; } = nameof(ChildrenPathSubjectPart);

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;

        private const string _relationId = @"\";
        private const string _relationDescription = @"CHILDREN_OF";

        public ChildrenPathSubjectPartParser(
            INodeValidator nodeValidator,
            IPathRelationParserBuilder pathRelationParserBuilder)
        {
            _nodeValidator = nodeValidator;

            var relationParser = pathRelationParserBuilder.CreatePathRelationParser(_relationDescription, _relationId);
            Parser = new LpsParser(Id, true, relationParser);//.Debug("IsChildOfOfPathSubjectParser")
        }

        public PathSubjectPart Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);
            return new ChildrenPathSubjectPart();
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }
    }
}
