// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    internal class PersonRootHandlerMapper : IRootHandlerMapper
    {
        public RootType Type => RootType.Person;

        public string Name { get; }

        public IRootHandler[] AllowedRootHandlers { get; }

        public PersonRootHandlerMapper()
        {
            Name = "person";

            AllowedRootHandlers = new IRootHandler[]
            {
                new PersonByLastNameFirstNameHandler(),
                new PersonByLastNameFirstNameWildcardHandler(),
                new PersonByLastNameWildcardFirstNameHandler(),
                new PersonByLastNameHandler(),

                new PersonRootByEmptyHandler(), // Should be at the end.
            };
        }
    }
}
