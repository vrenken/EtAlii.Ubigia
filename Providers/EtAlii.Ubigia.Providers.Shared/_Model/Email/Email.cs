namespace EtAlii.Ubigia.Provisioning
{
    public class Email
    {
        public string Address {  get { return _address; } }
        private readonly string _address;

        public Person Person { get { return _person; } }
        private readonly Person _person;

        public Email(string address, Person person)
        {
            _address = address;
            _person = person;
        }
    }
}
