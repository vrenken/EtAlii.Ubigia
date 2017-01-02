namespace EtAlii.Ubigia.Provisioning
{
    using System.Collections.Generic;

    public class Person
    {
        public int Number { get { return _number; } }
        private readonly int _number;

        public List<Email> Emails { get { return _emails; } }
        private readonly List<Email> _emails;

        public List<Phone> Phones { get { return _phones; } }
        private readonly List<Phone> _phones;

        public Person(int number)
        {
            _number = number;
            _emails = new List<Email>();
            _phones = new List<Phone>();
        }
    }
}
