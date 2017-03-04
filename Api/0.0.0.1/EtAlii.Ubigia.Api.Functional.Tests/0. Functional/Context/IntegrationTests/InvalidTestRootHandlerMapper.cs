namespace EtAlii.Ubigia.Api.Functional.Tests
{
    internal class InvalidTestRootHandlerMapper : IRootHandlerMapper
    {
        public string Name => _name;

        public IRootHandler[] AllowedRootHandlers => _allowedRootHandlers;
        private readonly IRootHandler[] _allowedRootHandlers;

        private readonly string _name;

        public InvalidTestRootHandlerMapper()
        {
            _name = "TestRoot";

            var timePreparer = new TimePreparer();

            _allowedRootHandlers = new IRootHandler[]
            {
                new TimeRootByPathBasedYyyymmddhhmmssHandler(timePreparer),
                new TimeRootByPathBasedYyyymmddhhmmssHandler(timePreparer),
            };
        }
    }
}
