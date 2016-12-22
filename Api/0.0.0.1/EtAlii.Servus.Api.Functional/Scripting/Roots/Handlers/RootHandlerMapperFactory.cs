namespace EtAlii.Servus.Api.Functional
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
                //new ObjectHandlerMapper(),
                //new StringHandlerMapper(),
            };
        }
    }
}