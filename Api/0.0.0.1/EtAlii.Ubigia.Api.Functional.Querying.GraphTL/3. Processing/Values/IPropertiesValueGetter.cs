namespace EtAlii.Ubigia.Api.Functional 
{
    internal interface IPropertiesValueGetter
    {
        Value Get(string valueName, Structure structure);
    }
}
