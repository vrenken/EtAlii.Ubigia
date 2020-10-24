namespace EtAlii.Ubigia.Api.Functional.Querying
{
    using System;

    internal class FieldContext : Context
    {
        public static FieldContext FromDirectives(NodesDirectiveResult[] nodesDirectiveResults)
        {
            return new FieldContext
            {
                Id = Guid.NewGuid(),
                NodesDirectiveResults = nodesDirectiveResults
            };
        }
    }
}