namespace EtAlii.Ubigia.Api.Functional
{
    internal interface IRemoveOperatorSelector
    {
        IRemoveOperatorSubProcessor Select(OperatorParameters parameters);
    }
}