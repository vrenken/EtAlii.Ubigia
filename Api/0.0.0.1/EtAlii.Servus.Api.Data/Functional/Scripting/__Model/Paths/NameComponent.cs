namespace EtAlii.Servus.Api.Data
{
    public class NameComponent : PathComponent
    {
        public string Name { get; private set; }

        public NameComponent(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
