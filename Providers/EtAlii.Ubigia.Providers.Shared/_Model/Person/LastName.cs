namespace EtAlii.Ubigia.Provisioning
{
    using System.Collections.Generic;

    public class LastName
    {
        public string Name => _name;
        private readonly string _name;

        public List<FirstName> FirstNames => _firstNames;
        private readonly List<FirstName> _firstNames;

        public LastName(string name)
        {
            _name = name;
            _firstNames = new List<FirstName>();
        }
    }
}
