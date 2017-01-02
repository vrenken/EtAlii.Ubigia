namespace EtAlii.Ubigia.Provisioning
{
    public class Organization
    {
        public string Name { get { return _name; } }
        private readonly string _name;

        public Organization(string name)
        {
            _name = name;
        }
    }
}
