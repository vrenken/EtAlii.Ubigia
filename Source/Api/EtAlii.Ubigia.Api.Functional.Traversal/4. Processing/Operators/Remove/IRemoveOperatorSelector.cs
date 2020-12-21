namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    internal interface IRemoveOperatorSelector
    {
        IRemoveOperatorSubProcessor Select(OperatorParameters parameters);
    }
}
