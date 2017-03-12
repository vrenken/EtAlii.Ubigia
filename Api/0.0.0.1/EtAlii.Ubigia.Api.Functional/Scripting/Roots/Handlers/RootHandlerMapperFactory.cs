namespace EtAlii.Ubigia.Api.Functional
{
    internal class RootHandlerMapperFactory : IRootHandlerMapperFactory
    {
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