namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using System;

    internal class OperationRegistration : Registration
    {
        public static OperationRegistration FromDirectives(NodesDirectiveResult[] nodesDirectiveResults)//, ComplexGraphType<object> parent)
        {
            return new OperationRegistration
            {
                Id = Guid.NewGuid(),
//                Parent = parent,
                NodesDirectiveResults = nodesDirectiveResults
            };
        }
    }
}