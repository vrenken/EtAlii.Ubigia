﻿namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using Moppet.Lapa;

    internal class ChildrenPathSubjectPartParser : IChildrenPathSubjectPartParser
    {
        public string Id { get; } = nameof(ChildrenPathSubjectPart);

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;

        private const string RelationId = @"\";
        private const string RelationDescription = @"CHILDREN_OF";

        public ChildrenPathSubjectPartParser(
            INodeValidator nodeValidator,
            IPathRelationParserBuilder pathRelationParserBuilder)
        {
            _nodeValidator = nodeValidator;

            var relationParser = pathRelationParserBuilder.CreatePathRelationParser(RelationDescription, RelationId);
            Parser = new LpsParser(Id, true, relationParser);//.Debug("IsChildOfOfPathSubjectParser")
        }

        public PathSubjectPart Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);
            return new ChildrenPathSubjectPart();
        }


        public void Validate(PathSubjectPartParserArguments arguments)
        {
            if (arguments.Before is ChildrenPathSubjectPart || arguments.After is ChildrenPathSubjectPart)
            {
                throw new ScriptParserException("Two child path separators cannot be combined.");
            }
            if (arguments.After is ParentPathSubjectPart)
            {
                throw new ScriptParserException("The child path separator cannot be followed by a parent path separator.");
            }
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }

        public bool CanValidate(PathSubjectPart part)
        {
            return part is ChildrenPathSubjectPart;
        }
    }
}