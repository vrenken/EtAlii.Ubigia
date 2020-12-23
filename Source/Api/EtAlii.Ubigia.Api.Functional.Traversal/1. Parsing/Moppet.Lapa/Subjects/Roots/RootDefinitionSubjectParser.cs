// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using Moppet.Lapa;

    internal class RootDefinitionSubjectParser : IRootDefinitionSubjectParser
    {
        public string Id { get; } = nameof(RootDefinitionSubject);

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;
        private readonly INodeFinder _nodeFinder;
        private readonly ITypeValueParser _typeValueParser;
//        private readonly IPathSubjectPartsParser _pathSubjectPartsParser
//        private const string _textId = "Text"
//        private const string _pathId = "SchemaPath"

        public RootDefinitionSubjectParser(
            INodeValidator nodeValidator,
            INodeFinder nodeFinder,
            ITypeValueParser typeValueParser
//            IPathSubjectPartsParser pathSubjectPartsParser
            )
        {
            _nodeValidator = nodeValidator;
            _nodeFinder = nodeFinder;
            _typeValueParser = typeValueParser;
//            _pathSubjectPartsParser = pathSubjectPartsParser

            Parser = new LpsParser
                (
                    Id, true,
                    _typeValueParser.Parser + //.Debug("TypeValueParser", true) //+
                    (
                        Lp.End //|
                        //(Lp.Char(':') + _pathSubjectPartsParser.Parser.OneOrMore().Wrap(PathId))
                    )//.Debug("RootDefinitionSubjectParser-inner", true)
                );//.Debug("RootDefinitionSubjectParser", true)
        }

        public Subject Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);
            var quotedTextNode = _nodeFinder.FindFirst(node, _typeValueParser.Id);
            var type = _typeValueParser.Parse(quotedTextNode);

            //PathSubject schema = null

            //var pathPart = _nodeFinder.FindFirst(node.Children, PathId)
            //if [pathPart ! = null]
            //[
            //    var parts = pathPart.Children.ToArray().Select(childNode => _pathSubjectPartsParser.Parse(childNode)).ToArray()
            //    schema = new PathSubject(parts)
            //]
            return new RootDefinitionSubject(type);//, schema)
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }

        public void Validate(SequencePart before, Subject item, int itemIndex, SequencePart after)
        {
            if (itemIndex == 0 || before == null)
            {
                throw new ScriptParserException("A root definition subject can not be used as first subject.");
            }
            if (!(before is AssignOperator))
            {
                throw new ScriptParserException("Root definition subjects can only be used with the assignment operator.");
            }
            if (after != null)
            {
                throw new ScriptParserException("Root definition subjects can only be used as the last subject in a sequence.");
            }
        }

        public bool CanValidate(Subject item)
        {
            return item is RootDefinitionSubject;
        }
    }
}
