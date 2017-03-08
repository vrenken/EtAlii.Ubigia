﻿// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Api.Functional
{
    using System.Linq;
    using Moppet.Lapa;

    internal class NonRootedPathSubjectParser : INonRootedPathSubjectParser
    {
        private const string _id = "NonRootedPathSubject";
        public LpsParser Parser => _parser;
        private readonly LpsParser _parser;
        private readonly INodeValidator _nodeValidator;
        private readonly IPathSubjectPartsParser _pathSubjectPartsParser;

        public NonRootedPathSubjectParser(
            INodeValidator nodeValidator,
            IPathSubjectPartsParser pathSubjectPartsParser)
        {
            _nodeValidator = nodeValidator;
            _pathSubjectPartsParser = pathSubjectPartsParser;
            _parser = new LpsParser
                (
                    _id, true,
                    _pathSubjectPartsParser.Parser.OneOrMore() +
                    Lp.Lookahead(Lp.Not("."))
                );//.Debug("PathSubjectParser", true);
        }

        public Subject Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, _id);
            var childNodes = node.Children ?? new LpNode[] { };
            var parts = childNodes.Select(childNode => _pathSubjectPartsParser.Parse(childNode)).ToArray();

            // A relative path with the length of 1 should not be parsed as a path but as a string constant.
            Subject result;
            var lengthIsOne = parts.Length == 1;
            if (lengthIsOne && parts[0] is ConstantPathSubjectPart)
            {
                result = new StringConstantSubject(((ConstantPathSubjectPart)parts[0]).Name);
            }
            else if (lengthIsOne && parts[0] is VariablePathSubjectPart)
            {
                result = new VariableSubject(((VariablePathSubjectPart)parts[0]).Name);
            }
            else
            {
                result = parts[0] is IsParentOfPathSubjectPart 
                    ? (NonRootedPathSubject)new AbsolutePathSubject(parts) 
                    : (NonRootedPathSubject)new RelativePathSubject(parts);
            }
            return result;
        }


        public bool CanParse(LpNode node)
        {
            return node.Id == _id;
        }

        public void Validate(SequencePart before, Subject subject, int subjectIndex, SequencePart after)
        {
            var pathSubject = subject as NonRootedPathSubject;
            var stringConstantSubject = subject as StringConstantSubject;
            if (pathSubject != null)
            {
                var parts = pathSubject.Parts;

                for (int i = 0; i < parts.Length; i++)
                {
                    var beforePathPart = i > 0 ? parts[i - 1] : null;
                    var afterPathPart = i < parts.Length - 1 ? parts[i + 1] : null;
                    var part = parts[i];
                    _pathSubjectPartsParser.Validate(beforePathPart, part, i, afterPathPart);
                }

                if (subjectIndex == 0 && pathSubject.Parts.FirstOrDefault() is ConstantPathSubjectPart)
                {
                    throw new ScriptParserException("A relative path part cannot be used as first subject.");
                }
            }
            else if (stringConstantSubject == null)
            {
                throw new ScriptParserException("Unsupported non-rooted path construction.");
            }
        }

        public bool CanValidate(Subject subject)
        {
            return subject is NonRootedPathSubject;
        }
    }
}
