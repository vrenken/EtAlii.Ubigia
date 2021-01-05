﻿namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using Moppet.Lapa;

    internal class AllChildrenPathSubjectPartParser : IAllChildrenPathSubjectPartParser
    {
        public string Id { get; } = nameof(AllChildrenPathSubjectPart);

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;

        private const string _relationId = @"\\";
        private const string _relationDescription = @"ALL_CHILDREN_OF";

        public AllChildrenPathSubjectPartParser(
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
            return new AllChildrenPathSubjectPart();
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }
    }
}
