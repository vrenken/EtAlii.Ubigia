namespace EtAlii.Ubigia.Api.Functional.Context
{
    internal interface IPropertiesValueGetter
    {
        Value Get(string valueName, Structure structure);
    }
}
