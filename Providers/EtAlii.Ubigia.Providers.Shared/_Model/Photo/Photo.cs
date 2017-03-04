﻿namespace EtAlii.Ubigia.Provisioning
{
    using System.Collections.Generic;

    public class Photo
    {
        public List<Person> Persons => _persons;
        private readonly List<Person> _persons;

        public Photo()
        {
            _persons = new List<Person>();
        }
    }
}
