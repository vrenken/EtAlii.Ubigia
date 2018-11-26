namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using System;

    internal class FieldRegistration : Registration
    {
        public static FieldRegistration FromDirectives(NodesDirectiveResult[] nodesDirectiveResults)//, ComplexGraphType<object> parent)
        {
            return new FieldRegistration
            {
                Id = Guid.NewGuid(),
//                Parent = parent,
                NodesDirectiveResults = nodesDirectiveResults
            };
        }
    }
}