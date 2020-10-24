namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    internal interface IRemoveOperatorSelector
    {
        IRemoveOperatorSubProcessor Select(OperatorParameters parameters);
    }
}