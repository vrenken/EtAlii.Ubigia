namespace EtAlii.Ubigia.Provisioning
{
    using System.Collections.Generic;

    public class Person
    {
        public int Number { get; }

        public List<Email> Emails { get; }

        public List<Phone> Phones { get; }

        public Person(int number)
        {
            Number = number;
            Emails = new List<Email>();
            Phones = new List<Phone>();
        }
    }
}
