namespace EtAlii.Ubigia.Provisioning
{
    using System.Collections.Generic;

    public class LastName
    {
        public string Name { get; }

        public List<FirstName> FirstNames { get; }

        public LastName(string name)
        {
            Name = name;
            FirstNames = new List<FirstName>();
        }
    }
}
