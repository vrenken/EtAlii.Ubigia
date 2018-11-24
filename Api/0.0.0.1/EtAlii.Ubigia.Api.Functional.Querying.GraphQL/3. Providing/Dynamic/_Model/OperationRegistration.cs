namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    internal class OperationRegistration : Registration
    {
        public static OperationRegistration FromDirectives(IEnumerable<NodesDirective> directives)
        {
            return new OperationRegistration
            {
                Id = Guid.NewGuid(),
                Directives = directives.ToArray()
            };
        }
    }
}