namespace EtAlii.Servus.Api.Functional
{
    internal interface IRemoveOperatorSelector
    {
        IRemoveOperatorSubProcessor Select(OperatorParameters parameters);
    }
}