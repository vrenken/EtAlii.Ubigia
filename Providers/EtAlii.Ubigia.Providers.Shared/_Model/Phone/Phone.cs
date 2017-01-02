namespace EtAlii.Ubigia.Provisioning
{
    public class Phone
    {
        public Person Person { get { return _person; } }
        private readonly Person _person;

        public string Number { get { return _number; } }
        private readonly string _number;

        public Phone(string number, Person person)
        {
            _number = number;
            _person = person;
        }
    }
}
