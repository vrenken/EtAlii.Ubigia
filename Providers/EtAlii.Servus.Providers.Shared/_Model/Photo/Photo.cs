namespace EtAlii.Servus.Provisioning
{
    using System.Collections.Generic;

    public class Photo
    {
        public List<Person> Persons { get { return _persons; } }
        private readonly List<Person> _persons;

        public Photo()
        {
            _persons = new List<Person>();
        }
    }
}
