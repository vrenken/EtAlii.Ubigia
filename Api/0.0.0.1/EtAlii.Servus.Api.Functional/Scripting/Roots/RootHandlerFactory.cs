namespace EtAlii.Servus.Api.Functional
{
    internal class RootHandlerFactory : IRootHandlerFactory
    {
        public RootHandlerFactory()
        {
        }

        public IRootHandler[] CreateDefaults()
        {
            return new IRootHandler[]
            {
                new TimeRootHandler(),
                new PersonRootHandler(), 
                //new ObjectHandler(),
                //new StringHandler(),
            };
        }
    }
}