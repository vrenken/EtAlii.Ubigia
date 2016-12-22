namespace EtAlii.Servus.Api.Functional
{
    public interface IFunctionContext
    {
        IPathProcessor PathProcessor { get; }
        IToIdentifierConverter ToIdentifierConverter { get; }
    }
}