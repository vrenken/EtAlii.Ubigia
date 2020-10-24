namespace EtAlii.Ubigia.Provisioning
{
    public class Phone
    {
        public Person Person { get; }

        public string Number { get; }

        public Phone(string number, Person person)
        {
            Number = number;
            Person = person;
        }
    }
}
