namespace EtAlii.Ubigia.Provisioning
{
    using System.Collections.Generic;

    public class FirstName
    {
        public string Name { get; }

        public List<Person> Persons { get; }

        public FirstName(string name)
        {
            Name = name;
            Persons = new List<Person>();
        }
    }
}
