namespace EtAlii.Ubigia.Provisioning
{
    using System.Collections.Generic;

    public class Photo
    {
        public List<Person> Persons { get; }

        public Photo()
        {
            Persons = new List<Person>();
        }
    }
}
