namespace EtAlii.Servus.Provisioning
{
    using System.Collections.Generic;

    public class FirstName
    {
        public string Name { get { return _name; } }
        private readonly string _name;

        public List<Person> Persons { get { return _persons; } }
        private readonly List<Person> _persons;

        public FirstName(string name)
        {
            _name = name;
            _persons = new List<Person>();
        }
    }
}
