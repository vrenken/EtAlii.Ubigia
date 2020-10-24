namespace EtAlii.Ubigia.Provisioning
{
    public sealed class ProviderElement : System.Configuration.ConfigurationElement
    {
        [System.Configuration.ConfigurationProperty("type", IsKey = true, IsRequired = true)]
        public string Type { get => (string)this["type"]; set => this["type"] = value; }
    }
}