namespace EtAlii.Servus.Api.Functional
{
    internal interface IAddOperatorSelector
    {
        IAddOperatorSubProcessor Select(OperatorParameters parameters);
    }
}