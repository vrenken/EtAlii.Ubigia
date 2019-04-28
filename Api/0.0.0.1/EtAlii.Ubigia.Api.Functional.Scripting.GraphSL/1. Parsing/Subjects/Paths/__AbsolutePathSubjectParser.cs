﻿//// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

//namespace EtAlii.Ubigia.Api.Functional
//[
//    using System.Linq
//    using Moppet.Lapa

//    internal class AbsolutePathSubjectParser : IAbsolutePathSubjectParser
//    [
//        public const string Id = "AbsolutePathSubject"
//        public LpsParser Parser [ get [ return _parser ] ]
//        private readonly LpsParser _parser
//        private readonly INodeValidator _nodeValidator
//        private readonly IPathSubjectPartsParser _pathSubjectPartsParser

//        public AbsolutePathSubjectParser(
//            INodeValidator nodeValidator,
//            IPathSubjectPartsParser pathSubjectPartsParser)
//        [
//            _nodeValidator = nodeValidator
//            _pathSubjectPartsParser = pathSubjectPartsParser
//            _parser = new LpsParser
//                (
//                    Id, true,
//                    _pathSubjectPartsParser.Parser.OneOrMore() +
//                    Lp.Lookahead(Lp.Not("."))
//                )//.Debug("PathSubjectParser", true)
//        ]
//        public Subject Parse(LpNode node)
//        [
//            _nodeValidator.EnsureSuccess(node, Id)
//            var childNodes = node.Children ?? new LpNode[] [ ]
//            var parts = childNodes.Select(childNode => _pathSubjectPartsParser.Parse(childNode)).ToArray()

//            // A relative path with the length of 1 should not be parsed as a path but as a string constant.
//            Subject result
//            if [parts.Length == 1 && parts[0] is ConstantPathSubjectPart]
//            [
//                result = new StringConstantSubject(((ConstantPathSubjectPart) parts[0]).Name)
//            ]
//            else
//            [
//                result = new PathSubject(parts)
//            ]
//            return result
//        ]
//        public bool CanParse(LpNode node)
//        [
//            return node.Id == Id
//        ]
//        public void Validate(SequencePart before, Subject subject, int subjectIndex, SequencePart after)
//        [
//            var pathSubject = subject as PathSubject
//            var stringConstantSubject = subject as StringConstantSubject
//            if [pathSubject != null]
//            [
//                var parts = pathSubject.Parts

//                fo r [int i eq 0 i lt parts.Length i pp]
//                [
//                    var beforePathPart = i > 0 ? parts[i - 1] : null
//                    var afterPathPart = i < parts.Length - 1 ? parts[i + 1] : null
//                    var part = parts[i]
//                    _pathSubjectPartsParser.Validate(beforePathPart, part, i, afterPathPart)
//                ]
//                if [subjectIndex == 0 && pathSubject.Parts.FirstOrDefault[] is ConstantPathSubjectPart]
//                [
//                    throw new ScriptParserException("A relative path part cannot be used as first subject.")
//                ]
//            ]
//            else if (stringConstantSubject == null)
//            [
//                throw new ScriptParserException("Unsupported path construction.")
//            ]
//        ]
//        public bool CanValidate(Subject subject)
//        [
//            return subject is PathSubject
//        ]
//    ]
//]