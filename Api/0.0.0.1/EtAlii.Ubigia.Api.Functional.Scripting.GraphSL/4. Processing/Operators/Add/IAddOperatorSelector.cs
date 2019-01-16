namespace EtAlii.Ubigia.Api.Functional
{
    internal interface IAddOperatorSelector
    {
        IAddOperatorSubProcessor Select(OperatorParameters parameters);
    }
}