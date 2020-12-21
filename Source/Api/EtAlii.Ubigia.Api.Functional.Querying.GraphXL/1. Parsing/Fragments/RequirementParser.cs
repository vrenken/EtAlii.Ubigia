namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using Moppet.Lapa;

    internal class RequirementParser : IRequirementParser
    {
        public LpsParser Parser { get; }

        public string Id { get; } = nameof(Requirement);

        private readonly INodeValidator _nodeValidator;
        //private readonly INodeFinder _nodeFinder

        public RequirementParser(INodeValidator nodeValidator
            //INodeFinder nodeFinder
            )
        {
            _nodeValidator = nodeValidator;
            //_nodeFinder = nodeFinder
            Parser = new LpsParser(Id, true, Lp.Char('!') | Lp.Char('?')).Maybe();
        }

        public Requirement Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);

            var requirement = Requirement.None;
            var match = node.ToString();
            switch (match)
            {
                case "?": requirement = Requirement.Optional; break;
                case "!": requirement = Requirement.Mandatory; break;
            }

            return requirement;
        }
    }
}
