namespace EtAlii.Servus.Api.Functional
{
    public class RootContext : IRootContext
    {
        IPathProcessor IRootContext.PathProcessor { get { return _pathProcessor; } }
        private readonly IPathProcessor _pathProcessor;

        IToIdentifierConverter IRootContext.ToIdentifierConverter { get { return _toIdentifierConverter; } }
        private readonly IToIdentifierConverter _toIdentifierConverter;

        IPathSubjectForOutputConverter IRootContext.Converter { get { return _converter; } }
        private IPathSubjectForOutputConverter _converter;

        internal RootContext(
            IPathProcessor pathProcessor, 
            IToIdentifierConverter toIdentifierConverter, 
            IPathSubjectForOutputConverter converter)
        {
            _pathProcessor = pathProcessor;
            _toIdentifierConverter = toIdentifierConverter;
            _converter = converter;
        }
    }
}