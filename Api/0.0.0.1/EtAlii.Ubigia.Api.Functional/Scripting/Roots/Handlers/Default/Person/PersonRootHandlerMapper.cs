namespace EtAlii.Ubigia.Api.Functional
{
    internal class PersonRootHandlerMapper : IRootHandlerMapper
    {
        public string Name => _name;
        private readonly string _name;

        public IRootHandler[] AllowedRootHandlers => _allowedRootHandlers;
        private readonly IRootHandler[] _allowedRootHandlers;

        public PersonRootHandlerMapper()
        {
            _name = "person";

            _allowedRootHandlers = new IRootHandler[]
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