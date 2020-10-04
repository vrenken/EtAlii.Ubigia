namespace EtAlii.Ubigia
{
    public static class PropertiesHelper
    {
        public static void SetStored(IPropertyDictionary properties, bool stored)
        {
            ((PropertyDictionary)properties).Stored = stored;
        }
    }
}
