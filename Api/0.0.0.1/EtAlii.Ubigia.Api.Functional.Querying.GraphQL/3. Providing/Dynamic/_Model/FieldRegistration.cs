namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    internal class FieldRegistration : Registration
    {
        public static FieldRegistration FromDirectives(NodesDirectiveResult[] nodesDirectiveResults)
        {
            return new FieldRegistration
            {
                Id = Guid.NewGuid(),
                NodesDirectiveResults = nodesDirectiveResults
            };
        }
    }
}