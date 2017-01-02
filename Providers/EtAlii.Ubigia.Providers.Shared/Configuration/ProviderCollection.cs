namespace EtAlii.Ubigia.Provisioning
{
    using System;
    using System.Configuration;

    [ConfigurationCollection(typeof(ProviderElement), AddItemName = "add", CollectionType=ConfigurationElementCollectionType.BasicMap)]
    public sealed class ProviderCollection : System.Configuration.ConfigurationElementCollection
    {
        protected override System.Configuration.ConfigurationElement CreateNewElement()
        {
            return new ProviderElement();
        }

        protected override object GetElementKey(System.Configuration.ConfigurationElement element)
        {
            return ((ProviderElement)element).Type;
        }

        public override System.Configuration.ConfigurationElementCollectionType CollectionType
        {
            get { return System.Configuration.ConfigurationElementCollectionType.BasicMap; }
        }

        protected override string ElementName
        {
            get { return "add"; }
        }

        protected override bool IsElementName(string elementName)
        {
            return elementName.Equals(ElementName, StringComparison.InvariantCultureIgnoreCase);
        }

        public ProviderElement this[int index]
        {
            get { return (ProviderElement)BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        new public ProviderElement this[string providerType]
        {
            get { return (ProviderElement)BaseGet(providerType); }
        }

        public bool ContainsKey(string key)
        {
            bool result = false;
            object[] keys = BaseGetAllKeys();
            foreach (object obj in keys)
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