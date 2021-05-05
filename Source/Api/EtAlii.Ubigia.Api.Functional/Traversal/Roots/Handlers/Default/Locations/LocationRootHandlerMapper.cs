namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    internal class LocationRootHandlerMapper : IRootHandlerMapper
    {
        public string Name { get; }

        public IRootHandler[] AllowedRootHandlers { get; }

        public LocationRootHandlerMapper()
        {
            Name = "location";

           AllowedRootHandlers = new IRootHandler[]
            {
                new LocationRootByEmptyHandler(), // only root, no arguments, should be at the end.
            };
        }
    }
}
