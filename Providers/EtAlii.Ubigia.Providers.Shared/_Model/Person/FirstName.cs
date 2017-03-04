namespace EtAlii.Ubigia.Provisioning
{
    using System.Collections.Generic;

    public class FirstName
    {
        public string Name => _name;
        private readonly string _name;

        public List<Person> Persons => _persons;
        private readonly List<Person> _persons;

        public FirstName(string name)
        {
            _name = name;
            _persons = new List<Person>();
        }
    }
}
