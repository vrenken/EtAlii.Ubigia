namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    internal interface IAddOperatorSelector
    {
        IAddOperatorSubProcessor Select(OperatorParameters parameters);
    }
}
