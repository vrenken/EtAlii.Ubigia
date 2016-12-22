namespace EtAlii.Servus.Api.Logical
{
    internal interface IToIdentifierAssignerSelector
    {
        IToIdentifierAssigner TrySelect(object o);
    }
}