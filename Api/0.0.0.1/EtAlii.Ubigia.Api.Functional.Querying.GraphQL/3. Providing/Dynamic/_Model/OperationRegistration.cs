﻿namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    internal class OperationRegistration
    {
        public Guid Id { get; private set; }
        public TraverseDirective[] Directives { get; private set; }

        public static OperationRegistration FromDirectives(IEnumerable<TraverseDirective> directives)
        {
            return new OperationRegistration
            {
                Id = Guid.NewGuid(),
                Directives = directives.ToArray()
            };
        }
    }
}