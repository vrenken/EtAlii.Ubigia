namespace EtAlii.Ubigia.Api.Functional
{
    internal class LocationsRootHandlerMapper : IRootHandlerMapper
    {
        public string Name { get { return _name; } }
        private readonly string _name;

        public IRootHandler[] AllowedRootHandlers { get { return _allowedRootHandlers; } }
        private readonly IRootHandler[] _allowedRootHandlers;

        public LocationsRootHandlerMapper()
        {
            _name = "locations";

           _allowedRootHandlers = new IRootHandler[]
            {
                new LocationsRootByEmptyHandler(), // only root, no arguments, should be at the end.
            };
        }
    }
}