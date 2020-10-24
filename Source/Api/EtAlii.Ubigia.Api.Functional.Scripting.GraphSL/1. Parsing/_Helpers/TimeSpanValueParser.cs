﻿namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using System;
    using Moppet.Lapa;

    internal class TimeSpanValueParser : ITimeSpanValueParser
    {
        private readonly INodeValidator _nodeValidator;
        private readonly INodeFinder _nodeFinder;

        public LpsParser Parser { get; }

        public string Id { get; } = "TimeSpanValue";

        public TimeSpanValueParser(
            INodeValidator nodeValidator, 
            INodeFinder nodeFinder)
        {
            _nodeValidator = nodeValidator;
            _nodeFinder = nodeFinder;
            Parser = new LpsParser(Id, true, LpDateTime.TimeSpan());
        }


        public TimeSpan Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);

            var result = TimeSpan.MinValue;

            var timeNode = _nodeFinder.FindFirst(node, "TimeSpan");
            if (timeNode != null)
            {
                LpDateTime.TryParseTimeSpan(timeNode, ref result);
            }

            return result;
        }

        public bool CanParse(LpNode node)
        {
            var time = TimeSpan.MinValue;

            var success = node.Id == Id;
            if (success)
            {
                var timeNode = _nodeFinder.FindFirst(node, "TimeSpan");
                if (timeNode != null)
                {
                    success &= LpDateTime.TryParseTimeSpan(timeNode, ref time);
                }
            }
            return success;
        }
    }
}