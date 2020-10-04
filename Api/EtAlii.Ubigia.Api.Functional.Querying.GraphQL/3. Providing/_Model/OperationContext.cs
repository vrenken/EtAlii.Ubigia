namespace EtAlii.Ubigia.Api.Functional.Querying
{
    using System;

    internal class OperationContext : Context
    {
        public static OperationContext FromDirectives(NodesDirectiveResult[] nodesDirectiveResults)
        {
            return new OperationContext
            {
                Id = Guid.NewGuid(),
                NodesDirectiveResults = nodesDirectiveResults
            };
        }
    }
}