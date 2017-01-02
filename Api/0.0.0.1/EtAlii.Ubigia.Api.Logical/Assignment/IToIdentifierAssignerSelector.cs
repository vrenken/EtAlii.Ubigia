namespace EtAlii.Ubigia.Api.Logical
{
    internal interface IToIdentifierAssignerSelector
    {
        IToIdentifierAssigner TrySelect(object o);
    }
}