namespace EtAlii.Ubigia.Api.Functional
{
    internal class RootHandlerMapperFactory : IRootHandlerMapperFactory
    {
        public RootHandlerMapperFactory()
        {
        }

        public IRootHandlerMapper[] CreateDefaults()
        {
            return new IRootHandlerMapper[]
            {
                new TimeRootHandlerMapper(),
                new PersonRootHandlerMapper(),
                new ProvidersRootHandlerMapper(),
                new LocationsRootHandlerMapper(),
                //new ObjectHandlerMapper(),
                //new StringHandlerMapper(),
            };
        }
    }
}