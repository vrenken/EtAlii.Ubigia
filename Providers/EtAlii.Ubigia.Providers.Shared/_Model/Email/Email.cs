namespace EtAlii.Ubigia.Provisioning
{
    public class Email
    {
        public string Address { get; }

        public Person Person { get; }

        public Email(string address, Person person)
        {
            Address = address;
            Person = person;
        }
    }
}
