// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Linq;
    using Moppet.Lapa;
    using Moppet.Lapa.Parsers;

    internal class DateTimeValueParser : IDateTimeValueParser
    {
        private readonly INodeValidator _nodeValidator;

        public LpsParser Parser { get; }

        string IDateTimeValueParser.Id => Id;
        public const string Id = "DateTimeValue";

        public DateTimeValueParser(INodeValidator nodeValidator)
        {
            _nodeValidator = nodeValidator;
            Parser = new LpsParser(Id, true, LpDateTime.DateTime());
        }


        public DateTime Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);

            var result = DateTime.MinValue;

            if (!LpDateTime.TryParseDateTime(node.Children.First(), ref result))
            {
                throw new ScriptParserException("Cannot parse DateTime: " + node.Match);
            }

            return result;
        }

        public bool CanParse(LpNode node)
        {
            var result = DateTime.MinValue;

            var success = node.Id == Id;
            if (success)
            {
                success = LpDateTime.TryParseDateTime(node.Children.First(), ref result);
            }
            return success;
        }
    }
}
