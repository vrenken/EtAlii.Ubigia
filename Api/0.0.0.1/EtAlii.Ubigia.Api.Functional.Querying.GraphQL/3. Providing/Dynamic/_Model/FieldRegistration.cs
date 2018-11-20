namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    internal class FieldRegistration
    {
        public Guid Id { get; private set; }
        public TraverseDirective[] Directives { get; private set; }

        public static FieldRegistration FromDirectives(IEnumerable<TraverseDirective> directives)
        {
            return new FieldRegistration
            {
                Id = Guid.NewGuid(),
                Directives = directives.ToArray()
            };
        }
    }
}