namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    internal class OperationRegistration
    {
        public Guid OperationId { get; private set; }
        public TraverseDirective[] Directives { get; private set; }

        public static OperationRegistration FromDirectives(IEnumerable<TraverseDirective> directives)
        {
            return new OperationRegistration
            {
                OperationId = Guid.NewGuid(),
                Directives = directives.ToArray()
            };
        }
    }
}