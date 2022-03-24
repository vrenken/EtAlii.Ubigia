// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using Moppet.Lapa;

    internal sealed class TypedPathSubjectPartParser : ITypedPathSubjectPartParser
    {
        public string Id => nameof(TypedPathSubjectPart);

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;
        private readonly INodeFinder _nodeFinder;
        private const string TextId = "Text";

        public TypedPathSubjectPartParser(
            INodeValidator nodeValidator,
            INodeFinder nodeFinder)
        {
            _nodeValidator = nodeValidator;
            _nodeFinder = nodeFinder;

            var types = new[]
            {
                "Words", "Word",
                "Number",
                "Year", "Month", "Day", "Hour", "Minute", "Second", "Millisecond",
            };

            Parser = new LpsParser
                (Id, true,
                    Lp.One(c => c == '[') + //.Debug("Bracket-Open", true) +
                    Lp.Any(true, types).Id(TextId) + //.Debug("Content", true) +
                    Lp.One(c => c == ']')//.Debug("Bracket-Close", true)
                );//.Debug("TypedPathSubjectPartParser", true)
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }

        public PathSubjectPart Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);
            var text = _nodeFinder.FindFirst(node, TextId).Match.ToString();
            var formatter = TypedPathFormatter.FromString(text.ToUpper());
            return new TypedPathSubjectPart(formatter);
        }
    }
}
