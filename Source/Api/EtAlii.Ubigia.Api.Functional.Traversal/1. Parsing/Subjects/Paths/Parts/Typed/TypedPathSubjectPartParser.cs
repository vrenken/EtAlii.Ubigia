// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using Moppet.Lapa;

    internal class TypedPathSubjectPartParser : ITypedPathSubjectPartParser
    {
        public string Id { get; } = nameof(TypedPathSubjectPart);

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;
        private readonly INodeFinder _nodeFinder;
        private const string _textId = "Text";

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
                    Lp.Any(true, types).Id(_textId) + //.Debug("Content", true) +
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
            var text = _nodeFinder.FindFirst(node, _textId).Match.ToString();
            var formatter = TypedPathFormatter.FromString(text.ToUpper());
            return new TypedPathSubjectPart(formatter);
        }

        public void Validate(PathSubjectPartParserArguments arguments)
        {
            //if [before is ConstantPathSubjectPart || after is ConstantPathSubjectPart]
            //[
            //    throw new ScriptParserException("Two constant path parts cannot be combined.")
            //]
            //if [partIndex ! = 0 || after = = null]
            //[
            //    var constant = (ConstantPathSubjectPart)part
            //    if [constant.Name = = string.Empty]
            //    [
            //        throw new ScriptParserException("An empty constant path part is only allowed in single part paths.")
            //    ]
            //]
            //if [partIndex = = 0 && after ! = null]
            //[
            //    var constant = (ConstantPathSubjectPart)part
            //    if [constant.Name = = string.Empty]
            //    [
            //        throw new ScriptParserException("An empty constant path part is only allowed in single part paths.")
            //    ]
            //]
        }

        public bool CanValidate(PathSubjectPart part)
        {
            return part is TypedPathSubjectPart;
        }
    }
}
