namespace EtAlii.Servus.Api
{
    // TODO: Should be in the fabric namespace.

    public static class PropertiesHelper
    {
        public static void SetStored(IPropertyDictionary properties, bool stored)
        {
            ((PropertyDictionary)properties).Stored = stored;
        }
    }
}
