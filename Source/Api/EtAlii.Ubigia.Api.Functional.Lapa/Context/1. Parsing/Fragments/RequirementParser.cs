namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using Moppet.Lapa;

    internal class RequirementParser : IRequirementParser
    {
        public LpsParser Parser { get; }

        public string Id { get; } = nameof(Requirement);

        private readonly INodeValidator _nodeValidator;

        public RequirementParser(INodeValidator nodeValidator)
        {
            _nodeValidator = nodeValidator;
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
