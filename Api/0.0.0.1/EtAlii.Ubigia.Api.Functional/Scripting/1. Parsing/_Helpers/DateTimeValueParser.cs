﻿namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;
    using Moppet.Lapa;

    internal class DateTimeValueParser : IDateTimeValueParser
    {
        private readonly INodeValidator _nodeValidator;

        public LpsParser Parser { get; }

        public string Id { get; } = "DateTimeValue";

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
