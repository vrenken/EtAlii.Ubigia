namespace EtAlii.Ubigia.Api.Functional
{
    internal class ProvidersRootHandlerMapper : IRootHandlerMapper
    {
        public string Name { get { return _name; } }
        private readonly string _name;

        public IRootHandler[] AllowedRootHandlers { get { return _allowedRootHandlers; } }
        private readonly IRootHandler[] _allowedRootHandlers;

        public ProvidersRootHandlerMapper()
        {
            _name = "providers";

           _allowedRootHandlers = new IRootHandler[]
            {
                new ProvidersRootByEmptyHandler(), // only root, no arguments, should be at the end.
            };
        }
    }
}