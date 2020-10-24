namespace EtAlii.Ubigia.Provisioning
{
    using System;
    using System.Configuration;

    [ConfigurationCollection(typeof(ProviderElement), AddItemName = "add", CollectionType=ConfigurationElementCollectionType.BasicMap)]
    public sealed class ProviderCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ProviderElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ProviderElement)element).Type;
        }

        public override ConfigurationElementCollectionType CollectionType => ConfigurationElementCollectionType.BasicMap;

        protected override string ElementName => "add";

        protected override bool IsElementName(string elementName)
        {
            return elementName.Equals(ElementName, StringComparison.InvariantCultureIgnoreCase);
        }

        public ProviderElement this[int index]
        {
            get => (ProviderElement)BaseGet(index);
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        public new ProviderElement this[string providerType] => (ProviderElement)BaseGet(providerType);

        public bool ContainsKey(string key)
        {
            var result = false;
            var keys = BaseGetAllKeys();
            foreach (var obj in keys)
            {
                if ((string)obj == key)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
    }
}