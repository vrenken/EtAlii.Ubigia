namespace EtAlii.Ubigia.Api.Functional
{
    internal class LocationsRootHandlerMapper : IRootHandlerMapper
    {
        public string Name { get; }

        public IRootHandler[] AllowedRootHandlers { get; }

        public LocationsRootHandlerMapper()
        {
            Name = "locations";

           AllowedRootHandlers = new IRootHandler[]
            {
                new LocationsRootByEmptyHandler(), // only root, no arguments, should be at the end.
            };
        }
    }
}