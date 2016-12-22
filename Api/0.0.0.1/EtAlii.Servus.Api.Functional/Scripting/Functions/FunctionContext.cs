namespace EtAlii.Servus.Api.Functional
{
    public class FunctionContext : IFunctionContext
    {
        IPathProcessor IFunctionContext.PathProcessor { get { return _pathProcessor; } }
        private readonly IPathProcessor _pathProcessor;

        IToIdentifierConverter IFunctionContext.ToIdentifierConverter { get { return _toIdentifierConverter; } }
        private readonly IToIdentifierConverter _toIdentifierConverter;

        internal FunctionContext(
            IPathProcessor pathProcessor, 
            IToIdentifierConverter toIdentifierConverter)
        {
            _pathProcessor = pathProcessor;
            _toIdentifierConverter = toIdentifierConverter;
        }
    }
}