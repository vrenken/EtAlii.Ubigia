namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    internal interface IAddOperatorSelector
    {
        IAddOperatorSubProcessor Select(OperatorParameters parameters);
    }
}