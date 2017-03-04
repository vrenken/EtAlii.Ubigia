// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;
    using Moppet.Lapa;

    internal class TypedPathSubjectPartParser : ITypedPathSubjectPartParser
    {
        public string Id => _id;
        private readonly string _id = "TypedPathSubjectPart";

        public LpsParser Parser => _parser;
        private readonly LpsParser _parser;

        private readonly INodeValidator _nodeValidator;
        private readonly INodeFinder _nodeFinder;
        private const string TextId = "Text";
        private readonly string[] _types;

        public TypedPathSubjectPartParser(
            INodeValidator nodeValidator,
            INodeFinder nodeFinder)
        {
            _nodeValidator = nodeValidator;
            _nodeFinder = nodeFinder;

            _types = new[]
            {
                "Words", "Word",
                "Number",
                "Year", "Month", "Day", "Hour", "Minute", "Second", "Millisecond",
            };

            _parser = new LpsParser
                (Id, true, 
                    Lp.One(c => c == '[') + //.Debug("Bracket-Open", true) + 
                    Lp.Any(true, _types).Id(TextId) + //.Debug("Content", true) + 
                    Lp.One(c => c == ']')//.Debug("Bracket-Close", true)
                );//.Debug("TypedPathSubjectPartParser", true);
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

        public void Validate(PathSubjectPart before, PathSubjectPart part, int partIndex, PathSubjectPart after)
        {
            //if (before is ConstantPathSubjectPart || after is ConstantPathSubjectPart)
            //{
            //    throw new ScriptParserException("Two constant path parts cannot be combined.");
            //}
            //if (partIndex != 0 || after == null)
            //{
            //    var constant = (ConstantPathSubjectPart)part;
            //    if (constant.Name == String.Empty)
            //    {
            //        throw new ScriptParserException("An empty constant path part is only allowed in single part paths.");
            //    }
            //}
            //if (partIndex == 0 && after != null)
            //{
            //    var constant = (ConstantPathSubjectPart)part;
            //    if (constant.Name == String.Empty)
            //    {
            //        throw new ScriptParserException("An empty constant path part is only allowed in single part paths.");
            //    }
            //}
        }

        public bool CanValidate(PathSubjectPart part)
        {
            return part is TypedPathSubjectPart;
        }
    }
}
