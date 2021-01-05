﻿namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using Moppet.Lapa;

    internal class AllNextPathSubjectPartParser : IAllNextPathSubjectPartParser
    {
        public string Id { get; } = nameof(AllNextPathSubjectPart);

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;

        private const string _relationId = @">>";
        private const string _relationDescription = @"ALL_NEXT_OF";

        public AllNextPathSubjectPartParser(
            INodeValidator nodeValidator,
            IPathRelationParserBuilder pathRelationParserBuilder)
        {
            _nodeValidator = nodeValidator;

            var relationParser = pathRelationParserBuilder.CreatePathRelationParser(_relationDescription, _relationId);
            Parser = new LpsParser(Id, true, relationParser);
        }

        public PathSubjectPart Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);
            return new AllNextPathSubjectPart();
        }


        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }
    }
}
