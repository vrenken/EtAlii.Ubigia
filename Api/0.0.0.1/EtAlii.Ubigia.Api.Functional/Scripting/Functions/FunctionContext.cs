namespace EtAlii.Ubigia.Api.Functional
{
    public class FunctionContext : IFunctionContext
    {
        IPathProcessor IFunctionContext.PathProcessor => _pathProcessor;
        private readonly IPathProcessor _pathProcessor;

        IToIdentifierConverter IFunctionContext.ToIdentifierConverter => _toIdentifierConverter;
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